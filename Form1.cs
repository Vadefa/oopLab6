using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D; // for GraphicPath
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;    //for writing&reaeding files

namespace oopLab6
{
    public partial class Form1 : Form
    {
        Graphics grObj;
        StorageService storage;
        Model model;
        TreeObserver tree;

        Keys pressedKey;


        public Form1()
        {
            InitializeComponent();

            grObj = canvas.CreateGraphics();

            storage = new StorageService(grObj);
            tree = new TreeObserver(treeView1, treeView1_AfterSelect, storage);
            storage.addObserver(tree);
            
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
            public abstract bool is_undermouse(Point mouseP);


            //for triangle:
            public abstract void setP3(Point p);
            public abstract Point getP3();

            //for opeartions:
            public abstract void focus();
            public abstract void unfocus();
            public abstract bool is_inFocus();
            public abstract void paint(Graphics grObj);
            public abstract bool is_correctPos(Point p);
            public abstract void move(Point shift);

            //for save&load:
            public abstract void save(StreamWriter sw);
            public abstract void load(StreamReader sr);

            //all figures should observe for sticky figures so I thought I can put this method in there
            public abstract bool onSubjectIntersects(AFigure sticky);
            public abstract void onSubjectMoved(AFigure sticky, Point shift);
        }

        public abstract class SingleObserver
        {
            public abstract void onSubjectChanged();
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
            protected GraphicsPath grPath;

            protected int maxPosX;
            protected int maxPosY;

            public Figure(Point p1, Point p2, int thickness, Color color, Graphics grObj, bool allow_reverse, int canvasWidth, int canvasHeight)
            {
                name = "figure";
                this.thickness = thickness;
                this.color = color;
                grPath = new GraphicsPath();
                maxPosX = canvasWidth;
                maxPosY = canvasHeight;
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
            public override bool is_correctPos(Point p)
            {
                if (p.X >= 0 && p.X + thickness <= maxPosX && p.Y >= 0 && p.Y + thickness <= maxPosX)
                    return true;
                else
                    return false;
            }
            public override void move(Point shift)
            {
                Point tp1 = new Point();
                Point tp2 = new Point();


                tp1.X = p1.X + shift.X;
                tp1.Y = p1.Y + shift.Y;

                tp2.X = p2.X + shift.X;
                tp2.Y = p2.Y + shift.Y;

                //p1.X = p1.X + shift.X;
                //p1.Y = p1.Y + shift.Y;

                //p2.X = p2.X + shift.X;
                //p2.Y = p2.Y + shift.Y;

                if (is_correctPos(tp1) && is_correctPos(tp2))
                    {
                        p1 = tp1;
                        p2 = tp2;
                    }
            }
            public override void focus()
            {
                is_focused = true;
            }
            public override void unfocus()
            {
                is_focused = false;
            }
            public override bool is_inFocus()
            {
                return is_focused;
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
            public override bool is_undermouse(Point mouseP)
            {
                //checking the contour || checking the internal part of the object
                if (grPath.IsOutlineVisible(mouseP, new Pen(color, thickness)) || grPath.IsVisible(mouseP))
                    return true;
                else
                    return false;
            }


            //for triangle:
            public override void setP3(Point p) { }
            public override Point getP3()
            {
                return new Point(1, 1);
            }

            //for save&load:
            public override void save(StreamWriter sw)
            {
                try
                {
                    sw.WriteLine(getName());
                    sw.WriteLine(p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString() + " " + maxPosX.ToString() + " " + maxPosY.ToString());
                    string col = color.ToString();
                    col = col.Remove(0, 7);
                    col = col.Remove(col.LastIndexOf(']'));
                    sw.WriteLine(col + " " + thickness.ToString());
                }
                catch
                {
                    MessageBox.Show("We got the problem of saving objects");
                }
            }
            public override void load(StreamReader sr)
            {
                try
                {
                    string[] cords = sr.ReadLine().Split();
                    p1 = new Point(int.Parse(cords[0]), int.Parse(cords[1]));
                    p2 = new Point(int.Parse(cords[2]), int.Parse(cords[3]));
                    maxPosX = int.Parse(cords[4]);
                    maxPosY = int.Parse(cords[5]);

                    string[] props = sr.ReadLine().Split();
                    color = Color.FromName(props[0]);
                    thickness = int.Parse(props[1]);

                }
                catch
                {
                    MessageBox.Show("Troubles with loading an object");
                }
            }

            //as observer:
            public override bool onSubjectIntersects(AFigure sticky)
            {
                RectangleF borders = grPath.GetBounds();
                if (borders.IntersectsWith((sticky as Sticky).getGtPath().GetBounds()))
                {
                    return true;
                }
                else
                    return false;
            }
            public override void onSubjectMoved(AFigure sticky, Point shift)
            {
                move(shift);
            }
        }
        public class Section : Figure
        {
            public Section(Point p1, Point p2, int thickness, Color color, Graphics grObj, int canvasWidth, int canvasHeight)
                : base(p1, p2, thickness, color, grObj, false, canvasWidth, canvasHeight)
            {
                name = "sctn";
            }
            public override void paint(Graphics grObj)
            {
                //if (is_focused)
                //    grObj.DrawLine(new Pen(Color.Violet, thickness), p1, p2);
                //else
                //    grObj.DrawLine(new Pen(color, thickness), p1, p2);
                grPath.Dispose();
                grPath = new GraphicsPath();
                grPath.AddLine(p1, p2);
                if (is_focused)
                    grObj.DrawPath(new Pen(Color.Violet, thickness), grPath);
                else
                    grObj.DrawPath(new Pen(color, thickness), grPath);
            }
        }
        public class Ellipse : Figure
        {
            public Ellipse(Point p1, Point p2, int thickness, Color col, Graphics grObj, int canvasWidth, int canvasHeight)
                : base(p1, p2, thickness, col, grObj, true, canvasWidth, canvasHeight)
            {
                name = "elps";
            }
            public override void paint(Graphics grObj)
            {
                //if (is_focused)
                //    grObj.DrawEllipse(new Pen(Color.Violet, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                //else
                //    grObj.DrawEllipse(new Pen(color, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                grPath.Dispose();
                grPath = new GraphicsPath();
                grPath.AddEllipse(new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                if (is_focused)
                    grObj.DrawPath(new Pen(Color.Violet, thickness), grPath);
                else
                    grObj.DrawPath(new Pen(color, thickness), grPath);
            }

        }
        public class Rect : Figure
        {
            public Rect(Point p1, Point p2, int thickness, Color col, Graphics grObj, int canvasWidth, int canvasHeight)
                : base(p1, p2, thickness, col, grObj, true, canvasWidth, canvasHeight)
            {
                name = "rect";
            }
            public override void paint(Graphics grObj)
            {
                //if (is_focused)
                //    grObj.DrawRectangle(new Pen(Color.Violet, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                //else
                //    grObj.DrawRectangle(new Pen(color, thickness), new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                grPath.Dispose();
                grPath = new GraphicsPath();
                grPath.AddRectangle(new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                if (is_focused)
                    grObj.DrawPath(new Pen(Color.Violet, thickness), grPath);
                else
                    grObj.DrawPath(new Pen(color, thickness), grPath);
            }
        }
        public class Triangle : Figure
        {
            Point p3;
            public Triangle(Point p1, Point p2, Point p3, int thickness, Color col, Graphics grObj, int canvasWidth, int canvasHeight)
            : base(p1, p2, thickness, col, null, false, canvasWidth, canvasHeight)
            {
                name = "trn";
                this.p3 = p3;
                this.grObj = grObj;
                paint(grObj);
            }
            public override void paint(Graphics grObj)
            {
                //if (is_focused)
                //    grObj.DrawPolygon(new Pen(Color.Violet, thickness), new Point[] { p1, p2, p3 });
                //else
                //    grObj.DrawPolygon(new Pen(color, thickness), new Point[] { p1, p2, p3 });
                grPath.Dispose();
                grPath = new GraphicsPath();
                grPath.AddPolygon(new Point[] { p1, p2, p3 });
                if (is_focused)
                    grObj.DrawPath(new Pen(Color.Violet, thickness), grPath);
                else
                    grObj.DrawPath(new Pen(color, thickness), grPath);
            }
            public override void move(Point shift)
            {
                base.move(shift);
                p3.X = p3.X + shift.X;
                p3.Y = p3.Y + shift.Y;
            }

            public override Point getP3()
            {
                return p3;
            }
            public override void setP3(Point p3)
            {
                this.p3 = p3;
                if (ActiveForm != null)
                    ActiveForm.Invalidate();
            }


            // save && load:
            public override void save(StreamWriter sw)
            {
                base.save(sw);
                try
                {
                    sw.WriteLine(p3.X.ToString() + " " + p3.Y.ToString());
                }
                catch
                {
                    MessageBox.Show("We got the problem of saving objects");
                }
            }
            public override void load(StreamReader sr)
            {
                base.load(sr);
                try
                {
                    string[] cords3 = sr.ReadLine().Split();
                    p3 = new Point(int.Parse(cords3[0]), int.Parse(cords3[1]));
                }
                catch
                {
                    MessageBox.Show("Troubles with loading the coordinate of the third point of a triangle");
                }
            }
        }

        public class Sticky: Figure
        {
            private List<AFigure> _observers;
            private List<AFigure> _intersecters;
           
            public Sticky(Point p1, Point p2, int thickness, Color col, Graphics grObj, int canvasWidth, int canvasHeight)
                            : base(p1, p2, thickness, col, grObj, true, canvasWidth, canvasHeight)
            {
                name = "sticky";
                _observers = new List<AFigure>();
                _intersecters = new List<AFigure>();
            }
            public override void move(Point shift)
            {
                base.move(shift);
                observersInvoke(shift);
            }
            public override void paint(Graphics grObj)
            {
                grPath.Dispose();
                grPath = new GraphicsPath();
                grPath.AddRectangle(new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))));
                if (is_focused)
                {
                    grObj.DrawPath(new Pen(Color.Black, thickness), grPath);
                    grObj.FillPath(new SolidBrush(Color.Violet), grPath);
                }
                else
                {
                    grObj.DrawPath(new Pen(Color.Black, thickness), grPath);
                    grObj.FillPath(new SolidBrush(color), grPath);
                }
            }
            public GraphicsPath getGtPath()
            {
                return grPath;
            }
            public void addObserver(AFigure figure)
            {
                _observers.Add(figure);
            }
            public void addIntersecter(AFigure figure)
            {
                _intersecters.Add(figure);
            }
            public void removeObserver(AFigure figure)
            {
                _observers.Remove(figure);
            }
            public void removeIntersecter(AFigure figure)
            {
                _intersecters.Remove(figure);
            }
            public bool contains_Observer(AFigure figure)
            {
                if (_observers.Contains(figure))
                    return true;
                else
                    return false;
            }
            public bool contains_Intersecter(AFigure figure)
            {
                    return _intersecters.Contains(figure);
            }
            public AFigure getObserver(int index)
            {
                return (_observers[index]);
            }
            public AFigure getIntersecter(int index)
            {
                return _intersecters[index];
            }
            public int getObsCount()
            {
                return _observers.Count;
            }
            public int getIntersCount()
            {
                return _intersecters.Count;
            }

            public void observersInvoke(Point shift)
            {
                _intersecters = new List<AFigure>();
                foreach (AFigure observer in _observers)
                    if (observer.onSubjectIntersects(this))
                        _intersecters.Add(observer);

                foreach (AFigure intersecter in _intersecters)
                {
                        intersecter.onSubjectMoved(this, shift);
                }

            }


            //if intersected with another sticky object
            public override void onSubjectMoved(AFigure sticky, Point shift)
            {
                //the sticky object that was moved this object should not recursively move after this object will be moved.
                removeObserver(sticky);

                //also both their intersectors should not be moved twice.
                int count = (sticky as Sticky).getIntersCount();
                for(int i = 0; i < count; i++)
                {
                    if (contains_Intersecter((sticky as Sticky).getIntersecter(i)))
                        removeObserver((sticky as Sticky).getIntersecter(i));
                }
                move(shift);

                //now return everything like it was before
                addObserver(sticky);
                for (int i = 0; i < count; i++)
                {
                    if (contains_Intersecter((sticky as Sticky).getIntersecter(i)))
                        addObserver((sticky as Sticky).getIntersecter(i));
                }
            }
        }

        public class Storage
        {
            protected AFigure[] storage = new AFigure[0];
            virtual public void add(AFigure obj)
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
                while (j != i && j < storage.Length - 1)
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
            int selectedIndex;
            Graphics grObj;
            TreeObserver tree;
            public StorageService(Graphics grObj)
            {
                this.grObj = grObj;
                selectedIndex = -1;
            }
            public override void add(AFigure obj)
            {
                foreach (AFigure figure in storage)
                {
                    if (figure is Sticky)
                        (figure as Sticky).addObserver(obj);
                }

                if (obj is Group)
                {
                    foreach (AFigure figure in storage)
                    {
                        if (figure is Sticky)
                            for (int i = 0; i < (obj as Group).getCount(); i++)
                                (figure as Sticky).removeObserver((obj as Group).getFigure(i));
                        // if we don't do this, our group objects will move twice if sticky moved

                        //remove(obj);                    // deleting selected object from the storage, now it is in the group
                    }
                }
                else if (obj is Sticky)
                {
                    foreach (AFigure figure in storage)
                        (obj as Sticky).addObserver(figure);
                }

                base.add(obj);


                observersInvoke();
            }
            public override void remove(AFigure obj)
            {
                foreach (AFigure figure in storage)
                    if (figure is Sticky && figure != obj)
                    {
                        if ((figure as Sticky).contains_Observer(figure))
                            (figure as Sticky).removeObserver(figure);

                        //if ((figure as Sticky).contains_Intersecter(figure))
                        //    (figure as Sticky).removeIntersecter(figure);
                    }
                
                base.remove(obj);
                selectedIndex = -1;
                observersInvoke();
            }
            public void focus(AFigure obj)
            {
                selectedIndex = -1;
                int i = 0;
                while (selectedIndex == -1 && i < storage.Length)
                {
                    if (obj == storage[i])
                        selectedIndex = i;
                    else
                        i++;
                }
                observersInvoke();
            }
            public void unfocus()
            {
                selectedIndex = -1;
                observersInvoke();
            }
            public int selected_Index()
            {
                return selectedIndex;
            }
            public void removeAll()
            {
                storage = new AFigure[0];
                selectedIndex = -1;
                observersInvoke();
                ActiveForm.Invalidate();
            }
            public void paint()
            {
                foreach (AFigure f in storage)
                    if (f != null)
                        f.paint(grObj);
            }

            public bool contains(AFigure obj)
            {
                bool is_contain = false;
                int i = 0;
                while (is_contain == false && i < storage.Length)
                {
                    if (storage[i] == obj)
                        is_contain = true;
                    else
                        i++;
                }
                return is_contain;
            }
            public int getCount()
            {
                return storage.Length;
            }
            public AFigure getFigure(int iter)
            {
                return storage[iter];
            }
            public void addObserver(TreeObserver tree)
            {
                this.tree = tree;
            }
            public AFigure get_obj_underM(Point mouseP)
            {
                bool is_underM = false;
                int i = storage.Length - 1;
                while (is_underM == false && i >= 0)
                {
                    if (storage[i].is_undermouse(mouseP))
                        is_underM = true;
                    else
                        i--;
                }

                if (is_underM)
                    return storage[i];
                else
                    return null;
            }

            public void observersInvoke()
            {
                tree.onSubjectChanged();
            }

            //save & load
            public void load(AFigure obj)
            {
                //base.add(obj);
                add(obj);
                observersInvoke();
            }
            public void save(StreamWriter sw)
            {
                sw.WriteLine(storage.Length.ToString());
                foreach (AFigure f in storage)
                    f.save(sw);
            }
        }

        ////
        ////objects are done
        ////

        public class Group: AFigure
        {
            private int _maxcount;
            private int _count;
            private AFigure []_figures;
            private string _name;
            private Point p1;
            private Point p2;
            Graphics grObj;

            int maxPosX;
            int maxPosY;

            private bool is_focused;

            public Group(int maxcount, Graphics grObj, int canvasWidth, int canvasHeight)
            {
                _name = "group";
                _maxcount = maxcount;
                _count = 0;
                _figures = new AFigure[maxcount];       //all elements will be null thanks visual studio
                p1 = new Point(-1, -1);
                p2 = new Point(-1, -1);
                this.grObj = grObj;
                maxPosX = canvasWidth;
                maxPosY = canvasHeight;
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
                    if (figure.getP2().X < p1.X)
                        p1.X = figure.getP2().X;

                    if (figure.getP1().Y < p1.Y || p1.Y == -1)
                        p1.Y = figure.getP1().Y;
                    if (figure.getP2().Y < p1.Y)
                        p1.Y = figure.getP2().Y;


                    if (figure.getP2().X > p2.X)
                        p2.X = figure.getP2().X;
                    if (figure.getP1().X > p2.X)
                        p2.X = figure.getP1().X;

                    if (figure.getP2().Y > p2.Y)
                        p2.Y = figure.getP2().Y;
                    if (figure.getP1().Y > p2.Y)
                        p2.Y = figure.getP1().Y;

                    if (figure is Triangle)
                    {
                        if (figure.getP3().X < p1.X)
                            p1.X = figure.getP3().X;

                        if (figure.getP3().Y < p1.Y)
                            p1.Y = figure.getP3().Y;

                        if (figure.getP3().X > p2.X)
                            p2.X = figure.getP3().X;

                        if (figure.getP3().Y > p2.Y)
                            p2.Y = figure.getP3().Y;
                    }

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
                //Point shift = new Point(p.X - p1.X, p.Y - p1.Y);
                //p1.X = p1.X + shift.X;
                //p1.Y = p1.Y + shift.Y;

                //Point tempP = new Point();

                //for (int i = 0; i < _count; i++)
                //{
                //    tempP = _figures[i].getP1();
                //    tempP.X = tempP.X + shift.X;
                //    tempP.Y = tempP.Y + shift.Y;
                //    _figures[i].setP1(tempP);
                //}
            }
            public override void setP2(Point p) { }
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
            public override bool is_undermouse(Point mouseP)
            {
                bool has_objs_underM = false;
                int i = _maxcount - 1;
                while (has_objs_underM == false && i >= 0)
                {
                    if (_figures[i].is_undermouse(mouseP))
                        has_objs_underM = true;
                    else
                        i--;
                }

                return has_objs_underM;
            }

            //for triangle:
            public override void setP3(Point p) { }
            public override Point getP3()
            {
                return new Point(-1, -1);
            }

            //operations:
            public override void focus() {
                foreach (AFigure figure in _figures)
                    figure.focus();
                is_focused = true;
            }
            public override void unfocus()
            {
                foreach (AFigure figure in _figures)
                    figure.unfocus();
                is_focused = false;
            }
            public override bool is_inFocus()
            {
                return is_focused;
            }
            public override void paint(Graphics grObj)
            {
                if (_figures[0] != null)
                    foreach (AFigure figure in _figures)
                        figure.paint(grObj);
            }
            public override bool is_correctPos(Point p)
            {
                if (p.X >= 0 && p.X + _figures[0].getThickness() <= maxPosX && p.Y >= 0 && p.Y + _figures[0].getThickness() <= maxPosX)
                    return true;
                else
                    return false;
            }
            public override void move(Point shift)
            {
                Point tp1 = new Point();
                Point tp2 = new Point();

                tp1.X = p1.X + shift.X;
                tp1.Y = p1.Y + shift.Y;

                tp2.X = p2.X + shift.X;
                tp2.Y = p2.Y + shift.Y;

                if (is_correctPos(tp1) && is_correctPos(tp2))
                {
                    p1 = tp1;
                    p2 = tp2;
                    foreach (AFigure f in _figures)
                    {
                        f.move(shift);
                    }
                }
            }

            //for save&load:
            public override void save(StreamWriter sw)
            {
                try
                {
                    sw.WriteLine("group" + " " + _count.ToString() + " " + maxPosX.ToString() + " " + maxPosY.ToString()) ;
                    foreach (AFigure f in _figures)
                        f.save(sw);
                }
                catch
                {
                    MessageBox.Show("We got the problem of saving a group of objects");
                }
            }
            public override void load(StreamReader sr) {
                try
                {
                    MyFiguresArray tempArr = new MyFiguresArray();
                    string[] code;
                    for (int i = 0; i < _maxcount; i++)
                    {
                        code = sr.ReadLine().Split();
                        AFigure f = tempArr.createFigure(code, grObj);
                        if (f != null)
                        {
                            f.load(sr);
                            addFigure(f);
                        }
                    }
                    
                }
                catch
                {
                    MessageBox.Show("Troubles with loading objects from a group");
                }
            }

            //as observer:
            public override bool onSubjectIntersects(AFigure sticky)
            {
                bool collides = false;
                int i = 0;
                while (i < _maxcount && collides == false)
                    if (_figures[i].onSubjectIntersects(sticky))
                    collides = true;
                else
                    i++;

                if (collides)
                    return true;
                else
                    return false;
            }
            public override void onSubjectMoved(AFigure sticky, Point shift)
            {
                move(shift);
            }
        }

        public class FiguresArray
        {
            private int _count;
            private AFigure []_figures;
            public virtual AFigure createFigure(string[] code, Graphics grObj)
            {
                return null;
            }
            public void loadFigures(StreamReader sr, Graphics grObj, StorageService storage)
            {
                string[] code;
                try
                {
                    _count = int.Parse(sr.ReadLine());
                    _figures = new AFigure[_count];

                    for (int i = 0; i < _count; i++)
                    {
                        code = sr.ReadLine().Split();
                        _figures[i] = createFigure(code, grObj);

                        if (_figures[i] != null)
                        {
                            _figures[i].load(sr);
                            storage.load(_figures[i]);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Can't get objects from the file");
                }
            }
        }
        public class MyFiguresArray: FiguresArray
        {
            public override AFigure createFigure(string[] code, Graphics grObj)
            {
                AFigure figure = null;
                switch(code[0])
                {
                    case "sctn":
                        figure = new Section(new Point(0, 0), new Point(0, 0), 1, Color.Black, grObj, 1, 1);
                        break;
                    case "elps":
                        figure = new Ellipse(new Point(0, 0), new Point(0, 0), 1, Color.Black, grObj, 1, 1);
                        break;
                    case "rect":
                        figure =  new Rect(new Point(0, 0), new Point(0, 0), 1, Color.Black, grObj, 1, 1);
                        break;
                    case "trn":
                        figure = new Triangle(new Point(0, 0), new Point(0, 0), new Point(0, 0), 1, Color.Black, grObj, 1, 1);
                        break;
                        case "sticky":
                        figure = new Sticky(new Point(0, 0), new Point(0, 0), 1, Color.Black, grObj, 1, 1);
                        break;
                    default:            // if it is a group
                        figure = new Group(int.Parse(code[1].ToString()), grObj, int.Parse(code[2]), int.Parse(code[3]));
                        break;
                }
                return figure;
            }
        }
        public class TreeObserver: SingleObserver
        {
            private TreeView tree;
            private StorageService storage;        //this is the model of an observer from the book, so it has the single object to observe
            private TreeViewEventHandler handler;
            public TreeObserver(TreeView tree, TreeViewEventHandler handler, StorageService storage)
            {
                this.tree = tree;
                this.handler = handler;
                this.storage = storage;
            }
            public override void onSubjectChanged()
            {

                // we are refreshing the treeView so initially we should delete the old tree
                for (int n = tree.Nodes.Count - 1; n >= 0; n--)
                    tree.Nodes.RemoveAt(n);

                try
                {
                    tree.Nodes.Add("storage");

                    //adding figures by using the reversive method addNode
                    int count = storage.getCount();
                    for (int i = 0; i < count; i++)
                        addNode(tree.Nodes[0], storage.getFigure(i), i);


                    //focusing
                    tree.AfterSelect -= handler;
                    if (storage.selected_Index() != -1)
                        tree.SelectedNode = tree.Nodes[0].Nodes[storage.selected_Index()];
                    tree.AfterSelect += handler;

                    tree.ExpandAll();
                }
                catch
                {
                    MessageBox.Show("Error. Can't refresh the tree");
                }
            }
            public void addNode(TreeNode node, AFigure figure, int index)
            {
                node.Nodes.Add(figure.getName());

                if (figure is Group)
                {
                    int gCount = (figure as Group).getCount();
                    for (int j = 0; j < gCount; j++)
                    {
                        addNode(node.Nodes[index], (figure as Group).getFigure(j), j);
                    }
                }
            }

        }
        public class Model
        {
            private Color color;
            private int thickness;

            private Graphics grObj;
            private StorageService storage;
            private AFigure obj;

            private int canvasWidth;
            private int canvasHeight;

            public System.EventHandler observers;


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
                    obj.setP1(p);

                observers.Invoke(this, null);
            }
            public void setP2(Point p)
            {
                if (is_CorrectPos(p))
                    obj.setP2(p);

                observers.Invoke(this, null);
            }
            public void setP3(Point p)
            {
                if (is_CorrectPos(p))
                    obj.setP3(p);

                observers.Invoke(this, null);
            }
            public void setSize(int width, int height)
            {
                Point p = new Point(obj.getP1().X + width, obj.getP1().Y + height);
                if (is_CorrectPos(p))
                    obj.setP2(p);
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
                return obj.getP1();
            }
            public Point getP2()
            {
                return obj.getP2();
            }
            public Point getP3()
            {
                return obj.getP3();
            }
            public int getCanvWidth()
            {
                return canvasWidth;
            }
            public int getCanvHeight()
            {
                return canvasHeight;
            }



            //select an object
            public bool obj_is_selected()
            {
                if (obj != null)
                    return true;
                else
                    return false;
            }
            public void unselect()
            {
                if (obj == null)
                    return;
                else
                {
                    obj.unfocus();
                    objName = "";
                    obj = null;

                    storage.unfocus();
                    
                    observers.Invoke(this, null);
                }
            }
            public void setObject(AFigure obj)
            {
                if (obj == null)
                    return;

                // I think that sticky objects should not be added in the group
                if ((multiSelect == false) || (obj is Sticky) || (this.obj is Sticky))
                {
                    unselect();

                    this.obj = obj;
                    setObjName(obj.getName());
                    color = obj.getColor();
                    thickness = obj.getThickness();

                    obj.focus();
                    storage.focus(obj);
                }
                else
                {
                    Group g = new Group(2, grObj, canvasWidth, canvasHeight);      // count == 2 because first is the the group before, second - new object
                    g.addFigure(this.obj);
                    g.addFigure(obj);

                    storage.remove(this.obj);
                    storage.remove(obj);
                    unselect();

                    storage.add(g);
                    
                    this.obj = g;
                    setObjName(g.getName());
                    color = g.getColor();
                    thickness = g.getThickness();

                    g.focus();
                    storage.focus(g);
                }

                observers.Invoke(this, null);
            }
            public void setObjectFromTree(TreeView tree)
            {
                //we should not took elements inside the groups so we should check only the 2nd level of the tree's nodes
                bool has_selected = false;

                for (int i = 0; i < tree.Nodes[0].GetNodeCount(false) && has_selected == false; i++)
                    if (tree.Nodes[0].Nodes[i].IsSelected)
                    {
                        setObject(storage.getFigure(i));
                        has_selected = true;
                    }
                if (!has_selected)
                    tree.SelectedNode = null;
            }

            public AFigure getObject()
            {
                return obj;
            }


            ////// clicked mousebuttons: creating/deleting

            string btnName = "";
            string objName = "";
            Point mp1 = new Point(-1, -1);
            Point mp2 = new Point(-1, -1);
            Point mp3 = new Point(-1, -1);

            bool multiSelect = false;

            public void setObjName(string objName)
            {
                this.objName = objName;
            }
            public void setNameFromBtn(string btnName)
            {
                unselect();
                mPosReset();
                this.btnName = btnName;
                objName = btnName;
            }
            public string getObjName()
            {
                return objName;
            }
            public void mPosReset()
            {
                mp1 = new Point(-1, -1);
                mp2 = new Point(-1, -1);
                mp3 = new Point(-1, -1);
            }
            public void mouseProcess(Point mouseP)
            {
                if (btnName == "btnArw")
                {
                    AFigure figure = storage.get_obj_underM(mouseP);
                    if (figure != null)
                    {
                        setObject(figure);
                    }
                    else
                    {
                        unselect();
                    }
                    return;
                }

                if (mp1.X == -1)
                    mp1 = mouseP;
                
                else if (mp2.X == -1)
                {
                    mp2 = mouseP;
                    if (objName != "trn")
                    {
                        AFigure figure;
                        if (objName == "sctn")
                            figure = new Section(mp1, mp2, thickness, color, grObj, canvasWidth, canvasHeight);

                        else if (objName == "elps")
                            figure = new Ellipse(mp1, mp2, thickness, color, grObj, canvasWidth, canvasHeight);

                        else if (objName == "rect")
                            figure = new Rect(mp1, mp2, thickness, color, grObj, canvasWidth, canvasHeight);

                        else
                        {
                            figure = new Sticky(mp1, mp2, thickness, color, grObj, canvasWidth, canvasHeight);
                            //(figure as Sticky).setModel(this);
                        }
                        if (figure != null)
                        {
                            mPosReset();
                            storage.add(figure);
                            setObject(figure);
                        }
                    }
                }
                else
                {
                    mp3 = mouseP;
                    if (objName == "trn")
                    {
                        AFigure figure = new Triangle(mp1, mp2, mp3, thickness, color, grObj, canvasWidth, canvasHeight);

                        mPosReset();
                        storage.add(figure);
                        setObject(figure);
                    }
                }
            }
            public void deleteObj()
            {
                if (obj != null)
                {
                    storage.remove(obj);
                    unselect();
                    observers.Invoke(this, null);
                }
            }
            public void deleteAll()
            {
                storage.removeAll();
                observers.Invoke(this, null);
            }


            ////// pressed keybuttons: moving, scaling
            
            private bool allow_treeRefresh = true;             // I don't want that tree was refreshed every time the point moves

            public void refreshDeny()
            {
                allow_treeRefresh = false;
            }
            public void refreshAllow()
            {
                allow_treeRefresh = true;
            }
            public bool allows_treeRefresh()
            {
                return allow_treeRefresh;
            }
            public void keysProcess(Keys code)
            {
                if (obj == null)
                    return;

                if (code == Keys.ControlKey)
                {
                    multiSelect = true;
                    return;
                }

                allow_treeRefresh = false;

                Point p1 = obj.getP1();
                Point p2 = obj.getP2();

                Point shift = new Point(0, 0);
                if (code == Keys.NumPad4)
                {
                    shift.X = -1;
                    setPos(shift);
                }
                else if (code == Keys.NumPad6)
                {
                    shift.X = 1;
                    setPos(shift);
                }
                else if (code == Keys.NumPad8)
                {
                    shift.Y = -1;
                    setPos(shift);
                }
                else if (code == Keys.NumPad2)
                {
                    shift.Y = 1;
                    setPos(shift);
                }
                else if (code == Keys.Oemplus && !(obj is Triangle) && !(obj is Section) && !(obj is Group))
                {
                    p2.X = p1.X + Math.Abs(p2.X - p1.X) + 1;
                    p2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) + 1;
                    setP2(p2);
                }
                else if (code == Keys.OemMinus && !(obj is Triangle) && !(obj is Section) && !(obj is Group))
                {
                    p2.X = p1.X + Math.Abs(p2.X - p1.X) - 1;
                    p2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) - 1;
                    setP2(p2);
                }
                else if (code == Keys.Delete)
                {
                    refreshAllow();
                    deleteObj();
                }
                allow_treeRefresh = true;
            }
            public void keyUpProcess(Keys code)
            {
                if (code == Keys.ControlKey)
                    multiSelect = false;
            }
            public void setPos(Point shift)
            {
                obj.move(shift);
                observers.Invoke(this, null);

                //Point p1 = obj.getP1();
                //Point p2 = obj.getP2();
                //Point p3 = obj.getP3();

                //p1.X = p1.X + shift.X;
                //p2.X = p2.X + shift.X;
                //p3.X = p3.X + shift.X;
                //p1.Y = p1.Y + shift.Y;
                //p2.Y = p2.Y + shift.Y;
                //p3.Y = p3.Y + shift.Y;

                //if (objName != "trn" && is_CorrectPos(p1) && is_CorrectPos(p2))
                //{
                //    obj.move(shift);
                //    observers.Invoke(this, null);
                //}
                //else if (objName == "trn" && is_CorrectPos(p1) && is_CorrectPos(p2) && is_CorrectPos(p3))
                //{
                //    obj.move(shift);
                //    observers.Invoke(this, null);
                //}
            }
            public bool checkShift(AFigure figure, Point shift)
            {
                Point p1 = figure.getP1();
                Point p2 = figure.getP2();
                Point p3 = figure.getP3();

                p1.X = p1.X + shift.X;
                p2.X = p2.X + shift.X;
                p3.X = p3.X + shift.X;
                p1.Y = p1.Y + shift.Y;
                p2.Y = p2.Y + shift.Y;
                p3.Y = p3.Y + shift.Y;

                if (objName != "trn" && is_CorrectPos(p1) && is_CorrectPos(p2))
                {
                    return true;
                }
                else if (objName == "trn" && is_CorrectPos(p1) && is_CorrectPos(p2) && is_CorrectPos(p3))
                {
                    return true;
                }
                else
                    return false;
            }
            public void destructor()
            {
                Properties.Settings.Default.thickness = thickness;
                Properties.Settings.Default.canvasWidth = canvasWidth;
                Properties.Settings.Default.canvasHeight = canvasHeight;
                Properties.Settings.Default.Save();

                string path = "C:\\Users\\пк\\source\\repos\\oopLab6\\storage.txt";
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        storage.save(sw);
                    }
                }
                catch
                {
                    MessageBox.Show("We could not load objects from file");
                }
            }
            public Model(StorageService storage, Graphics grObj)
            {
                color = Color.Black;
                thickness = Properties.Settings.Default.thickness;
                canvasWidth = Properties.Settings.Default.canvasWidth;
                canvasHeight = Properties.Settings.Default.canvasHeight;
                this.storage = storage;
                this.grObj = grObj;
                btnName = "btnArw";

                //loading objects:
                MyFiguresArray figuresArray = new MyFiguresArray();
                string path = "C:\\Users\\пк\\source\\repos\\oopLab6\\storage.txt";
                try
                {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    figuresArray.loadFigures(sr, grObj, storage);
                }
                }
                catch
                {
                    MessageBox.Show("We could not load objects from file");
                }
            }
        }

        public void UpdateFromModel(object sender, EventArgs e)
        {
            flpSz.Visible = false;
            flpP1.Visible = false;
            flpP2.Visible = false;
            flpP3.Visible = false;

            if (model.obj_is_selected() == false)
            {
                Invalidate();
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


            AFigure obj = model.getObject();

            if (model.getObjName() == "sctn" || model.getObjName() == "trn")
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
            }
            else if (model.getObjName() != "group")
            {
                numWdt.ValueChanged -= new EventHandler(size_ValueChanged);
                numHgh.ValueChanged -= new EventHandler(size_ValueChanged);

                numWdt.Value = Math.Abs(model.getP2().X - model.getP1().X);
                numHgh.Value = Math.Abs(model.getP2().Y - model.getP1().Y);

                numWdt.ValueChanged += new EventHandler(size_ValueChanged);
                numHgh.ValueChanged += new EventHandler(size_ValueChanged);

                flpSz.Visible = true;
            }

            if (obj != null)
            {
                obj.setColor(model.getColor());
                obj.setThickness(model.getThickness());
            }

            this.Invalidate();
        }

        //
        //
        /////////////////////model is done///////////////////////
        //
        //
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
            model.mouseProcess(mousePos);
        }


        private void btnArw_Click(object sender, EventArgs e)
        {
            model.setNameFromBtn((sender as Button).Name);
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
        private void btnTrsh_Click(object sender, EventArgs e)
        {
            try
            {
                model.deleteObj();
            }
            catch
            {
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            model.deleteAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            model.setObjectFromTree(treeView1);
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            pressedKey = e.KeyCode;
            model.keysProcess(pressedKey);
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            model.keyUpProcess(e.KeyCode);
            e.Handled = true;
        }

        private void sticky_Click(object sender, EventArgs e)
        {
            model.setNameFromBtn((sender as Button).Name);
        }
    }
}