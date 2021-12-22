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
            Color color;
            protected Pen defaultPen;
            protected Pen focusedPen = new Pen(Color.Violet, penWidth);
            protected bool is_focused = false;
            
            protected Point p1;
            protected Size size;

            public void setWidth(int width)
            {
                size.Width = width;
                ActiveForm.Invalidate();
            }
            public void setHeight(int height)
            {
                size.Height = height;
                ActiveForm.Invalidate();
            }
            public void setX(int x)
            {
                p1.X = x;
                ActiveForm.Invalidate();
            }
            public void setY(int y)
            {
                p1.Y = y;
                ActiveForm.Invalidate();
            }
            public int getWidth()
            {
                return size.Width;
            }
            public int getHeight()
            {
                return size.Height;
            }
            public int getX()
            {
                return p1.X;
            }
            public int getY()
            {
                return p1.Y;
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
                p2 = new Point(p1.X + size.Width, p1.Y + size.Height);
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
                rect = new Rectangle(p1, size);
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
                rect = new Rectangle(p1, size);
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
                p2 = new Point(p1.X + size.Width, p1.Y + size.Height);
                p3 = new Point(p1.X + (p2.X - p1.X) / 2, p1.Y + (p2.Y + p1.Y) / 2);
                points = new Point[] { p1, p2, p3 };
                if (is_focused)
                    grObj.DrawPolygon(focusedPen, points);
                else
                    grObj.DrawPolygon(defaultPen, points);
            }
            public Triangle(int x1, int y1, int width, int height, Color col, Graphics grObj)
            : base(x1, y1, width, height, col)
            {
                p2 = new Point (p1.X + width, p1.Y + height);
                p3 = new Point(p1.X + (p2.X - p1.X) / 2, p1.Y + (p2.Y + p1.Y) / 2);
                points = new Point[] { p1, p2, p3 };
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
            public void setWidth(Figure obj, int width)
            {
                obj.setWidth(width);
            }
            public void setHeight(Figure obj, int height)
            {
                obj.setHeight(height);
            }
            public void setX(Figure obj, int x)
            {
                obj.setX(x);
            }
            public void setY(Figure obj, int y)
            {
                obj.setY(y);
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

            public bool is_CorrectSize(int value)
            {
                if ((value >= 10) && value <= 500)
                    return true;
                else
                    return false;
            }
            public bool is_CorrectPos(int value)
            {
                if (value >= 0 && value <= 500)
                    return true;
                else
                    return false;
            }
            public void setWidth(int value)
            {
                if (is_CorrectSize(value))
                {
                    width = value;
                    observers.Invoke(this, null);
                }
            }
            public void setHeight(int value)
            {
                if (is_CorrectSize(value))
                {
                    height = value;
                    observers.Invoke(this, null);
                }
            }
            public void setX(int value)
            {
                if (is_CorrectPos(value))
                {
                    x = value;
                    observers.Invoke(this, null);
                }
            }
            public void setY(int value)
            {
                if (is_CorrectPos(value))
                {
                    y = value;
                    observers.Invoke(this, null);
                }
            }

            public void getObject(Figure obj)
            {
                if (obj != null)
                {
                    width = obj.getWidth();
                    height = obj.getHeight();
                    x = obj.getX();
                    y = obj.getY();
                    observers.Invoke(this, null);
                }
            }

            public void destructor()
            {
                //Properties.Settings.Default.x = x;
                //Properties.Settings.Default.y = y;
                //Properties.Settings.Default.width = width;
                //Properties.Settings.Default.height = height;
                //Properties.Settings.Default.Save();
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

                x = Properties.Settings.Default.x;
                y = Properties.Settings.Default.y;
                width = Properties.Settings.Default.width;
                height = Properties.Settings.Default.height;
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