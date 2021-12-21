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
        Graphics grObj;
        StorageService storage;
        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();
            grObj = canvas.CreateGraphics();
        }
        public class Figure
        {
            protected const int penWidth = 4;
            Color color;
            protected Pen defaultPen;
            protected Pen focusedPen;
            protected bool is_focused = false;
            
            protected Point p1;
            protected Size size;

            public void focus()
            {
                is_focused = true;
                ActiveForm.Invalidate();
            }
            public void unfocus()
            {
                is_focused = false;
                ActiveForm.Invalidate();
            }
            virtual public void paint(Graphics grObj)
            {
            }

            public Figure(int x, int y, int width, int height, Color color)
            {
                p1 = new Point(x - width - ((int)(focusedPen.Width / 2)), y - height - ((int)(focusedPen.Width / 2)));
                size = new Size(width, height);
                this.color = color;
            }
            public Figure()
            {
                color = Color.Black;
                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
                size = new Size(10, 10);
                p1 = new Point(0, 0);
            }
        }
        public class Section : Figure
        {
            private Point p2;

            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawLine(focusedPen, p1, p2);
                else
                    grObj.DrawLine(defaultPen, p1, p2);
            }

            public Section(int x1, int y1, int x2, int y2, Color col, Graphics grObj)
                : base(x1, y1, 10, 10, col)
            {
                p2 = new Point(x2, y2);

                paint(grObj);

                
            }
        }
        public class Ellipse : Figure
        {
            private Rectangle rect;
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawEllipse(focusedPen, rect);
                else
                    grObj.DrawEllipse(defaultPen, rect);
            }

            public Ellipse(int x, int y, int width, int height, Color col, Graphics grObj)
                : base(x, y, width, height, col)
            {
                rect = new Rectangle(p1, size);
                paint(grObj);
            }
        }

        public class Rect: Figure
        {
            private Rectangle rect;

            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawRectangle(focusedPen, rect);
                else
                    grObj.DrawRectangle(defaultPen, rect);
            }
            public Rect(int x, int y, int width, int height, Color col, Graphics grObj)
                :base(x, y, width, height, col)
            {
                rect = new Rectangle(p1, size);
                paint(grObj);
            }
        }

        public class Triangle : Figure
        {
            private PointF[] points;
            Point p2;
            Point p3;

            public override void paint(Graphics grObj)
            {

            }
            public Triangle(int x1, int y1, int x2, int y2, int x3, int y3, Color col, Graphics grObj)
            :base(x1, y1, 0, 0, col)
            {

            }
        }

        public class Storage
        {
            protected List<Figure> storage;
            public void remove(Figure obj)                            // removes all nulled elements
            {
                storage.Remove(obj);
            }
            public void add(Figure obj, Graphics ellipses)
            {
                storage.Add(obj);
            }
            public Storage()
            {
                storage = new List<Figure>();
            }
        }
        public class StorageService : Storage
        {
            public void Draw(Graphics grObj)
            {
                foreach (Figure f in storage)
                    f.paint(grObj);
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            storage.Draw(grObj);
        }
    }
}