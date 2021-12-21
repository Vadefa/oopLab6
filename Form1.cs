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
        Color currentColor;
        string currentElement;

        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();


            model = new Model();
            model.observers += new EventHandler(UpdateFromModel);
            model.observers.Invoke(this, null);

            grObj = canvas.CreateGraphics();
        }


 
        public class Figure
        {
            protected const int penWidth = 4;
            Color color;
            protected Pen defaultPen;
            protected Pen focusedPen = new Pen(Color.Violet, penWidth);
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
                defaultPen = new Pen(color, penWidth);
                p1 = new Point(x - width / 2 - penWidth / 2, y - height / 2 - penWidth / 2);
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

            public Section(int x1, int y1, int width, int height, Color col, Graphics grObj)
                : base(x1, y1, width, height, col)
            {
                p2 = new Point(p1.X + width, p1.Y + height);

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
            public Triangle(int x1, int y1, int width, int height, Color col, Graphics grObj)
            : base(x1, y1, width, height, col)
            {
                p2 = new Point (p1.X + width, p1.Y + height);
                p3 = new Point(p2.X+ 50, p1.Y + 50);
                points = new Point[] { p1, p2, p3 };
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

        public void UpdateFromModel(object sender, EventArgs e)
        {
            currentColor = model.getColor();
            currentElement = model.getElement();
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

            private int width;
            private int height;
            private int x;
            private int y;


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
                observers.Invoke(this, null);
            }
            public void nullElements()
            {
                arrow = false;
                section = false;
                ellipse = false;
                rectangle = false;
                triangle = false;
            }
            public string getElement()
            {
                if (arrow == true)
                    return "arrow";
                else if (section == true)
                    return "section";
                else if (ellipse == true)
                    return "ellipse";
                else if (triangle == true)
                    return "triangle";
                else
                    return "rectangle";
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
                observers.Invoke(this, null);
            }


            public int getWidth()
            {
                return width;
            }
            public int getHeight()
            {
                return height;
            }
            public int getX()
            {
                return x;
            }
            public int gety()
            {
                return y;
            }

            public bool is_Correct(int value)
            {
                if ((value >= 10) && value <= 200)
                    return true;
                else
                    return false;
            }
            public void setWidth(int value)
            {
                if (is_Correct(value))
                {
                    width = value;
                    observers.Invoke(this, null);
                }
            }
            public void setHeight(int value)
            {
                if (is_Correct(value))
                {
                    height = value;
                    observers.Invoke(this, null);
                }
            }
            public void setX(int value)
            {
                if (is_Correct(value))
                {
                    x = value;
                    observers.Invoke(this, null);
                }
            }
            public void setY(int value)
            {
                if (is_Correct(value))
                {
                    y = value;
                    observers.Invoke(this, null);
                }
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


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            grObj = canvas.CreateGraphics();
            storage.Draw(grObj);
        }

        private void btnSctn_Click(object sender, EventArgs e)
        {
            model.setElement((sender as Button).Name);
        }

        private void btnArw_Click(object sender, EventArgs e)
        {
            model.setElement((sender as Button).Name);
        }

        private void btnElps_Click(object sender, EventArgs e)
        {
            model.setElement((sender as Button).Name);
        }

        private void btnTrn_Click(object sender, EventArgs e)
        {
            model.setElement((sender as Button).Name);
        }

        private void btnRct_Click(object sender, EventArgs e)
        {
            model.setElement((sender as Button).Name);
        }


        private void btnBlck_Click(object sender, EventArgs e)
        {
            model.setColor((sender as Button).BackColor);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            model.setColor((sender as Button).BackColor);
        }

        private void btnGrn_Click(object sender, EventArgs e)
        {
            model.setColor((sender as Button).BackColor);
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            if (currentElement == "section")
                storage.add(new Section(mousePos.X, mousePos.Y, 50, 50, currentColor, grObj), grObj);
            else if (currentElement == "ellipse")
                storage.add(new Ellipse(mousePos.X, mousePos.Y, 50, 50, currentColor, grObj), grObj);
            else if (currentElement == "triangle")
                storage.add(new Triangle(mousePos.X, mousePos.Y, 50, 50, currentColor, grObj), grObj);
            else if (currentElement == "rectangle")
                storage.add(new Rect(mousePos.X, mousePos.Y, 50, 50, currentColor, grObj), grObj);
        }

        private void numWdt_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}