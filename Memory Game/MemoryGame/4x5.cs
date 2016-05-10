using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Media;

namespace MemoryGame
{
    public partial class ThirdLevel : Form
    {
        private int timeElapsed, timeElapsed2, left;
        private int hits, opened, wins;
        private static readonly int TIME = 120;
        List<Frame> frames = new List<Frame>();
        private bool canOpen, wrong;

        public ThirdLevel()
        {
            InitializeComponent();
            #region Initialize Frames and add to list
            frames.Add(new Frame(this.pictureBox1, Properties.Resources.Arrow, Properties.Resources.QuestionMark, "Arrow"));
            frames.Add(new Frame(this.pictureBox2, Properties.Resources.Arrow, Properties.Resources.QuestionMark, "Arrow"));
            frames.Add(new Frame(this.pictureBox3, Properties.Resources.Bike, Properties.Resources.QuestionMark, "Bike"));
            frames.Add(new Frame(this.pictureBox4, Properties.Resources.Bike, Properties.Resources.QuestionMark, "Bike"));
            frames.Add(new Frame(this.pictureBox5, Properties.Resources.BlueCircle, Properties.Resources.QuestionMark, "BlueCircle"));
            frames.Add(new Frame(this.pictureBox6, Properties.Resources.BlueCircle, Properties.Resources.QuestionMark, "BlueCircle"));
            frames.Add(new Frame(this.pictureBox7, Properties.Resources.BrownSquare, Properties.Resources.QuestionMark, "BrownSquare"));
            frames.Add(new Frame(this.pictureBox8, Properties.Resources.BrownSquare, Properties.Resources.QuestionMark, "BrownSquare"));
            frames.Add(new Frame(this.pictureBox9, Properties.Resources.Diamond, Properties.Resources.QuestionMark, "Diamond"));
            frames.Add(new Frame(this.pictureBox10, Properties.Resources.Diamond, Properties.Resources.QuestionMark, "Diamond"));
            frames.Add(new Frame(this.pictureBox11, Properties.Resources.GreenTriangle, Properties.Resources.QuestionMark, "GreenTriangle"));
            frames.Add(new Frame(this.pictureBox12, Properties.Resources.GreenTriangle, Properties.Resources.QuestionMark, "GreenTriangle"));
            frames.Add(new Frame(this.pictureBox13, Properties.Resources.PurpleHeart, Properties.Resources.QuestionMark, "PurpleHeart"));
            frames.Add(new Frame(this.pictureBox14, Properties.Resources.PurpleHeart, Properties.Resources.QuestionMark, "PurpleHeart"));
            frames.Add(new Frame(this.pictureBox15, Properties.Resources.RedStar, Properties.Resources.QuestionMark, "RedStar"));
            frames.Add(new Frame(this.pictureBox16, Properties.Resources.RedStar, Properties.Resources.QuestionMark, "RedStar"));
            frames.Add(new Frame(this.pictureBox17, Properties.Resources.YingYang, Properties.Resources.QuestionMark, "YingYang"));
            frames.Add(new Frame(this.pictureBox18, Properties.Resources.YingYang, Properties.Resources.QuestionMark, "YingYang"));
            frames.Add(new Frame(this.pictureBox19, Properties.Resources.Cross, Properties.Resources.QuestionMark, "Cross"));
            frames.Add(new Frame(this.pictureBox20, Properties.Resources.Cross, Properties.Resources.QuestionMark, "Cross"));

            foreach (Frame f in frames)
            {
                f.pictureBox.Click += new System.EventHandler(this.click);
            }
            opened = 0;
            #endregion
            newGame();
        }

        private void newGame()
        {
            timer1.Start();
            timer2.Start();

            canOpen = true;
            wrong = false;
            hits = 0;
            opened = 0;
            timeElapsed = 0;
            timeElapsed2 = 0;
            left = TIME;

            foreach (Frame f in frames)
            {
                f.isSelected = false;
                f.isGuessed = false;
                f.pictureBox.Enabled = false;
            }


            Random r = new Random();

            foreach (Frame f in frames)
            {
                int index = r.Next(8);
                Frame temp = new Frame();
                temp.pictureBox = f.pictureBox;
                f.pictureBox = frames[index].pictureBox;
                frames[index].pictureBox = temp.pictureBox;
            }

            updateTime2();
            Invalidate();
        }


        Frame first, second;

        public void validateGuess()
        {
            Frame f1 = new Frame();
            Frame f2 = new Frame();
            bool tmp = true; // za proverka dali vo f1 veke ima ramka
            foreach (Frame f in frames)
            {
                if (f.isSelected)
                {
                    if (tmp)
                    {
                        f1 = f;
                        tmp = false;
                    }
                    else
                    {
                        f2 = f;
                    }
                }
            }
            if (f1.image1.Tag == f2.image1.Tag)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.pop);
                sound.Play();
                f1.isGuessed = true;
                f2.isGuessed = true;
                f1.isSelected = false;
                f2.isSelected = false;
                hits++;
                canOpen = true;

            }
            else
            {
                wrong = true;
                SoundPlayer sound = new SoundPlayer(Properties.Resources.buzz);
                sound.Play();
                first = f1;
                second = f2;
                Timer closer = new Timer();
                closer.Interval = 500;
                closer.Tick += new EventHandler(closer_Tick);
                closer.Start();
            }
        }

        private void closer_Tick(object sender, EventArgs e)
        {
            first.isSelected = false;
            second.isSelected = false;
            canOpen = true;

            ((Timer)sender).Stop();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            timeElapsed2++;
            updateTime2();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            timer1.Interval = 1000;
            timeElapsed++;
            updateTime();

            foreach (Frame f in frames)
            {
                f.open();
                f.pictureBox.Enabled = true;
            }

            if (timeElapsed == TIME || left < 0)
            {
                timer1.Stop();
                SoundPlayer sound = new SoundPlayer(Properties.Resources.sad_trombone);
                sound.Play();
                if (MessageBox.Show("Нова игра?", "Изгубивте!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    timer1.Interval = 5000;
                    newGame();
                }
                else
                    Close();
            }
            Invalidate();
            checkWin();
        }

        private void updateTime()
        {
            if (!wrong)
            {
                int left = TIME - timeElapsed;
                int min = left / 60;
                int sec = left % 60;
                lblTimeLeft.Text = string.Format("{0:00}:{1:00}", min, sec);
            }
            else
            {
                wrong = false;
                timeElapsed += 5;
                left = TIME - timeElapsed;
                int min = left / 60;
                int sec = left % 60;
                if (left < 0)
                {
                    lblTimeLeft.Text = string.Format("{0:00}:{1:00}", "00", "00");
                }
                else
                {
                    lblTimeLeft.Text = string.Format("{0:00}:{1:00}", min, sec);
                }
            }
        }

        private void updateTime2()
        {
            int left = 5 - timeElapsed2;
            int min = left / 60;
            int sec = left % 60;
            lblTimeLeft.Text = string.Format("{0:00}:{1:00}", min, sec);
            foreach (Frame f in frames)
            {
                f.open();
            }
        }

        private void checkWin()
        {
            if (hits == (frames.Count) / 2)
            {
                wins++;
                timer1.Stop();
                SoundPlayer sound = new SoundPlayer(Properties.Resources.level_completed);
                sound.Play();
                MessageBox.Show("Честито! Победивте!", "Победа");
                if (wins < 2)
                {
                    if (MessageBox.Show("Нова игра?", "Нова игра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {

                        timer1.Interval = 5000;
                        newGame();
                    }
                    else
                        Close();
                }
                else if (wins == 2)
                {
                    if (MessageBox.Show("Ја превртевте играта! Дали сакате да играте од почеток?", "Од почеток", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        FirstLevel form = new FirstLevel();
                        form.Show();
                        Close();
                    }
                    else
                    {
                        if (MessageBox.Show("Нова игра?", "Нова игра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {

                            timer1.Interval = 5000;
                            newGame();
                        }
                        else
                            Close();
                    }
                }
            }
        }

        private void click(object sender, EventArgs e)
        {
            if (canOpen == false) return;

            else
            {
                PictureBox tmp = sender as PictureBox;
                foreach (Frame f in frames)
                {
                    if (f.pictureBox == tmp)
                    {
                        if (opened == 0)
                        {
                            f.isSelected = true;
                            opened++;
                            Invalidate();
                        }
                        else if (opened == 1)
                        {
                            f.isSelected = true;
                            opened = 0;
                            canOpen = false;
                            Invalidate();
                            validateGuess();
                        }
                    }
                }
            }
        }

        private void ThirdLevel_Paint(object sender, PaintEventArgs e)
        {
            foreach (Frame f in frames)
            {
                if (f.isSelected || f.isGuessed)
                {
                    f.open();
                }
                else if (!timer2.Enabled)
                {
                    f.close();
                }
            }
        }

        private void новаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Нова игра?", "Нова игра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                timer1.Stop();
                timer1.Interval = 5000;
                newGame();
            }
        }

        private void крајToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            Close();
        }

        private void паузаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled && !timer2.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

   }
}
