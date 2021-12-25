﻿using System;
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
        //Model model;
        Color currentColor = Color.Black;
        string currentElement;
        Point mp1;
        Point mp2;
        Point mp3;

        public Form1()
        {
            InitializeComponent();
            storage = new StorageService();


            //model = new Model();
            //model.observers += new EventHandler(UpdateFromModel);
            //model.observers.Invoke(this, null);

            grObj = canvas.CreateGraphics();
        }

        //public class Model
        //{
        //    private Color color;
        //    private string element;

        //    private Size size;
        //    private Point p1;
        //    private Point p2;
        //    private Point p3;


        //    public System.EventHandler observers;

        //    public Color getColor()
        //    {
        //        return color;
        //    }
        //    public void setColor(Color color)
        //    {
        //        this.color = color;
        //    }
        //    public string getElement()
        //    {
        //        return element;
        //    }
        //    public void setElement(string name)
        //    {
        //        element = name;
        //        observers.Invoke(this, null);
        //    }
        //    public Size getSize()
        //    {
        //        return size;
        //    }
        //    public Point getP1()
        //    {
        //        return p1;
        //    }
        //    public Point getP2()
        //    {
        //        return p2;
        //    }
        //    public Point getP3()
        //    {
        //        return p3;
        //    }

        //    public bool is_CorrectSize(Size s)
        //    {
        //        if ((s.Width >= 10) && s.Width <= 500 && s.Height >= 10 && s.Height <= 500)
        //            return true;
        //        else
        //            return false;
        //    }
        //    public bool is_CorrectPos(Point p)
        //    {
        //        if (p.X >= 0 && p.X <= 500 && p.Y >= 0 && p.Y <= 500)
        //            return true;
        //        else
        //            return false;
        //    }
        //    public void setSize(Size size)
        //    {
        //        if (is_CorrectSize(size))
        //        {
        //            this.size = size;
        //            observers.Invoke(this, null);
        //        }
        //    }
        //    public void setP1(Point p)
        //    {
        //        if (is_CorrectPos(p))
        //        {
        //            p1 = p;
        //            observers.Invoke(this, null);
        //        }
        //    }
        //    public void setP2(Point p)
        //    {
        //        if (is_CorrectPos(p))
        //        {
        //            p2 = p;
        //            observers.Invoke(this, null);
        //        }
        //    }
        //    public void setP3(Point p)
        //    {
        //        if (is_CorrectPos(p))
        //        {
        //            p3 = p;
        //            observers.Invoke(this, null);
        //        }
        //    }

        //    public void getObject(Figure obj)
        //    {
        //        if (obj != null)
        //        {
        //            color = obj.getColor();
        //            size = obj.getSize();
        //            p1 = obj.getP1();
        //            p2 = obj.getP2();
        //            p3 = obj.getP3();
        //            observers.Invoke(this, null);
        //        }
        //    }
        //    public void destructor()
        //    {

        //    }
        //    public Model()
        //    {
        //        color = Color.Black;
        //        size = new Size(Properties.Settings.Default.width, Properties.Settings.Default.height);
        //        element = "";
        //        p1 = new Point(0, 0);
        //        p2 = new Point(0, 0);
        //        p3 = new Point(0, 0);
        //    }
        //}
        //public void UpdateFromModel(object sender, EventArgs e)
        //{
        //    currentColor = model.getColor();
        //    currentElement = model.getElement();

        //    numWdt.Value = model.getSize().Width;
        //    numHgh.Value = model.getSize().Height;
        //    numPosX.Value = model.getP1().X;
        //    numPosY.Value = model.getP1().Y;

        //    nump2X.Value = model.getP2().X;
        //    nump2Y.Value = model.getP2().Y;

        //    nump3X.Value = model.getP3().X;
        //    nump3Y.Value = model.getP3().Y;

        //    if (lvObj.SelectedItem != null)
        //    {
        //        (lvObj.SelectedItem as Figure).setColor(currentColor);
        //        (lvObj.SelectedItem as Figure).setSize(new Size((int)numWdt.Value, (int)numHgh.Value));
        //        (lvObj.SelectedItem as Figure).setP1(new Point((int)numPosX.Value, (int)numPosY.Value));
        //        (lvObj.SelectedItem as Figure).setP2(new Point((int)nump2X.Value, (int)nump2Y.Value));
        //        (lvObj.SelectedItem as Figure).setP3(new Point((int)nump3X.Value, (int)nump3Y.Value));
        //    }

        //}


        public class Figure
        {
            protected Point p1;
            protected Point p2;
            protected int thickness;
            protected Color color;

            protected bool is_focused = false;
            public Figure(Point p1, Point p2, int thickness, Color color, Graphics grObj, bool allow_reverse)
            {
                this.thickness = thickness;
                this.color = color;
                if (allow_reverse)
                {
                    if (p1.X < p2.X)                            // if we don't painting as the default left-right, up-down style
                    {
                        if (p1.Y < p2.Y)
                        {
                            this.p1 = p1;
                            this.p2 = p2;
                        }
                        else
                        {
                            this.p1 = new Point(p1.X, p2.Y);
                            this.p2 = new Point(p2.X, p1.Y);
                        }
                    }
                    else
                    {
                        if (p1.Y > p2.Y)
                        {
                            this.p1 = p2;
                            this.p2 = p1;
                        }
                        else
                        {
                            this.p1 = new Point(p2.X, p1.Y);
                            this.p2 = new Point(p1.X, p2.Y);
                        }
                    }
                }
                else
                {
                    this.p1 = p1;
                    this.p2 = p2;
                }
                if (grObj != null)              // grObj == null means we don't want to paint the object from the base constructor
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
            public Section(Point p1, Point p2, int thickness, Color color, Graphics grObj)
                : base(p1, p2, thickness, color, grObj, false)
            {
            }
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawLine(new Pen(Color.Violet, thickness), p1, p2);
                else
                    grObj.DrawLine(new Pen(color, thickness), p1, p2);
            }
        }
        public class Ellipse : Figure
        {
            public Ellipse(Point p1, Point p2, int thickness, Color col, Graphics grObj)
                : base(p1, p2, thickness, col, grObj, true)
            {
            }
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawEllipse(new Pen(Color.Violet, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                else
                    grObj.DrawEllipse(new Pen(color, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
            }

        }
        public class Rect: Figure
        {
            public Rect(Point p1, Point p2, int thickness, Color col, Graphics grObj)
                : base(p1, p2, thickness, col, grObj, true)
            {
            }
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawRectangle(new Pen(Color.Violet, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                else
                    grObj.DrawRectangle(new Pen(color, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
            }
        }
        public class Triangle : Figure
        {
            Point p3;
            public Triangle(Point p1, Point p2, Point p3, int thickness, Color col, Graphics grObj)
            : base(p1, p2, thickness, col, null, false)
            {
                this.p3 = p3;
                paint(grObj);
            }
            public override void paint(Graphics grObj)
            {
                if (is_focused)
                    grObj.DrawPolygon(new Pen(Color.Violet, thickness), new Point[] { p1, p2, p3 });
                else
                    grObj.DrawPolygon(new Pen(color, thickness), new Point[] { p1, p2, p3 });
            }

            public Point getP3()
            {
                return p3;
            }
            public void setP3(Point p3)
            {
                this.p3 = p3;
                ActiveForm.Invalidate();
            }
        }

        public class Storage
        {
            protected Figure[] storage = new Figure[0];
            public void add(Figure obj)
            {
                Figure[] temp = new Figure[storage.Length];
                for (int i = 0; i < temp.Length; i++)
                    temp[i] = storage[i];

                storage = new Figure[temp.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                    storage[i] = temp[i];

                storage[storage.Length - 1] = obj;
            }
            virtual public void remove(Figure obj)
            {
                int i = 0;
                for ( ; i < storage.Length; i++)
                    if (storage[i] == obj)
                        break;

                Figure[] temp = new Figure[storage.Length - 1];
                int j = 0;
                while (j != i)
                {
                    temp[j] = storage[j];
                    j++;
                }
                j = j + 1;
                for (; j < storage.Length; j++)
                    temp[j - 1] = storage[j];

                storage = new Figure[temp.Length];
                for (i = 0; i < temp.Length; i++)
                    storage[i] = temp[i];
            }
        }
        public class StorageService : Storage
        {
            Figure selected;
            public void add(Figure obj, Graphics gtObj, ListBox lb)
            {
                unfocus();
                base.add(obj);
                lb.Items.Add(obj);
                lb.SelectedItem = obj;
                focus(obj);
                ActiveForm.Invalidate();
            }
            public void remove(Figure obj, ListBox lb)
            {
                base.remove(obj);
                lb.Items.Remove(obj);
                ActiveForm.Invalidate();
            }
            public void removeAll(ListBox lb)
            {
                storage = new Figure[0];
                lb.Items.Clear();
                ActiveForm.Invalidate();
            }
            public void paint(Graphics grObj)
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

        ////
        ////classes are done
        ////
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            canvas.Invalidate();
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            grObj = canvas.CreateGraphics();
            storage.paint(grObj);
        }
        
        public void btnClick()
        {
            mp1 = new Point(-1, -1);
            mp2 = new Point(-1, -1);
            mp3 = new Point(-1, -1);
        }
        private void btnSctn_Click(object sender, EventArgs e)
        {
            //model.setElement((sender as Button).Name);
            currentElement = "btnSctn";
            btnClick();
        }

        private void btnArw_Click(object sender, EventArgs e)
        {
            //model.setElement((sender as Button).Name);
            currentElement = "btnArw";
            btnClick();
        }

        private void btnElps_Click(object sender, EventArgs e)
        {
            //model.setElement((sender as Button).Name);
            currentElement = "btnElps";
            btnClick();
        }

        private void btnTrn_Click(object sender, EventArgs e)
        {
            //model.setElement((sender as Button).Name);
            currentElement = "btnTrn";
            btnClick();
        }

        private void btnRct_Click(object sender, EventArgs e)
        {
            //model.setElement((sender as Button).Name);
            currentElement = "btnRct";
            btnClick();
        }


        private void btnBlck_Click(object sender, EventArgs e)
        {
            //model.setColor((sender as Button).BackColor);
            currentColor = Color.Black;
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            //model.setColor((sender as Button).BackColor);
            currentColor = Color.Blue;
        }

        private void btnGrn_Click(object sender, EventArgs e)
        {
            //model.setColor((sender as Button).BackColor);
            currentColor = Color.ForestGreen;
        }

        private void size_ValueChanged(object sender, EventArgs e)
        {
            //model.setSize(new Size((int)numWdt.Value, (int)numHgh.Value));
        }

        private void numP1_ValueChanged(object sender, EventArgs e)
        {
            //model.setP1(new Point((int)numPosX.Value, (int)numPosY.Value));
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            if (currentElement == "btnSctn")
            {
                if (mp1.X == -1)
                {
                    mp1 = mousePos;
                }
                else
                {
                    mp2 = mousePos;
                    storage.add(new Section(mp1, mp2, (int)numThck.Value, currentColor, grObj), grObj, lvObj);
                    btnClick();
                }
            }
            else if (currentElement == "btnElps")
            {
                if (mp1.X == -1)
                {
                    mp1 = mousePos;
                }
                else
                {
                    mp2 = mousePos;
                    storage.add(new Ellipse(mp1, mp2, (int)numThck.Value, currentColor, grObj), grObj, lvObj);
                    btnClick();
                }
            }
            else if (currentElement == "btnTrn")
            {
                if (mp1.X == -1)
                {
                    mp1 = mousePos;
                }
                else if (mp2.X == -1)
                {
                    mp2 = mousePos;
                }
                else
                {
                    mp3 = mousePos;
                    storage.add(new Triangle(mp1, mp2, mp3, (int)numThck.Value, currentColor, grObj), grObj, lvObj);
                    btnClick();
                }
            }
            else if (currentElement == "btnRct")
            {
                if (mp1.X == -1)
                {
                    mp1 = mousePos;
                }
                else
                {
                    mp2 = mousePos;
                    storage.add(new Rect(mp1, mp2, (int)numThck.Value, currentColor, grObj), grObj, lvObj);
                    btnClick();
                }
            }
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
            //model.destructor();
        }

        private void btnTrsh_Click(object sender, EventArgs e)
        {
            storage.remove(lvObj.SelectedItem as Figure, lvObj);
        }

        private void lvObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            storage.unfocus();
            //model.getObject(lvObj.SelectedItem as Figure);
            storage.focus(lvObj.SelectedItem as Figure);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            storage.removeAll(lvObj);
        }
    }
}