using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace oopLab6
{
    public partial class PopupCanvSetSize : Form
    {
        private PopupDelegate d;
        public PopupCanvSetSize(PopupDelegate sender)
        {
            d = sender;
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            d(this);
        }
    }
}
