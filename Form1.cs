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
            
            protected int x;
            protected int y;

            protected int width;
            protected int height;

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
                this.x = x - width - ((int)(focusedPen.Width / 2));
                this.y = y - height - ((int)(focusedPen.Width / 2));
                this.width = width;
                this.height = height;
                this.color = color;
            }
            public Figure()
            {
                color = Color.Black;
                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
                x = 0;
                y = 0;
            }
        }
        class Section : Figure
        {
            private Point p1;
            private Point p2;
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawLine(focusedPen, p1, p2);
                else
                    grObj.DrawLine(defaultPen, p1, p2);
            }
            
            public Section(int x1, int y1, int x2, int y2, Graphics grObj)
            {
                p1 = new Point(x1, y1);
                p2 = new Point(x2, y2);
            }
        }
        class Ellipse : Figure
        {
            private Rectangle rect;


            public override void paint(Graphics paintForm)
            {
                if (is_focused)
                    paintForm.DrawEllipse(focusedPen, rect);
                else
                    paintForm.DrawEllipse(defaultPen, rect);
            }

            public bool checkUnderMouse(Graphics paintForm, int x_mouse, int y_mouse)
            {
                int x0 = x;
                int y0 = y;

                int x1 = x + width * 2 + ((int)(defaultPen.Width / 2));
                int y1 = y + height * 2 + ((int)(defaultPen.Width / 2));

                if ((x_mouse >= x0) && (x_mouse <= x1) && (y_mouse >= y0) && (y_mouse <= y1))
                    return true;
                else
                    return false;
            }

            public Ellipse(int x, int y, int width, int height, Color col, Graphics paintForm)
                : base(x, y, width, height, col)
            {
                rect = new Rectangle(this.x, this.y, width * 2, height * 2);
                paint(paintForm);
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
            }
            public Storage()
            {
                storage = new List<Figure>();
            }
        }
        class StorageService : Storage
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
