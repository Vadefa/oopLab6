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

            grObj = canvas.CreateGraphics();
            storage = new StorageService(lvObj, grObj);
            model = new Model(storage, grObj);
            model.observers += new EventHandler(UpdateFromModel);
            model.observers.Invoke(this, null);

        }

        public abstract class AFigure
        {
            public abstract void setP1(Point p);
            public abstract void setP2(Point p);
            public abstract void setThickness(int thickness);
            public abstract void setColor(Color color);

            public abstract Point getP1();
            public abstract Point getP2();
            public abstract int getThickness();
            public abstract Color getColor();
            public abstract string getName();

            public abstract void focus();
            public abstract void unfocus();
            public abstract void paint(Graphics grObj);
        }
        public class Figure: AFigure
        {
            protected string name;
            protected Point p1;
            protected Point p2;
            protected int thickness;
            protected Color color;

            protected bool is_focused = false;

            protected Graphics grObj;
            public Figure(Point p1, Point p2, int thickness, Color color, Graphics grObj, bool allow_reverse)
            {
                name = "figure";
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
                {
                    this.grObj = grObj;
                    paint(grObj);
                }
            }
            public override void paint(Graphics grObj)
            {
            }
            public override void focus()
            {
                is_focused = true;
            }
            public override void unfocus()
            {
                is_focused = false;
            }
            public override void setP1(Point p)
            {
                p1 = p;
            }
            public override void setP2(Point p)
            {
                p2 = p;
            }
            public override void setThickness(int thickness)
            {
                this.thickness = thickness;
            }
            public override void setColor(Color color)
            {
                this.color = color;
            }
            public override Point getP1()
            {
                return p1;
            }
            public override Point getP2()
            {
                return p2;
            }
            public override int getThickness()
            {
                return thickness;
            }
            public override Color getColor()
            {
                return color;
            }
            public override string getName()
            {
                return name;
            }

        }
        public class Section : Figure
        {
            public Section(Point p1, Point p2, int thickness, Color color, Graphics grObj)
                : base(p1, p2, thickness, color, grObj, false)
            {
                name = "sctn";
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
                name = "elps";
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
                name = "rect";
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
                name = "trn";
                this.p3 = p3;
                this.grObj = grObj;
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
                if (ActiveForm != null)
                    ActiveForm.Invalidate();
            }
        }

        public class Storage
        {
            protected AFigure[] storage = new AFigure[0];
            public void add(AFigure obj)
            {
                AFigure[] temp = new AFigure[storage.Length];
                for (int i = 0; i < temp.Length; i++)
                    temp[i] = storage[i];

                storage = new AFigure[temp.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                    storage[i] = temp[i];

                storage[storage.Length - 1] = obj;
            }
            virtual public void remove(AFigure obj)
            {
                int i = 0;
                for (; i < storage.Length; i++)
                    if (storage[i] == obj)
                        break;

                AFigure[] temp = new AFigure[storage.Length - 1];
                int j = 0;
                while (j != i)
                {
                    temp[j] = storage[j];
                    j++;
                }
                j = j + 1;
                for (; j < storage.Length; j++)
                    temp[j - 1] = storage[j];

                storage = new AFigure[temp.Length];
                for (i = 0; i < temp.Length; i++)
                    storage[i] = temp[i];
            }
        }
        public class StorageService : Storage
        {
            AFigure selected;
            ListBox lb;
            Graphics grObj;
            public StorageService(ListBox lb, Graphics grObj)
            {
                this.lb = lb;
                this.grObj = grObj;
            }
            public new void add(AFigure obj)
            {
                if (obj is Group)
                {
                    remove();                   // deleting all selected objects from the storage, now they're in the group
                    base.add(obj);
                    lb.Items.Add(obj);
                    lb.SelectedItem = obj;
                }
                else
                {
                    base.add(obj);
                    unfocus();
                    focus(obj);

                    lb.ClearSelected();
                    lb.Items.Add(obj);
                    if (ActiveForm != null)
                        ActiveForm.Invalidate();
                    lb.SelectedItem = obj;
                }
            }
            public void remove()
            {
                if (lb.SelectedItem != null)
                {
                    //getting the collection of selected elements for deleting them from both storage and listbox
                    ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lb);
                    selectedItems = lb.SelectedItems;
                    
                    //using reverge step allows to delete them all, because after deleting sI.count reduces by 1
                    for(int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        remove(selectedItems[i] as AFigure);
                        lb.Items.Remove(selectedItems[i]);
                    }
                    //lb.ClearSelected();
                    if (ActiveForm != null)
                        ActiveForm.Invalidate();
                }
            }
            public void removeAll()
            {
                storage = new AFigure[0];
                lb.Items.Clear();
                ActiveForm.Invalidate();
            }
            public void paint()
            {
                foreach (AFigure f in storage)
                    f.paint(grObj);
            }
            public void focus(AFigure obj)
            {
                if (obj != null)
                {
                    selected = obj;
                    selected.focus();
                    if (ActiveForm != null)
                        ActiveForm.Invalidate();
                }
            }
            public void unfocus()
            {
                if (selected != null)
                {
                    selected.unfocus();
                    if (ActiveForm != null)
                        ActiveForm.Invalidate();
                }
            }
        }

        ////
        public class Group: AFigure
        {
            private int _maxcount;
            private int _count;
            private AFigure []_figures;
            private string _name;
            private Point p1;
            private Point p2;

            public Group(int maxcount)
            {
                _name = "group";
                _maxcount = maxcount;
                _count = 0;
                _figures = new AFigure[maxcount];       //all elements will be null thanks visual studio
                p1 = new Point(-1, -1);
                p2 = new Point(-1, -1);
            }
            public bool addFigure(AFigure figure)
            {
                if (_count >= _maxcount)
                    return false;
                else
                {
                    _count = _count + 1;
                    _figures[_count - 1] = figure;


                    if (figure.getP1().X < p1.X || p1.X == -1)
                        p1.X = figure.getP1().X;

                    if (figure.getP1().Y < p1.Y || p1.Y == -1)
                        p1.Y = figure.getP1().Y;


                    if (figure.getP2().X > p2.X)
                        p2.X = figure.getP2().X;

                    if (figure.getP2().Y > p2.Y)
                        p2.Y = figure.getP2().Y;

                    return true;
                }
            }
            public int getCount()
            {
                return _count;
            }
            public AFigure getFigure(int iter)
            {
                return _figures[iter];
            }

            // realization of methods

            public override void setP1(Point p)
            {
                Point shift = new Point(p.X - p1.X, p.Y - p1.Y);

                Point tempP = new Point();

                for (int i = 0; i < _count; i++)
                {
                    tempP = _figures[i].getP1();
                    tempP.X = tempP.X + shift.X;
                    tempP.Y = tempP.Y + shift.Y;
                    _figures[i].setP1(tempP);
                }
            }
            public override void setP2(Point p)
            {
                Point shift = new Point(p.X - p2.X, p.Y - p2.Y);

                Point tempP = new Point();

                for (int i = 0; i < _count; i++)
                {
                    tempP = _figures[i].getP2();
                    tempP.X = tempP.X + shift.X;
                    tempP.Y = tempP.Y + shift.Y;
                    _figures[i].setP2(tempP);
                }
            }
            public override void setThickness(int thickness) {

                foreach (AFigure figure in _figures)
                    figure.setThickness(thickness);
            }
            public override void setColor(Color color)
            {
                foreach (AFigure figure in _figures)
                    figure.setColor(color);
            }

            public override Point getP1()
            {
                return p1;
            }
            public override Point getP2()
            {
                return p2;
            }
            public override int getThickness()
            {
                return _figures[0].getThickness();
            }
            public override Color getColor()
            {
                return _figures[0].getColor();
            }
            public override string getName()
            {
                return _name;
            }

            public override void focus() {
                foreach (AFigure figure in _figures)
                    figure.focus();
            }
            public override void unfocus()
            {
                foreach (AFigure figure in _figures)
                    figure.unfocus();
            }
            public override void paint(Graphics grObj)
            {
                foreach (AFigure figure in _figures)
                    figure.paint(grObj);
            }


        }
        ////

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
            private Graphics grObj;
            private StorageService storage;
            private AFigure obj;

            private int canvasWidth;
            private int canvasHeight;

            private bool selected = false;

            private int count;

            public System.EventHandler observers;

            public bool obj_is_selected()
            {
                return selected;
            }
            public void unselect()
            {
                selected = false;
                objName = "";
                mPosReset();
                observers.Invoke(this, null);
            }

            public bool is_CorrectPos(Point p)
            {
                if (p.X >= 0 && p.X + thickness <= canvasWidth && p.Y >= 0 && p.Y + thickness <= canvasHeight)
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

            public void setObject(AFigure obj, int count, ListBox lb, EventHandler handler)
            {
                this.count = count;
                setObjName(obj.getName());
                if (count > 1)
                {
                    Group g = new Group(count);
                    foreach (object o in lb.SelectedItems)
                    {
                        g.addFigure(o as AFigure);
                    }
                    lb.SelectedIndexChanged -= new EventHandler(handler);
                    storage.remove();                                           // removed all sected object from storage
                    storage.add(g);                                             // and added them as a group
                    lb.SelectedIndexChanged += new EventHandler(handler);       /* handlers are calling when we using storage.add, but we
                                                                                   don't need it */ 
                    this.obj = g;
                    color = g.getColor();
                    thickness = g.getThickness();
                    p1 = g.getP1();
                    p2 = g.getP2();
                }
                else
                {
                    this.obj = obj;
                    color = obj.getColor();
                    thickness = obj.getThickness();
                    p1 = obj.getP1();
                    p2 = obj.getP2();
                    if (obj is Triangle)
                        p3 = (obj as Triangle).getP3();
                }
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

            string objName = "";
            Point mp1 = new Point(-1, -1);
            Point mp2 = new Point(-1, -1);
            Point mp3 = new Point(-1, -1);

            public void setObjName(string objName)
            {
                this.objName = objName;
            }
            public void setNameFromBtn(string btnName)
            {
                unselect();
                objName = btnName;
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
            public string getObjName()
            {
                return objName;
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
                    if (objName != "trn")
                    {
                        if (objName == "sctn")
                        {
                            storage.add(new Section(mp1, mp2, thickness, color, grObj));
                        }
                        else if (objName == "elps")
                        {
                            storage.add(new Ellipse(mp1, mp2, thickness, color, grObj));
                        }
                        else if (objName == "rect")
                        {
                            storage.add(new Rect(mp1, mp2, thickness, color, grObj));
                        }
                        mPosReset();
                    }

                }
                else
                {
                    mp3 = mouseP;
                    if (objName == "trn")
                    {
                        storage.add(new Triangle(mp1, mp2, mp3, thickness, color, grObj));
                        mPosReset();
                    }
                }
            }
            public void deleteObj()
            {
                if (selected)
                {
                    storage.remove();
                    selected = false;
                    observers.Invoke(this, null);
                }
            }
            public void deleteAll()
            {
                storage.removeAll();
                selected = false;
                observers.Invoke(this, null);
            }


            ////// pressed keybuttons: moving

            
            public void move(Keys code)
            {
                Point p1 = this.p1;
                Point p2 = this.p2;
                Point p3 = this.p3;

                if (code == Keys.Left)
                {
                    p1.X = p1.X - 1;
                    p2.X = p2.X - 1;
                    if (obj is Triangle)
                        p3.X = p3.X - 1;
                    setPos(p1, p2, p3);
                }
                else if (code == Keys.Right)
                {
                    p1.X = p1.X + 1;
                    p2.X = p2.X + 1;
                    if (obj is Triangle)
                        p3.X = p3.X + 1;
                    setPos(p1, p2, p3);
                }
                else if (code == Keys.Up)
                {
                    p1.Y = p1.Y - 1;
                    p2.Y = p2.Y - 1;
                    if (obj is Triangle)
                        p3.Y = p3.Y - 1;
                    setPos(p1, p2, p3);

                }
                else if (code == Keys.Down)
                {
                    p1.Y = p1.Y + 1;
                    p2.Y = p2.Y + 1;
                    if (obj is Triangle)
                        p3.Y = p3.Y + 1;
                    setPos(p1, p2, p3);
                }

                else if (code == Keys.Oemplus && !(obj is Triangle) && !(obj is Section))
                {
                    p2.X = p1.X + Math.Abs(p2.X - p1.X) + 1;
                    p2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) + 1;
                    setP2(p2);
                }
                else if (code == Keys.OemMinus && !(obj is Triangle) && !(obj is Section))
                {
                    p2.X = p1.X + Math.Abs(p2.X - p1.X) - 1;
                    p2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) - 1;
                    setP2(p2);
                }
                else if (code == Keys.Delete)
                    deleteObj();
            }
            public void setPos(Point p1, Point p2, Point p3)
            {
                if (is_CorrectPos(p1) && is_CorrectPos(p2) && objName != "trn")
                {
                    this.p1 = p1;
                    this.p2 = p2;
                }
                
                if (is_CorrectPos(p1) && is_CorrectPos(p2) && is_CorrectPos(p3) && objName == "trn")
                {
                    this.p1 = p1;
                    this.p2 = p2;
                    this.p3 = p3;
                }
                observers.Invoke(this, null);
            }
            public void destructor()
            {
                Properties.Settings.Default.thickness = thickness;
                Properties.Settings.Default.canvasWidth = canvasWidth;
                Properties.Settings.Default.canvasHeight = canvasHeight;
                Properties.Settings.Default.Save();
            }
            public Model(StorageService storage, Graphics grObj)
            {
                color = Color.Black;
                thickness = Properties.Settings.Default.thickness;
                canvasWidth = Properties.Settings.Default.canvasWidth;
                canvasHeight = Properties.Settings.Default.canvasHeight;
                p1 = new Point(-1, -1);
                p2 = new Point(-1, -1);
                p3 = new Point(-1, -1);
                this.storage = storage;
                this.grObj = grObj;
            }
        }

        public void UpdateFromModel(object sender, EventArgs e)
        {
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



            storage.unfocus();
            storage.focus(lvObj.SelectedItem as AFigure);


            if (model.getObjName() == "sctn" || model.getObjName() == "trn" || model.getObjName() == "group")
            {
                numPosX.ValueChanged -= new EventHandler(numP1_ValueChanged);
                numPosY.ValueChanged -= new EventHandler(numP1_ValueChanged);
                nump2X.ValueChanged -= new EventHandler(numP2_ValueChanged);
                nump2Y.ValueChanged -= new EventHandler(numP2_ValueChanged);

                numPosX.Value = model.getP1().X;
                numPosY.Value = model.getP1().Y;
                nump2X.Value = model.getP2().X;
                nump2Y.Value = model.getP2().Y;

                nump2X.ValueChanged += new EventHandler(numP2_ValueChanged);
                nump2Y.ValueChanged += new EventHandler(numP2_ValueChanged);
                numPosX.ValueChanged += new EventHandler(numP1_ValueChanged);
                numPosY.ValueChanged += new EventHandler(numP1_ValueChanged);

                flpP1.Visible = true;
                flpP2.Visible = true;
                flpSz.Visible = false;

                if (model.getObjName() == "trn")
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
                    flpP3.Visible = false;
            }
            else
            {
                numWdt.ValueChanged -= new EventHandler(size_ValueChanged);
                numHgh.ValueChanged -= new EventHandler(size_ValueChanged);

                numWdt.Value = Math.Abs(model.getP2().X - model.getP1().X);
                numHgh.Value = Math.Abs(model.getP2().Y - model.getP1().Y);

                numWdt.ValueChanged += new EventHandler(size_ValueChanged);
                numHgh.ValueChanged += new EventHandler(size_ValueChanged);

                flpP1.Visible = false;
                flpP2.Visible = false;
                flpP3.Visible = false;
                flpSz.Visible = true;
            }

            if (lvObj.SelectedItem != null)
            {
                (lvObj.SelectedItem as AFigure).setColor(model.getColor());
                (lvObj.SelectedItem as AFigure).setThickness(model.getThickness());
                (lvObj.SelectedItem as AFigure).setP1(model.getP1());
                (lvObj.SelectedItem as AFigure).setP2(model.getP2());
                if (lvObj.SelectedItem is Triangle)
                    (lvObj.SelectedItem as Triangle).setP3(model.getP3());

            }
            this.Invalidate();
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
            storage.paint();
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
            model.setNameFromBtn((sender as Button).Name);
        }
        private void btnElps_Click(object sender, EventArgs e)
        {
            model.setNameFromBtn((sender as Button).Name);
        }
        private void btnTrn_Click(object sender, EventArgs e)
        {
            model.setNameFromBtn((sender as Button).Name);
        }
        private void btnRct_Click(object sender, EventArgs e)
        {
            model.setNameFromBtn((sender as Button).Name);
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
                model.setObject(lvObj.SelectedItem as AFigure, lvObj.SelectedItems.Count, sender as ListBox, lvObj_SelectedIndexChanged);
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