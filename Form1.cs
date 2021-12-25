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
        public void UpdateFromModel(object sender, EventArgs e)
        {
            currentColor = model.getColor();
            currentElement = model.getElement();

            numWdt.Value = model.getSize().Width;
            numHgh.Value = model.getSize().Height;
            numPosX.Value = model.getP1().X;
            numPosY.Value = model.getP1().Y;

            nump2X.Value = model.getP2().X;
            nump2Y.Value = model.getP2().Y;

            nump3X.Value = model.getP3().X;
            nump3Y.Value = model.getP3().Y;

            if (lvObj.SelectedItem != null)
            {
                (lvObj.SelectedItem as Figure).setColor(currentColor);
                (lvObj.SelectedItem as Figure).setSize(new Size((int)numWdt.Value, (int)numHgh.Value));
                (lvObj.SelectedItem as Figure).setP1(new Point((int)numPosX.Value, (int)numPosY.Value));
                (lvObj.SelectedItem as Figure).setP2(new Point((int)nump2X.Value, (int)nump2Y.Value));
                (lvObj.SelectedItem as Figure).setP3(new Point((int)nump3X.Value, (int)nump3Y.Value));
            }

        }


        public class Figure
        {
            protected Point p1;
            protected Point p2;
            protected int thickness;
            protected Color color;

            protected bool is_focused = false;
            public Figure(Point p1, Point p2, int thickness, Color color, Graphics grObj)
            {
                this.thickness = thickness;
                this.color = color;
                this.p1 = new Point(p1.X - thickness, p1.Y - thickness / 2);
                this.p2 = new Point(p2.X - thickness, p2.Y - thickness / 2);
                paint(grObj);
            }
            virtual public void paint(Graphics grObj)
            {
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
            public void setThickness(int thickness)
            {
                this.thickness = thickness;
                ActiveForm.Invalidate();
            }
            public void setColor(Color color)
            {
                this.color = color;
                ActiveForm.Invalidate();
            }
            public Point getP1()
            {
                return p1;
            }
            public Point getP2()
            {
                return p2;
            }
            public int getThickness()
            {
                return thickness;
            }
            public Color getColor()
            {
                return color;
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
            public void remove(Figure obj, ListBox lb)
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            canvas.Invalidate();
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

        private void size_ValueChanged(object sender, EventArgs e)
        {
            model.setSize(new Size((int)numWdt.Value, (int)numHgh.Value));
        }

        private void numP1_ValueChanged(object sender, EventArgs e)
        {
            model.setP1(new Point((int)numPosX.Value, (int)numPosY.Value));
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            if (currentElement == "btnSctn")
                storage.add(new Section(mousePos, new Point((int)nump2X.Value, (int)nump2Y.Value), new Size((int)numWdt.Value, (int)numHgh.Value), currentColor, grObj), grObj, lvObj);
            else if (currentElement == "btnElps")
                storage.add(new Ellipse(mousePos, new Size((int)numWdt.Value, (int)numHgh.Value), currentColor, grObj), grObj, lvObj);
            else if (currentElement == "btnTrn")
                storage.add(new Triangle(mousePos, new Point((int)nump2X.Value, (int)nump2Y.Value), new Point((int)nump3X.Value, (int)nump3Y.Value), new Size((int)numWdt.Value, (int)numHgh.Value), currentColor, grObj), grObj, lvObj);
            else if (currentElement == "btnRct")
                storage.add(new Rect(mousePos, new Size((int)numWdt.Value, (int)numHgh.Value), currentColor, grObj), grObj, lvObj);
            else
            {
                if (lvObj.SelectedItem != null)
                {
                    storage.unfocus();
                    lvObj.SetSelected(lvObj.SelectedIndex, false);
                }
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