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
            model.observers += new EventHandler(UpdateFromModel);
            model.actions += new EventHandler(ActionsFromModel);
            model.moving += new EventHandler(MovingsFromModel);
            model.observers.Invoke(this, null);


            grObj = canvas.CreateGraphics();
        }


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
            }
            public void unfocus()
            {
                is_focused = false;
            }
            public void setP1(Point p)
            {
                p1 = p;
            }
            public void setP2(Point p)
            {
                p2 = p;
            }
            public void setThickness(int thickness)
            {
                this.thickness = thickness;
            }
            public void setColor(Color color)
            {
                this.color = color;
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
        public class Rect : Figure
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
                for (; i < storage.Length; i++)
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
                base.add(obj);
                unfocus();
                focus(obj);
                lb.Items.Add(obj);
                if (ActiveForm != null)
                    ActiveForm.Invalidate();
                lb.SelectedItem = obj;
            }
            public void remove(Figure obj, ListBox lb)
            {
                base.remove(obj);
                lb.Items.Remove(obj);
                lb.ClearSelected();
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
                    selected = obj;
                    selected.focus();
                    ActiveForm.Invalidate();
                }
            }
            public void unfocus()
            {
                if (selected != null)
                {
                    selected.unfocus();
                    ActiveForm.Invalidate();
                }
            }
        }

        ////
        ////objects are done
        ////

        public class Model
        {
            private Color color;
            private int thickness;
            private Point p1;
            private Point p2;
            private Point p3;

            private int canvasWidth;
            private int canvasHeight;

            private bool selected = false;

            public System.EventHandler observers;

            public bool obj_is_selected()
            {
                return selected;
            }
            public void unselect()
            {
                selected = false;
                mPosReset();
                observers.Invoke(this, null);
            }

            public bool is_CorrectPos(Point p)
            {
                if (p.X >= 1 && p.X <= canvasWidth && p.Y >= 1 && p.Y <= canvasHeight)
                    return true;
                else
                    return false;
            }
            public void setColor(Color color)
            {
                this.color = color;
                observers.Invoke(this, null);
            }
            public void setThickness(int thickness)
            {
                if (thickness > 0 && thickness <= 20)
                    this.thickness = thickness;
                observers.Invoke(this, null);
            }
            public void setP1(Point p)
            {
                if (is_CorrectPos(p))
                    p1 = p;

                observers.Invoke(this, null);
            }
            public void setP2(Point p)
            {
                if (is_CorrectPos(p))
                    p2 = p;

                observers.Invoke(this, null);
            }
            public void setP3(Point p)
            {
                if (is_CorrectPos(p))
                    p3 = p;

                observers.Invoke(this, null);
            }
            public void setSize(int width, int height)
            {
                Point p = new Point(p1.X + width, p1.Y + height);
                if (is_CorrectPos(p))
                    p2 = p;
                observers.Invoke(this, null);
            }
            public void setcanvWidth(int width)
            {
                if (ActiveForm != null)
                    if (width < ActiveForm.Size.Width && width >= 50)
                    {
                        canvasWidth = width;
                        ActiveForm.Invalidate();
                    }
                observers.Invoke(this, null);
            }
            public void setcanvHeight(int height)
            {
                if (ActiveForm != null)
                    if (height < ActiveForm.Size.Height && height >= 50)
                    {
                        canvasHeight = height;
                        ActiveForm.Invalidate();
                    }
                observers.Invoke(this, null);
            }

            public void setObject(Figure obj)
            {
                color = obj.getColor();
                thickness = obj.getThickness();
                p1 = obj.getP1();
                p2 = obj.getP2();
                if (obj is Triangle)
                    p3 = (obj as Triangle).getP3();

                selected = true;
                observers.Invoke(this, null);
            }
            public Color getColor()
            {
                return color;
            }
            public int getThickness()
            {
                return thickness;
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
            public int getCanvWidth()
            {
                return canvasWidth;
            }
            public int getCanvHeight()
            {
                return canvasHeight;
            }


            ////// clicked mousebuttons: creating/deleting

            public EventHandler actions;
            string btnName = "";
            Point mp1 = new Point(-1, -1);
            Point mp2 = new Point(-1, -1);
            Point mp3 = new Point(-1, -1);

            public void setBtn(string btnName)
            {
                unselect();
                this.btnName = btnName;
            }
            public Point getMp1()
            {
                return mp1;
            }
            public Point getMp2()
            {
                return mp2;
            }
            public Point getMp3()
            {
                return mp3;
            }
            public string getBtn()
            {
                return btnName;
            }
            public void mPosReset()
            {
                mp1 = new Point(-1, -1);
                mp2 = mp1;
                mp3 = mp1;
            }
            public void createObj(Point mouseP)
            {
                if (mp1.X == -1)
                {
                    mp1 = mouseP;
                }
                else if (mp2.X == -1)
                {
                    mp2 = mouseP;
                    if (btnName != "btnTrn")
                        actions.Invoke(this, null);
                }
                else
                {
                    mp3 = mouseP;
                    actions.Invoke(this, null);
                }
            }
            public void deleteObj()
            {
                btnName = "btnTrsh";
                actions.Invoke(this, null);
            }
            public void deleteAll()
            {
                btnName = "deleteAll";
                actions.Invoke(this, null);
            }

            ////// pressed keybuttons: moving

            public EventHandler moving;
            private string direction = "";

            public string getDirection()
            {
                return direction;
            }
            public void move(Keys code)
            {
                if (code == Keys.Left)
                    direction = "left";
                else if (code == Keys.Right)
                    direction = "right";
                else if (code == Keys.Up)
                    direction = "up";
                else if (code == Keys.Down)
                    direction = "down";

                else if (code == Keys.Oemplus)
                    direction = "sizeUp";
                else if (code == Keys.OemMinus)
                    direction = "sizeDown";

                moving.Invoke(this, null);
            }
            public void setPos(Point p1, Point p2, Point p3)
            {
                if (is_CorrectPos(p1))
                    this.p1 = p1;
                if (is_CorrectPos(p2))
                    this.p2 = p2;
                if (is_CorrectPos(p3))
                    this.p3 = p3;

                observers.Invoke(this, null);
            }
            public void destructor()
            {
                Properties.Settings.Default.thickness = thickness;
                Properties.Settings.Default.canvasWidth = canvasWidth;
                Properties.Settings.Default.canvasHeight = canvasHeight;
                Properties.Settings.Default.Save();
            }
            public Model()
            {
                color = Color.Black;
                thickness = Properties.Settings.Default.thickness;
                canvasWidth = Properties.Settings.Default.canvasWidth;
                canvasHeight = Properties.Settings.Default.canvasHeight;
                p1 = new Point(-1, -1);
                p2 = new Point(-1, -1);
                p3 = new Point(-1, -1);
            }
        }

        public void UpdateFromModel(object sender, EventArgs e)
        {
            numPosX.Maximum = model.getCanvWidth();
            nump2X.Maximum = model.getCanvWidth();
            nump3X.Maximum = model.getCanvWidth();
            numWdt.Maximum = model.getCanvWidth();
            numPosY.Maximum = model.getCanvHeight();
            nump2Y.Maximum = model.getCanvHeight();
            nump3Y.Maximum = model.getCanvHeight();
            numHgh.Maximum = model.getCanvHeight();

            numThck.ValueChanged -= new EventHandler(numThck_ValueChanged);
            numThck.Value = model.getThickness();
            numThck.ValueChanged += new EventHandler(numThck_ValueChanged);


            if (model.obj_is_selected() == false)
            {
                flpSz.Visible = false;
                flpP1.Visible = false;
                flpP2.Visible = false;
                flpP3.Visible = false;
                storage.unfocus();
                lvObj.ClearSelected();
                return;
            }
            else
            {
                flpP1.Visible = true;
                flpP2.Visible = true;
            }

            storage.unfocus();
            storage.focus(lvObj.SelectedItem as Figure);

            numPosX.ValueChanged -= new EventHandler(numP1_ValueChanged);
            numPosY.ValueChanged -= new EventHandler(numP1_ValueChanged);
            nump2X.ValueChanged -= new EventHandler(numP2_ValueChanged);
            nump2Y.ValueChanged -= new EventHandler(numP2_ValueChanged);

            numPosX.Value = model.getP1().X;
            numPosY.Value = model.getP1().Y;
            nump2X.Value = model.getP2().X;
            nump2Y.Value = model.getP2().Y;

            numPosX.ValueChanged += new EventHandler(numP1_ValueChanged);
            numPosY.ValueChanged += new EventHandler(numP1_ValueChanged);
            nump2X.ValueChanged += new EventHandler(numP2_ValueChanged);
            nump2Y.ValueChanged += new EventHandler(numP2_ValueChanged);

            if (model.getBtn() == "btnTrn")
            {
                nump3X.ValueChanged -= new EventHandler(numP3_ValueChanged);
                nump3Y.ValueChanged -= new EventHandler(numP3_ValueChanged);

                nump3X.Value = model.getP3().X;
                nump3Y.Value = model.getP3().Y;
                flpP3.Visible = true;

                nump3X.ValueChanged += new EventHandler(numP3_ValueChanged);
                nump3Y.ValueChanged += new EventHandler(numP3_ValueChanged);
            }
            else
            {
                flpP3.Visible = false;
            }

            if (model.getBtn() != "btnTrn" && model.getBtn() != "btnSctn")
            {
                numWdt.ValueChanged -= new EventHandler(size_ValueChanged);
                numHgh.ValueChanged -= new EventHandler(size_ValueChanged);

                numWdt.Value = Math.Abs(model.getP2().X - model.getP1().X);
                numHgh.Value = Math.Abs(model.getP2().Y - model.getP1().Y);

                numWdt.ValueChanged += new EventHandler(size_ValueChanged);
                numHgh.ValueChanged += new EventHandler(size_ValueChanged);
                flpSz.Visible = true;
            }
            else
            {
                flpSz.Visible = false;
            }

            if (lvObj.SelectedItem != null)
            {
                (lvObj.SelectedItem as Figure).setColor(model.getColor());
                (lvObj.SelectedItem as Figure).setThickness(model.getThickness());
                (lvObj.SelectedItem as Figure).setP1(model.getP1());
                (lvObj.SelectedItem as Figure).setP2(model.getP2());
                if (lvObj.SelectedItem is Triangle)
                    (lvObj.SelectedItem as Triangle).setP3(model.getP3());

            }
            if (ActiveForm != null)
                ActiveForm.Invalidate();
        }
        public void ActionsFromModel(object sender, EventArgs e)
        {
            string btn = model.getBtn();

            if (btn == "")
                return;

            if (btn == "btnSctn")
            {
                storage.add(new Section(model.getMp1(), model.getMp2(), model.getThickness(), model.getColor(), grObj), grObj, lvObj);
            }
            else if (btn == "btnElps")
            {
                storage.add(new Ellipse(model.getMp1(), model.getMp2(), model.getThickness(), model.getColor(), grObj), grObj, lvObj);
            }
            else if (btn == "btnTrn")
            {
                storage.add(new Triangle(model.getMp1(), model.getMp2(), model.getMp3(), model.getThickness(), model.getColor(), grObj), grObj, lvObj);
            }
            else if (btn == "btnRct")
            {
                storage.add(new Rect(model.getMp1(), model.getMp2(), model.getThickness(), model.getColor(), grObj), grObj, lvObj);
            }
            else if (btn == "btnTrsh")
            {
                storage.remove(lvObj.SelectedItem as Figure, lvObj);
                model.unselect();
            }
            else if (btn == "deleteAll")
            {
                storage.removeAll(lvObj);
                model.unselect();
            }
            model.mPosReset();
        }
        public void MovingsFromModel(object sender, EventArgs e)
        {
            string direction = model.getDirection();

            if (direction == "")
                return;

            Point p1 = model.getP1();
            Point p2 = model.getP2();
            Point p3 = model.getP3();

            if (direction == "left")
            {
                p1.X = p1.X - 1;
                p2.X = p2.X - 1;
                if (lvObj.SelectedItem is Triangle)
                    p3.X = p3.X - 1;
            }
            else if (direction == "right")
            {
                p1.X = p1.X + 1;
                p2.X = p2.X + 1;
                if (lvObj.SelectedItem is Triangle)
                    p3.X = p3.X + 1;
            }
            else if (direction == "up")
            {
                p1.Y = p1.Y - 1;
                p2.Y = p2.Y - 1;
                if (lvObj.SelectedItem is Triangle)
                    p3.Y = p3.Y - 1;
            }
            else if (direction == "down")
            {
                p1.Y = p1.Y + 1;
                p2.Y = p2.Y + 1;
                if (lvObj.SelectedItem is Triangle)
                    p3.Y = p3.Y + 1;
            }
            else if (direction == "sizeUp")
            {
                if (!(lvObj.SelectedItem is Triangle) && !(lvObj.SelectedItem is Section))
                {
                    p2.X = p2.X + 1;
                    p2.Y = p2.Y + 1;
                }
            }
            else if (direction == "sizeDown")
            {
                if (!(lvObj.SelectedItem is Triangle) && !(lvObj.SelectedItem is Section))
                {
                    p2.X = p2.X - 1;
                    p2.Y = p2.Y - 1;
                }
            }

            model.setPos(p1, p2, p3);
        }

        //model is done

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.destructor();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            canvas.Invalidate();
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            grObj = canvas.CreateGraphics();
            storage.paint(grObj);
        }
        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            model.createObj(mousePos);
        }


        private void btnArw_Click(object sender, EventArgs e)
        {
            model.unselect();
        }
        private void btnSctn_Click(object sender, EventArgs e)
        {
            model.setBtn((sender as Button).Name);
        }
        private void btnElps_Click(object sender, EventArgs e)
        {
            model.setBtn((sender as Button).Name);
        }
        private void btnTrn_Click(object sender, EventArgs e)
        {
            model.setBtn((sender as Button).Name);
        }
        private void btnRct_Click(object sender, EventArgs e)
        {
            model.setBtn((sender as Button).Name);
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
        private void numThck_ValueChanged(object sender, EventArgs e)
        {
            model.setThickness((int)(sender as NumericUpDown).Value);
        }



        private void size_ValueChanged(object sender, EventArgs e)
        {
            model.setSize((int)numWdt.Value, (int)numHgh.Value);
        }
        private void numP1_ValueChanged(object sender, EventArgs e)
        {
            model.setP1(new Point((int)numPosX.Value, (int)numPosY.Value));
        }
        private void numP2_ValueChanged(object sender, EventArgs e)
        {
            model.setP2(new Point((int)nump2X.Value, (int)nump2Y.Value));
        }
        private void numP3_ValueChanged(object sender, EventArgs e)
        {
            model.setP3(new Point((int)nump3X.Value, (int)nump3Y.Value));
        }


        private void lvObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObj.SelectedItem != null)
            {
                model.setObject(lvObj.SelectedItem as Figure);
                storage.focus(lvObj.SelectedItem as Figure);
            }
        }
        private void btnTrsh_Click(object sender, EventArgs e)
        {
            model.deleteObj();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            model.deleteAll();
        }
        private void lvObj_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            model.move(e.KeyCode);
            e.Handled = true;
        }
    }
}