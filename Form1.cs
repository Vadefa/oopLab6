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
        Model model;

        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();
            model = new Model();
            grObj = canvas.CreateGraphics();
        }


        public class Model
        {
            private bool colorBlack;
            private bool colorBlue;
            private bool colorGreen;

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
                nullColors();
                if (color == Color.Black)
                    colorBlack = true;
                else if (color == Color.Blue)
                    colorBlue = true;
                else
                    colorGreen = true;
            }
            public void nullElements()
            {
                arrow = false;
                section = false;
                ellipse = false;
                rectangle = false;
                triangle = false;
            }
            public bool getElement(string name)
            {
                if (name == "btnArw")
                    if (arrow == true)
                        return true;
                    else
                        return false;
                else if (name == "btnSctn")
                    if (section == true)
                        return true;
                    else
                        return false;
                else if (name == "btnElps")
                    if (ellipse == true)
                        return true;
                    else
                        return false;
                else if (name == "btnTrn")
                    if (triangle == true)
                        return true;
                    else
                        return false;
                else
                    if (rectangle == true)
                        return true;
                else
                    return false;
            }
            public void setElement(string name)
            {
                nullElements();
                if (name == "btnArw")
                    arrow = true;
                else if (name == "btnSctn")
                    section = true;
                else if (name == "btnElps")
                    ellipse = true;
                else if (name == "btnTrn")
                    triangle = true;
                else
                    rectangle = true;
            }

            public Model()
            {
                colorBlack = true;
                colorBlue = false;
                colorGreen = false;

                arrow = true;
                section = false;
                ellipse = false;
                rectangle = false;
                triangle = false;

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

        private void btnArw_Click(object sender, EventArgs e)
        {
            //model.setElement()
        }
    }
}