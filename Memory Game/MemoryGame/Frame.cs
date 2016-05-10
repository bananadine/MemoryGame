using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace MemoryGame
{
    public class Frame
    {
        public bool isSelected { get; set; }
        public bool isGuessed { get; set; }
        public PictureBox pictureBox { get; set; }
        public Image image1 { get; set; }
        public Image image2 { get; set; }

        public Frame() { }
        public Frame(PictureBox pb, Image i1, Image i2, string tag)
        {
            isSelected = false;
            isGuessed = false;
            pictureBox = pb;
            image1 = i1;
            image2 = i2;
            image1.Tag = tag;
        }

        public void open()
        {
            pictureBox.Image = this.image1;
        }

        public void close()
        {
            pictureBox.Image = this.image2;
        }
    }
}
