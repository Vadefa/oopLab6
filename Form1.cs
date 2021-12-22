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
        int width;
        int height;
        int x;
        int y;

        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();


            model = new Model();
            model.observers += new EventHandler(UpdateFromModel);
            model.observers.Invoke(this, null);

            grObj = canvas.CreateGraphics();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            canvas.Invalidate();
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            grObj = canvas.CreateGraphics();
            storage.Draw(grObj);
        }


        public class Figure
        {
            protected const int penWidth = 4;
            protected Pen defaultPen;
            protected Pen focusedPen;
            protected bool is_focused = false;

            Color color;
            protected Size size;
            protected Point p1;
            protected Point p2;
            protected Point p3;

            public void setColor(Color color)
            {
                this.color = color;
                ActiveForm.Invalidate();
            }
            public void setSize(Size size)
            {
                this.size = size;
                ActiveForm.Invalidate();
            }
            public void setP1(Point p)
            {
                p1 = p;
                ActiveForm.Invalidate();
            }
            public void setP2(Point p)
            {
                p2 = p;
                ActiveForm.Invalidate();
            }
            public void setP3(Point p)
            {
                p3 = p;
                ActiveForm.Invalidate();
            }
            public Color getColor()
            {
                return color;
            }
            public Size getSize()
            {
                return size;
            }
            public Point getP1()
            {
                return p1;
            }
            public Point getP2()
            {
                return p2;
            }
            public Point getP3()
            {
                return p3;
            }
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

            public Figure(Point p, Size size, Color color)
            {
                this.color = color;
                this.size = size;
                p1 = p;
                p2 = new Point(0, 0);
                p3 = new Point(0, 0);

                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
            }
            public Figure(Point p1, Point p2, Size size, Color color)
            {
                this.color = color;
                this.size = size;
                this.p1 = p1;
                this.p2 = p2;
                p3 = new Point(0, 0);

                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
            }
            public Figure(Point p1, Point p2, Point p3, Size size, Color color)
            {
                this.color = color;
                this.size = size;
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;

                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
            }
            public Figure()
            {
                color = Color.Black;
                size = new Size(10, 10);
                p1 = new Point(0, 0);
                p2 = new Point(0, 0);
                p3 = new Point(0, 0);
                defaultPen = new Pen(color, penWidth);
                focusedPen = new Pen(Color.Violet, penWidth);
            }
        }
        public class Section : Figure
        {
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawLine(focusedPen, p1, p2);
                else
                    grObj.DrawLine(defaultPen, p1, p2);
            }
            public Section(Point p1, Point p2, Size size, Color col, Graphics grObj)
                : base(p1, p2, size, col)
            {
                paint(grObj);
            }
        }
        public class Ellipse : Figure
        {
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawEllipse(focusedPen, new Rectangle(p1, size));
                else
                    grObj.DrawEllipse(defaultPen, new Rectangle(p1, size));
            }

            public Ellipse(Point p, Size size, Color col, Graphics grObj)
                : base(p, size, col)
            {
                paint(grObj);
            }
        }

        public class Rect: Figure
        {
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawRectangle(focusedPen, new Rectangle(p1, size));
                else
                    grObj.DrawRectangle(defaultPen, new Rectangle(p1, size));
            }
            public Rect(Point p, Size size, Color col, Graphics grObj)
                :base(p, size, col)
            {
                paint(grObj);
            }
        }

        public class Triangle : Figure
        {
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawPolygon(focusedPen, new Point[] { p1, p2, p3 });
                else
                    grObj.DrawPolygon(defaultPen, new Point[] { p1, p2, p3 });
            }
            public Triangle(Point p1, Point p2, Point p3, Size size, Color col, Graphics grObj)
            : base(p1, p2, p3, size, col)
            {
                paint(grObj);
            }
        }

        public class Storage
        {
            protected List<Figure> storage;
            public void remove(Figure obj, ListBox lb)                            // removes all nulled elements
            {
                storage.Remove(obj);
                lb.Items.Remove(obj);
            }
            public void add(Figure obj, Graphics ellipses, ListBox lb)
            {
                storage.Add(obj);
                lb.Items.Add(obj);
            }
            public Storage()
            {
                storage = new List<Figure>();
            }
        }
        public class StorageService : Storage
        {
            Figure selected;
            public void Draw(Graphics grObj)
            {
                foreach (Figure f in storage)
                    f.paint(grObj);
            }
            public void focus(Figure obj)
            {
                if (obj != null)
                {
                    obj.focus();
                    selected = obj;
                }
            }
            public void unfocus()
            {
                if (selected != null)
                    selected.unfocus();
            }
        }

        public void UpdateFromModel(object sender, EventArgs e)
        {
            currentColor = model.getColor();
            currentElement = model.getElement();
            width = model.getWidth();
            height = model.getHeight();
            x = model.getX();
            y = model.gety();

            numWdt.Value = width;
            numHgh.Value = height;
            numPosX.Value = x;
            numPosY.Value = y;

            if (lvObj.SelectedItem != null)
            {
                storage.setWidth(lvObj.SelectedItem as Figure, width);
                storage.setHeight(lvObj.SelectedItem as Figure, height);
                storage.setX(lvObj.SelectedItem as Figure, x);
                storage.setY(lvObj.SelectedItem as Figure, y);
            }

        }
        public class Model
        {
            private Color color;
            private string element;

            private Size size;
            private Point p1;
            private Point p2;
            private Point p3;


            public System.EventHandler observers;

            public Color getColor()
            {
                return color;
            }
            public void setColor(Color color)
            {
                this.color = color;
            }
            public string getElement()
            {
                return element;
            }
            public void setElement(string name)
            {
                element = name;
                observers.Invoke(this, null);
            }
            public Size getSize()
            {
                return size;
            }
            public Point getP1()
            {
                return p1;
            }
            public Point getP2()
            {
                return p2;
            }
            public Point getP3()
            {
                return p3;
            }

            public bool is_CorrectSize(Size s)
            {
                if ((s.Width >= 10) && s.Width <= 500 && s.Height >= 10 && s.Height <= 500)
                    return true;
                else
                    return false;
            }
            public bool is_CorrectPos(Point p)
            {
                if (p.X >= 0 && p.X <= 500 && p.Y >= 0 && p.Y <= 500)
                    return true;
                else
                    return false;
            }
            public void setSize(Size size)
            {
                if (is_CorrectSize(size))
                {
                    this.size = size;
                    observers.Invoke(this, null);
                }
            }
            public void setP1(Point p)
            {
                if (is_CorrectPos(p))
                {
                    p1 = p;
                    observers.Invoke(this, null);
                }
            }
            public void setP2(Point p)
            {
                if (is_CorrectPos(p))
                {
                    p2 = p;
                    observers.Invoke(this, null);
                }
            }
            public void setP3(Point p)
            {
                if (is_CorrectPos(p))
                {
                    p3 = p;
                    observers.Invoke(this, null);
                }
            }

            public void getObject(Figure obj)
            {
                if (obj != null)
                {
                    color = obj.getColor();
                    size = obj.getSize();
                    p1 = obj.getP1();
                    p2 = obj.getP2();
                    p3 = obj.getP3();
                    observers.Invoke(this, null);
                }
            }

            public void destructor()
            {

            }
            public Model()
            {
                color = Color.Black;
                size = new Size(Properties.Settings.Default.width, Properties.Settings.Default.height);
                element = "";
                p1 = new Point(0, 0);
                p2 = new Point(0, 0);
                p3 = new Point(0, 0);
            }
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

        private void numWdt_ValueChanged(object sender, EventArgs e)
        {
            model.setWidth((int)(sender as NumericUpDown).Value);
            
        }

        private void numHgh_ValueChanged(object sender, EventArgs e)
        {

            model.setHeight((int)(sender as NumericUpDown).Value);
        }

        private void numPosX_ValueChanged(object sender, EventArgs e)
        {
            model.setX((int)(sender as NumericUpDown).Value);
        }

        private void numPosY_ValueChanged(object sender, EventArgs e)
        {
            model.setY((int)(sender as NumericUpDown).Value);
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            if (currentElement == "section")
                storage.add(new Section(mousePos.X, mousePos.Y, width, height, currentColor, grObj), grObj, lvObj);
            else if (currentElement == "ellipse")
                storage.add(new Ellipse(mousePos.X, mousePos.Y, width, height, currentColor, grObj), grObj, lvObj);
            else if (currentElement == "triangle")
                storage.add(new Triangle(mousePos.X, mousePos.Y, width, height, currentColor, grObj), grObj, lvObj);
            else if (currentElement == "rectangle")
                storage.add(new Rect(mousePos.X, mousePos.Y, width, height, currentColor, grObj), grObj, lvObj);
            else
            {
                storage.unfocus();
                lvObj.SetSelected(lvObj.SelectedIndex, false);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.destructor();
        }

        private void btnTrsh_Click(object sender, EventArgs e)
        {
            storage.remove(lvObj.SelectedItem as Figure, lvObj);
        }

        private void lvObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            storage.unfocus();
            model.getObject(lvObj.SelectedItem as Figure);
            storage.focus(lvObj.SelectedItem as Figure);
        }
    }
}