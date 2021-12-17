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
            private const int penWidth = 4;
            private Pen defaultPen = new Pen(Color.Black, penWidth);
            private Pen focusedPen = new Pen(Color.Violet, penWidth);

            protected bool is_focused;
            virtual public void paint()
            {

            }

            public Figure()
            {

            }
        }
        class Section : Figure
        {
            private Point p1;
            private Point p2;
            public override void paint()
            {

            }
            
            public Section(int x1, int y1, int x2, int y2, Graphics grObj)
            {
                p1 = new Point(x1, y1);
                p2 = new Point(x2, y2);
                

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
