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
        public Button button { get; set; }
        public Image image { get; set; }

        public Frame() { }
        public Frame(Button b, Image i, string tag)
        {
            isSelected = false;
            isGuessed = false;
            button = b;
            image = i;
            image.Tag = tag;     
        }

        public void open(Graphics g)
        {
            button.Visible = false;
            g.DrawImage(this.image, button.Location);
        }

        public void close() {
            button.Visible = true;
        }
    }
}
