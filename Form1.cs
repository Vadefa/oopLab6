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


        public class Model
        {
            private List<bool> colors;
            private bool colorBlack;
            private bool colorBlue;
            private bool colorGreen;

            private bool[] elements;
            private bool arrow;
            private bool section;
            private bool ellipse;
            private bool rectangle;
            private bool triangle;


            public System.EventHandler observers;

            private void nullColors()
            {
                colorBlack = false;
                colorBlue = false;
                colorGreen = false;
            }
            public Color getColor()
            {
                if (colorBlack == true)
                    return Color.Black;
                else if (colorBlue == true)
                    return Color.Blue;
                else
                    return Color.ForestGreen;
            }
            public void setColor(Color color)
            {
                if (color == Color.Black)
                {
                    nullColors();
                    colorBlack = true;
                }
                else if (color == Color.Blue)
                {
                    nullColors();
                    colorBlue = true;
                }
                else
                {
                    nullColors();
                    colorGreen = true;
                }
            }
            //public void setValueA(int value)
            //{
            //    if (value_isCorrect(value))
            //    {
            //        if (value <= valueB)
            //            valueA = value;
            //        else if (value <= valueC)
            //        {
            //            valueA = value;
            //            valueB = value;
            //        }
            //        else
            //        {
            //            valueA = value;
            //            valueB = value;
            //            valueC = value;
            //        }
            //    }
            //    observers.Invoke(this, null);
            //}
            //public void setValueB(int value)
            //{
            //    if ((value >= valueA) && (value <= valueC))
            //        valueB = value;
            //    observers.Invoke(this, null);
            //}
            //public void setValueC(int value)
            //{
            //    if (value_isCorrect(value))
            //    {
            //        if (value >= valueB)
            //            valueC = value;
            //        else if (value >= valueA)
            //        {
            //            valueC = value;
            //            valueB = value;
            //        }
            //        else
            //        {
            //            valueC = value;
            //            valueB = value;
            //            valueA = value;
            //        }
            //    }
            //    observers.Invoke(this, null);
            //}
            //public int getValueA() { return valueA; }
            //public int getValueB() { return valueB; }
            //public int getValueC() { return valueC; }

            public Model()
            {
                colorBlack = true;
                colorBlue = false;
                colorGreen = false;
                colors = new List<bool> { colorBlack, colorBlue, colorGreen };

                arrow = true;
                section = false;
                ellipse = false;
                rectangle = false;
                triangle = false;
                elements = new bool[5] { arrow, section, ellipse, rectangle, triangle };

                //valueA = Properties.Settings.Default.valueA;
                //valueB = Properties.Settings.Default.valueB;
                //valueC = Properties.Settings.Default.valueC;
            }
            ~Model()
            {
                //Properties.Settings.Default.valueA = valueA;
                //Properties.Settings.Default.valueB = valueB;
                //Properties.Settings.Default.valueC = valueC;
                //Properties.Settings.Default.Save();

            }
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
            private Point[] points;
            Point p2;
            Point p3;

            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawPolygon(focusedPen, points);
                else
                    grObj.DrawPolygon(defaultPen, points);
            }
            public Triangle(int x1, int y1, int x2, int y2, int x3, int y3, Color col, Graphics grObj)
            :base(x1, y1, 10, 10, col)
            {
                points = new Point[] { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
                paint(grObj);
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

        private void btnSctn_Click(object sender, EventArgs e)
        {

        }
    }
}