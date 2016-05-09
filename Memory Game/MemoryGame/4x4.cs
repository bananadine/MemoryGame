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
    public partial class SecondLevel : Form
    {
        private int timeElapsed, timeElapsed2, left;
        private int hits;
        private int opened;
        private static readonly int TIME = 90;
        List<Frame> frames = new List<Frame>();
        private bool can, wrong;
        private int wins = 0;

        public SecondLevel()
        {
            InitializeComponent();
            #region Initialize frames and add to list
            frames.Add(new Frame(this.button1, Properties.Resources.Arrow, "Arrow"));
            frames.Add(new Frame(this.button2, Properties.Resources.Arrow, "Arrow"));
            frames.Add(new Frame(this.button3, Properties.Resources.Bike, "Bike"));
            frames.Add(new Frame(this.button4, Properties.Resources.Bike, "Bike"));
            frames.Add(new Frame(this.button5, Properties.Resources.BlueCircle, "BlueCircle"));
            frames.Add(new Frame(this.button6, Properties.Resources.BlueCircle, "BlueCircle"));
            frames.Add(new Frame(this.button7, Properties.Resources.BrownSquare, "BrownSquare"));
            frames.Add(new Frame(this.button8, Properties.Resources.BrownSquare, "BrownSquare"));
            frames.Add(new Frame(this.button9, Properties.Resources.Diamond, "Diamond"));
            frames.Add(new Frame(this.button10, Properties.Resources.Diamond, "Diamond"));
            frames.Add(new Frame(this.button11, Properties.Resources.GreenTriangle, "GreenTriangle"));
            frames.Add(new Frame(this.button12, Properties.Resources.GreenTriangle, "GreenTriangle"));
            frames.Add(new Frame(this.button13, Properties.Resources.PurpleHeart, "PurpleHeart"));
            frames.Add(new Frame(this.button14, Properties.Resources.PurpleHeart, "PurpleHeart"));
            frames.Add(new Frame(this.button15, Properties.Resources.RedStar, "RedStar"));
            frames.Add(new Frame(this.button16, Properties.Resources.RedStar, "RedStar"));

            foreach (Frame f in frames)
            {
                f.button.Click += new System.EventHandler(this.click);
            }
            opened = 0;
            #endregion
            newGame();
        }

        private void newGame()
        {
            timer1.Start();
            timer2.Start();
            foreach (Frame f in frames)
            {
                f.isSelected = false;
                f.isGuessed = false;
                f.button.Visible = true;
                f.button.Enabled = false;
            }


            Random r = new Random();

            foreach (Frame f in frames)
            {
                int index = r.Next(16);
                Frame temp = new Frame();
                temp.button = f.button;
                f.button = frames[index].button;
                frames[index].button = temp.button;
            }

            foreach (Frame f in frames)
            {
                f.button.BackgroundImage = f.image;
            }



            can = true;
            wrong = false;
            hits = 0;
            opened = 0;
            timeElapsed = 0;
            timeElapsed2 = 0;
            updateTime2();
            Invalidate();
        }

        private void click(object sender, EventArgs e)
        {
            if (can == false) return;
            Button tmp = sender as Button;
            foreach (Frame f in frames)
            {
                if (f.button == tmp)
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
                        can = false;
                        Invalidate();
                        validateGuess();
                    }
                }
            }
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
                    else { f2 = f; }
                }
            }
            if (f1.image.Tag == f2.image.Tag)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.pop);
                sound.Play();
                f1.isGuessed = true;
                f2.isGuessed = true;
                f1.isSelected = false;
                f2.isSelected = false;
                hits++;
                can = true;

            }
            else
            {
                wrong = true;
                SoundPlayer sound = new SoundPlayer(Properties.Resources.buzz);
                sound.Play();
                first = f1;
                second = f2;
                Timer t1 = new Timer();
                t1.Interval = 500;
                t1.Tick += new EventHandler(t1_Tick);
                t1.Start();
            }
        }

        private void t1_Tick(object sender, EventArgs e)
        {
            first.isSelected = false;
            second.isSelected = false;

            can = true;
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
                f.button.BackgroundImage = Properties.Resources.QuestionMark;
                f.button.Enabled = true;
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
        }

        private void SecondLevel_Paint(object sender, PaintEventArgs e)
        {
            foreach (Frame f in frames)
            {
                    if (f.isSelected || f.isGuessed)
                    {
                        f.open(e.Graphics);
                    }
                    else
                    {
                        f.close();
                    }
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
                    if (MessageBox.Show("Следно ниво?", "Следно ниво", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {

                        ThirdLevel form = new ThirdLevel();
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
