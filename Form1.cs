using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oopLab6
{
    public partial class Form1 : Form
    {

        StorageService storage;
        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();
        }
        class Figure
        {
            private Pen defaultPen;
            virtual public void paint()
            {
            }

            public Figure()
            {

            }
        }
        class Section : Figure
        {

            public override void paint()
            {

            }
        }
        class Storage
        {
            protected List<Figure> storage;
            public void remove(Figure obj)                            // removes all nulled elements
            {
                storage.Remove(obj);
            }
            public void add(Figure obj, Graphics ellipses)
            {
                storage.Add(obj);
                //unfocus it
            }
            public Storage()
            {
                storage = new List<Figure>();
            }
        }
        class StorageService : Storage
        {
            public void Draw()
            {
                foreach (Figure f in storage)
                    f.paint();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
