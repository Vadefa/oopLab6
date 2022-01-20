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
        StorageService storage;
        Model model;

        public Form1()
        {
            InitializeComponent();

            storage = new StorageService();
            model = new Model(storage);
            model.observers += new EventHandler(UpdateFromModel);
            model.observers.Invoke(this, null);
        }

        public abstract class AFigure
        {
            public abstract void setP1(Point p);
            public abstract void setP2(Point p);
            public abstract void setMaxPosX(int maxPosX);
            public abstract void setMaxPosY(int maxPosY);
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
            public abstract void paint(Graphics grObj);
            public abstract void move(Point shift);

            //for check:
            public abstract bool is_inBorders(Point p);

            //for save&load:
            public abstract void save(StreamWriter sw);
            public abstract void load(StreamReader sr);
        }
        public class Figure: AFigure
        {
            protected string name;
            protected Point p1;
            protected Point p2;
            protected int thickness;
            protected Color color;
            
            protected int maxPosX;
            protected int maxPosY;

            protected bool is_focused = false;
            protected GraphicsPath grPath;

            public Figure()
            {
                name = "";
                p1 = new Point(0, 0);
                p2 = new Point(0, 0);
                thickness = 1;
                color = Color.Black;
                grPath = new GraphicsPath();
            }
            public Figure(Point p1, Point p2, int thickness, Color color, bool allow_reverse, int maxPosX, int maxPosY)
            {
                name = "figure";
                this.thickness = thickness;
                this.color = color;
                this.maxPosX = maxPosX;
                this.maxPosY = maxPosY;
                grPath = new GraphicsPath();
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
            }
            public override void paint(Graphics grObj)
            {
            }
            public override void move(Point shift)
            {

                Point tp1 = new Point();
                Point tp2 = new Point();
                
                tp1.X = p1.X + shift.X;
                tp1.Y = p1.Y + shift.Y;

                tp2.X = p2.X + shift.X;
                tp2.Y = p2.Y + shift.Y;

                if (is_inBorders(tp1) && is_inBorders(tp2))
                {
                    p1 = tp1;
                    p2 = tp2;
                }
            }
            public override bool is_inBorders(Point p)
            {
                if (p.X - thickness >= 0 && p.X + thickness <= maxPosX && p.Y - thickness >= 0 && p.Y + thickness <= maxPosY)
                    return true;
                else
                    return false;
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
                if (is_inBorders(p))
                    p1 = p;
            }
            public override void setP2(Point p)
            {
                if (is_inBorders(p))
                    p2 = p;
            }
            
            public override void setMaxPosX(int maxPosX)
            {
                this.maxPosX = maxPosX;
            }
            public override void setMaxPosY(int maxPosY)
            {
                this.maxPosY = maxPosY;
            }
            public override void setThickness(int thickness)
            {
                // we can move out of borders if we'll change thickness so we should check it
                if (is_inBorders(new Point(p1.X - this.thickness + thickness, p1.Y - this.thickness + thickness))
                    && is_inBorders(new Point(p2.X - this.thickness + thickness, p2.Y - this.thickness + thickness)))
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
                    sw.WriteLine(p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                        + " " + maxPosX.ToString() + " " + maxPosY.ToString());
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
        }
        public class Section : Figure
        {
            public Section()
                : base()
            {
                name = "sctn";
            }
            public Section(Point p1, Point p2, int thickness, Color color, int maxPosX, int maxPosY)
                : base(p1, p2, thickness, color, false, maxPosX, maxPosY)
            {
                name = "sctn";
            }
            public override void paint(Graphics grObj)
            {
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
            public Ellipse() : base()
            {
                name = "elps";
            }
            public Ellipse(Point p1, Point p2, int thickness, Color col, int maxPosX, int maxPosY)
                : base(p1, p2, thickness, col, true, maxPosX, maxPosY)
            {
                name = "elps";
            }
            public override void paint(Graphics grObj)
            {
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
            public Rect() : base()
            {
                name = "rect";
            }
            public Rect(Point p1, Point p2, int thickness, Color col, int maxPosX, int maxPosY)
                : base(p1, p2, thickness, col, true, maxPosX, maxPosY)
            {
                name = "rect";
            }
            public override void paint(Graphics grObj)
            {
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
            public Triangle() : base()
            {
                name = "trn";
            }
            public Triangle(Point p1, Point p2, Point p3, int thickness, Color col, int maxPosX, int maxPosY)
            : base(p1, p2, thickness, col, false, maxPosX, maxPosY)
            {
                name = "trn";
                this.p3 = p3;
            }
            public override void paint(Graphics grObj)
            {
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
                Point tp1 = new Point();
                Point tp2 = new Point();
                Point tp3 = new Point();

                tp1.X = p1.X + shift.X;
                tp1.Y = p1.Y + shift.Y;
                tp2.X = p2.X + shift.X;
                tp2.Y = p2.Y + shift.Y;
                tp3.X = p3.X + shift.X;
                tp3.Y = p3.Y + shift.Y;

                if (is_inBorders(tp1) && is_inBorders(tp2) && is_inBorders(tp3))
                {
                    p1 = tp1;
                    p2 = tp2;
                    p3 = tp3;
                }
            }

            public override void setThickness(int thickness)
            {
                if (is_inBorders(new Point(p1.X - this.thickness + thickness, p1.Y - this.thickness + thickness))
                    && is_inBorders(new Point(p2.X - this.thickness + thickness, p2.Y - this.thickness + thickness))
                    && is_inBorders(new Point(p3.X - this.thickness + thickness, p3.Y - this.thickness + thickness)))
                    this.thickness = thickness;
        }
            public override Point getP3()
            {
                return p3;
            }
            public override void setP3(Point p3)
            {
                if (is_inBorders(p3))
                    this.p3 = p3;
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
            public StorageService()
            {
            }
            public override void add(AFigure obj)
            {
                base.add(obj);
            }
            public void fill(AFigure obj)
            {
                base.add(obj);
            }
            public void load(AFigure obj)
            {
                base.add(obj);
            }
            public void save(StreamWriter sw)
            {
                sw.WriteLine(storage.Length.ToString());
                foreach (AFigure f in storage)
                    f.save(sw);
            }
            public override void remove(AFigure figure)
            {
                base.remove(figure);
            }
            public void clear()
            {
                storage = new AFigure[0];
            }
            public void paint(Graphics grObj)
            {
                foreach (AFigure f in storage)
                    f.paint(grObj);
            }

            public AFigure check_objs_underM(Point mouseP)
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
                {
                    return storage[i];
                }
                else
                    return null;
            }
            public int getCount()   // an emergency situation was occured - we need to send the storage to model
            {
                return storage.Length;
            }
            public AFigure getFigure(int iter)
            {
                return storage[iter];
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
            private Point p1;       //left upper corner
            private Point p2;       //right lower corner
            private int thickness;
            private int maxPosX;
            private int maxPosY;

            public Group()
            {
                _count = 0;
            }
            public Group(int maxcount, int maxPosX, int maxPosY, int thickness)
            {
                _name = "group";
                _maxcount = maxcount;
                _count = 0;
                _figures = new AFigure[maxcount];       //all elements will be null thanks visual studio
                p1 = new Point(-1, -1);
                p2 = new Point(-1, -1);

                this.maxPosX = maxPosX;
                this.maxPosY = maxPosY;
                this.thickness = thickness;
            }
            public bool addFigure(AFigure figure)
            {
                if (_count >= _maxcount)
                    return false;
                else
                {
                    if (_count != 0)
                        figure.setColor(getColor());

                    _count = _count + 1;
                    _figures[_count - 1] = figure;
                    

                    //setting new corners if the object is outside of the current group's "rectangle"
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
            public void removeFigure(AFigure figure)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_figures[i] == figure)
                    {
                        AFigure[] temp = new AFigure[_count - 1];
                        int j = 0;
                        while (j != i)
                        {
                            temp[j] = _figures[j];
                            j++;
                        }
                        j = j + 1;
                        for (; j < _count; j++)
                            temp[j - 1] = _figures[j];

                        _figures = new AFigure[temp.Length];
                        for (i = 0; i < temp.Length; i++)
                            _figures[i] = temp[i];

                        _count = _count - 1;
                        break;
                    }
                }
            }
            public void removeFigures()
            {
                for (int i = _count - 1; i >= 0; i--)
                {
                    _figures[i] = null;
                }
            }
            public List<AFigure> ungroup()
            {
                List<AFigure> figures = new List<AFigure>();
                foreach (AFigure figure in _figures)
                {
                    figures.Add(figure);
                }
                removeFigures();
                return figures;
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
            public override void setP1(Point p) { }
            public override void setP2(Point p) { }
            public override void setMaxPosX(int maxPosX)
            {
                this.maxPosX = maxPosX;
            }
            public override void setMaxPosY(int maxPosY)
            {
                this.maxPosY = maxPosY;
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
            }
            public override void unfocus()
            {
                foreach (AFigure figure in _figures)
                    if (figure != null)
                        figure.unfocus();
            }
            public override void paint(Graphics grObj)
            {
                if (_figures[0] != null)
                    foreach (AFigure figure in _figures)
                        figure.paint(grObj);
            }
            public override void move(Point shift)
            {
                Point tp1 = new Point();
                Point tp2 = new Point();

                tp1.X = p1.X + shift.X;
                tp1.Y = p1.Y + shift.Y;

                tp2.X = p2.X + shift.X;
                tp2.Y = p2.Y + shift.Y;

                if (is_inBorders(tp1) && is_inBorders(tp2))
                {
                    p1 = tp1;
                    p2 = tp2;
                    foreach (AFigure f in _figures)
                    {
                        f.move(shift);
                    }
                }
            }

            //check:
            public override bool is_inBorders(Point p)
            {
                if (p.X - thickness >= 0 && p.X + thickness <= maxPosX && p.Y - thickness >= 0 && p.Y + thickness <= maxPosY)
                    return true;
                else
                    return false;
            }

            //for save&load:
            public override void save(StreamWriter sw)
            {
                try
                {
                    sw.WriteLine("group");
                    sw.WriteLine(_count.ToString() + " " + maxPosX.ToString() + " " + maxPosY.ToString());
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
                    string[] code;
                    code = sr.ReadLine().Split();
                    _maxcount = int.Parse(code[0]);
                    _figures = new AFigure[_maxcount];
                    maxPosX = int.Parse(code[1]);
                    maxPosX = int.Parse(code[2]);

                    p1 = new Point(-1, -1);
                    p2 = new Point(-1, -1);

                    MyFiguresArray tempArr = new MyFiguresArray();
                    for (int i = 0; i < _maxcount; i++)
                    {
                        code = sr.ReadLine().Split();
                        AFigure f = tempArr.createFigure(code);
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
        }

        public class FiguresArray
        {
            private int _count;
            private AFigure []_figures;
            public virtual AFigure createFigure(string[] code) { return null; }
            
            public void loadFigures(StreamReader sr, StorageService storage)
            {
                string[] code;
                try
                {
                    _count = int.Parse(sr.ReadLine());
                    _figures = new AFigure[_count];

                    for (int i = 0; i < _count; i++)
                    {
                        code = sr.ReadLine().Split();
                        _figures[i] = createFigure(code);

                        if (_figures[i] != null)        //if null -> empty string in the txt or something wrong
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
            public override AFigure createFigure(string[] code)
            {
                AFigure figure = null;
                switch(code[0])
                {
                    case "sctn":
                        figure = new Section();
                        break;
                    case "elps":
                        figure = new Ellipse();
                        break;
                    case "rect":
                        figure =  new Rect();
                        break;
                    case "trn":
                        figure = new Triangle();
                        break;
                    default:            // if it is a group
                        figure = new Group();
                        break;
                }
                return figure;
            }
        }

        public class Model
        {
            //argegating data:
            private StorageService storage;

            //observer that calls the controller to refresh the canvas
            public EventHandler observers;
            private bool is_a_set;                          //if we handle more than one figures

            //data related objects
            private List<AFigure> selectedFigures;          //figures those are processing by the model
            private Point[] positions;
            private int thickness;
            private Color color;

            //data related the canvas
            private int canvasWidth;
            private int canvasHeight;


            private string btnName;

            //creating and selecting elements
            public void mouseClickGetting(Point mouseP)
            {
                for (int i = 0; i < positions.Length; i++)
                    if (positions[i].X == -1)
                    {
                        positions[i] = mouseP;
                        handlePosition();

                        break;
                    }

                // if the array is full, it won't be added
            }
            private void handlePosition()
            {
                //when handling the position we want to create an object or select an object. It depends on the button that was pressed before.
                switch (btnName)
                {
                    case "btnSctn":
                        tryCreateFigure("sctn");
                        break;

                    case "btnElps":
                        tryCreateFigure("elps");
                        break;

                    case "btnRect":
                        tryCreateFigure("rect");
                        break;

                    case "btnTrn":
                        tryCreateFigure("trn");
                        break;

                    case "btnArw":
                        btnArrowHandle();
                        break;

                    default:
                        break;
                }
            }
            public void tryCreateFigure(string type)
            {
                unselect();     //we are creating a new element so let's forget about properties of the element we created before
                if (positions[1].X != -1)
                {
                    if (type == "trn" && positions[2].X != -1)
                    {
                        create(new Triangle(positions[0], positions[1], positions[2], thickness, color, canvasWidth, canvasHeight));
                    }
                    else
                    {
                        switch (type)
                        {
                            case "sctn":
                                create(new Section(positions[0], positions[1], thickness, color, canvasWidth, canvasHeight));
                                break;
                            case "elps":
                                create(new Ellipse(positions[0], positions[1], thickness, color, canvasWidth, canvasHeight));
                                break;
                            case "rect":
                                create(new Rect(positions[0], positions[1], thickness, color, canvasWidth, canvasHeight));
                                break;

                        }
                    }
                }
                
            }
            private void create(AFigure figure)
            {
                selectedFigures.Add(figure);
                storage.add(figure);
                positionReset();
                obsInvoke();
            }
            private void positionReset()
            {
                positions = new Point[3] { new Point(-1, -1), new Point(-1, -1), new Point(-1, -1) };
            }
            private void btnArrowHandle()
            {
                //If we click on the canvas by the arrow, then we want to select the object.
                //Here we need only one Point - point where the mouse was just clicked. It should be positions[0]
                AFigure figure = storage.check_objs_underM(positions[0]);
                if (figure != null)
                {
                    selectedFigures.Add(figure);    //we're not creating the new element so we don't use the method create()
                    obsInvoke();
                }
                else
                {
                    unselect();
                    positionReset();
                }

                positionReset();
            }

            //handling elements:
            public void buttonPressedGetting(Keys key)
            {
                switch (key)
                {
                    case Keys.NumPad4:
                        move(new Point(-1, 0));
                        break;
                    case Keys.NumPad6:
                        move(new Point(1, 0));
                        break;
                    case Keys.NumPad8:
                        move(new Point(0, -1));
                        break;
                    case Keys.NumPad2:
                        move(new Point(0, 1));
                        break;
                    case Keys.Delete:
                        delete();
                        break;
                    case Keys.G:
                        group();
                        break;
                    case Keys.U:
                        ungroup();
                        break;
                    case Keys.Oemplus:
                        handleSizeSetting('+');
                        break;
                    case Keys.OemMinus:
                        handleSizeSetting('-');
                        break;
                    default:
                        break;
                }
            }
            public void move(Point shift)
            {
                foreach (AFigure figure in selectedFigures)
                    figure.move(shift);
                obsInvoke();
            }
            private void handleSizeSetting(char sign)
            {
                if (selectedFigures.Count == 1 && !(selectedFigures[0] is Section) && !(selectedFigures[0] is Triangle)
                    && !(selectedFigures[0] is Group))
                {
                    int width;
                    int height;
                    if (sign == '+')
                    {
                        width = selectedFigures[0].getP2().X - selectedFigures[0].getP1().X + 1;
                        height = selectedFigures[0].getP2().Y - selectedFigures[0].getP1().Y + 1;
                    }
                    else
                    {
                        width = selectedFigures[0].getP2().X - selectedFigures[0].getP1().X - 1;
                        height = selectedFigures[0].getP2().Y - selectedFigures[0].getP1().Y - 1;
                    }
                    setSize(width, height);
                }
            }
            public void delete()
            {
                foreach (AFigure figure in selectedFigures)
                    storage.remove(figure);
                unselect();
                positionReset();
                obsInvoke();
            }
            public void clear()
            {
                unselect();
                positionReset();
                storage.clear();
                obsInvoke();
            }
            public void group()
            {
                if (selectedFigures.Count > 1)
                {
                    Group g = new Group(selectedFigures.Count, canvasWidth, canvasHeight, thickness);
                    for (int i = 0; i < selectedFigures.Count; i++)
                    {
                        g.addFigure(selectedFigures[i]);
                        storage.remove(selectedFigures[i]);
                    }
                    unselect();
                    positionReset();
                    selectedFigures.Add(g);
                    storage.add(g);
                    obsInvoke();
                }
            }
            public void ungroup()
            {
                if (selectedFigures.Count == 1 && selectedFigures[0] is Group)
                {
                    Group tempG = (selectedFigures[0] as Group);
                    unselect();
                    positionReset();
                    AFigure tempF;
                    for (int i = 0; i < tempG.getCount(); i++)
                    {
                        tempF = tempG.getFigure(i);
                        create(tempF);           //I want that pushed out figures were added in the storage too and were initially focused
                    }
                }
            }
            private void unselect()
            {
                foreach (AFigure f in selectedFigures)
                    f.unfocus();

                selectedFigures = new List<AFigure>();
                //positionReset();
                obsInvoke();
            }
            private void obsInvoke()    // selects all the selected figures and invokes the controller
            {
                if (selectedFigures.Count > 1)
                    is_a_set = true;
                else
                    is_a_set = false;

                foreach (AFigure figure in selectedFigures)
                {
                    figure.focus();
                }

                observers.Invoke(this, null);
            }

            //setters
            public void setBtn(string btnName)
            {
                unselect();
                positionReset();
                this.btnName = btnName;
            }
            public void setCol(Color color)
            {
                this.color = color;
                foreach (AFigure figure in selectedFigures)
                    figure.setColor(color);

                obsInvoke();
            }
            public void setThick(int thickness)
            {
                if (thickness > 0 && thickness <= 20)
                {
                    this.thickness = thickness;
                    foreach (AFigure figure in selectedFigures)
                        figure.setThickness(thickness);
                }
                obsInvoke();
            }
            public void setSize(int width, int height)
            {
                if (selectedFigures.Count == 0)
                    return;

                Point tp1 = selectedFigures[0].getP1();
                Point tp2 = selectedFigures[0].getP2();

                int dWidth = width - (tp2.X - tp1.X);   // current width of an object = p2.X - p1.X
                int dHeight = height - (tp2.Y - tp1.Y);

                tp2.X = tp2.X + dWidth;
                tp2.Y = tp2.Y + dHeight;

                if (tp2.X > selectedFigures[0].getP1().X && tp2.Y > selectedFigures[0].getP1().Y)
                {
                    selectedFigures[0].setP2(tp2);      // the size of a figure changes depending on its p2. p1 will remain the same
                }
                obsInvoke();
            }
            public void setPos(Point p, char number)
            {
                if (selectedFigures.Count == 0)
                    return;

                switch (number)
                {
                    case '1':
                        selectedFigures[0].setP1(p);
                        break;
                    case '2':
                        selectedFigures[0].setP2(p);
                        break;
                    case '3':
                        selectedFigures[0].setP3(p);
                        break;
                    default:
                        break;
                }
                obsInvoke();
            }


            //getters
            public bool handles_set()
            {
                return is_a_set;
            }
            public AFigure getFigure(int iter)
            {
                if (selectedFigures.Count != 0)
                    return selectedFigures[iter];
                else
                    return null;
            }
            public int getCanvWidth()
            {
                return canvasWidth;
            }
            public int getCanvHeight()
            {
                return canvasHeight;
            }
            public int getThick()
            {
                return thickness;
            }

            //handling pop-up form:
            public void canvSetSizeSend(PopupCanvSetSize popup)
            {
                unselect();
                positionReset();

                popup.mtbW.Text = canvasWidth.ToString();
                popup.mtbH.Text = canvasHeight.ToString();
            }
            public void canvSetSizeReceive(PopupCanvSetSize popup)
            {
                int width = int.Parse(popup.mtbW.Text);
                int height = int.Parse(popup.mtbH.Text);

                if (width > 99 && width < 1000 && height > 99 && height < 1000)
                {
                    canvasWidth = width;
                    canvasHeight = height;
                    popup.Close();
                    refreshCanvas();
                }
                else
                {
                    if (width < 99)
                        popup.mtbW.Text = "100";
                    else if (width >= 1000)
                        popup.mtbW.Text = "999";

                    if (height < 99)
                        popup.mtbH.Text = "100";
                    else if (height >= 1000)
                        popup.mtbH.Text = "99";
                }
            }
            public void refreshCanvas()
            {
                List<AFigure> figuresToDelete = new List<AFigure>();
                int count = storage.getCount();
                AFigure figure;
                for (int i = 0; i < count; i++)
                {
                    figure = storage.getFigure(i);
                    if (figure.getP1().X + figure.getThickness() > canvasWidth
                        || figure.getP1().Y + figure.getThickness() > canvasHeight
                        || figure.getP2().X + figure.getThickness() > canvasWidth
                        || figure.getP2().Y + figure.getThickness() > canvasHeight)
                    {
                        figuresToDelete.Add(figure);
                    }
                    else
                    {
                        figure.setMaxPosX(canvasWidth);
                        figure.setMaxPosY(canvasHeight);
                    }
                }

                foreach (AFigure f in figuresToDelete)
                {
                    //if (f is Group) //of course I should have made the recoursive method, I just don't want to clutter up (захламлять) the model
                    //{
                    //    AFigure groupFigure;
                    //    int groupCount = (f as Group).getCount();

                    //    for (int i = groupCount - 1; i >= 0; i--)
                    //    {
                    //        groupFigure = (f as Group).getFigure(i);
                    //        if (groupFigure.getP1().X + groupFigure.getThickness() > canvasWidth
                    //            || groupFigure.getP1().Y + groupFigure.getThickness() > canvasHeight
                    //            || groupFigure.getP2().X + groupFigure.getThickness() > canvasWidth
                    //            || groupFigure.getP2().Y + groupFigure.getThickness() > canvasHeight)
                    //        {
                    //            (f as Group).removeFigure(groupFigure);
                    //        }
                    //        else
                    //        {
                    //            groupFigure.setMaxPosX(canvasWidth);
                    //            groupFigure.setMaxPosY(canvasHeight);
                    //        }
                    //    }

                    //    if ((f as Group).getCount() == 1)
                    //    {
                    //        //if count = 1 then, in the group remained only one element.
                    //        //but the group that contains only one element - is not the group anymore. It's a single object.
                    //        //so let's ungroup it:

                    //        AFigure tempF = (f as Group).getFigure(0);
                    //        unselect();
                    //        storage.add(tempF);
                    //    }
                    //}

                    storage.remove(f);
                }
                obsInvoke();
            }

            //destructor and constructor
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
                    MessageBox.Show("We can not save objects in the file");
                }
            }
            public Model(StorageService storage)
            {
                this.storage = storage;


                is_a_set = false;
                selectedFigures = new List<AFigure>();
                positions = new Point[3] { new Point(-1, -1), new Point(-1, -1), new Point(-1, -1) };
                thickness = Properties.Settings.Default.thickness;
                color = Color.Black;

                btnName = "";

                canvasWidth = Properties.Settings.Default.canvasWidth;
                canvasHeight = Properties.Settings.Default.canvasHeight;

                ////loading objects:
                MyFiguresArray figuresArray = new MyFiguresArray();
                string path = "C:\\Users\\пк\\source\\repos\\oopLab6\\storage.txt";
                try
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        figuresArray.loadFigures(sr, storage);
                    }
                }
                catch
                {
                    MessageBox.Show("We can not load objects from file");
                }
            }
        }

        public void UpdateFromModel(object sender, EventArgs e)
        {
            flpSz.Visible = false;
            flpP1.Visible = false;
            flpP2.Visible = false;
            flpP3.Visible = false;

            int width = model.getCanvWidth();
            int height = model.getCanvHeight();

            //canvas.Size = new Size(width, height);
            canvas.Height = height;
            canvas.Width = width;

            numPosX.Maximum = width;
            nump2X.Maximum = width;
            nump3X.Maximum = width;
            numWdt.Maximum = width;
            numPosY.Maximum = height;
            nump2Y.Maximum = height;
            nump3Y.Maximum = height;
            numHgh.Maximum = height;


            numThck.ValueChanged -= new EventHandler(numThck_ValueChanged);
            if (model.getFigure(0) != null)
                numThck.Value = model.getFigure(0).getThickness();
            else
                numThck.Value = model.getThick();
            numThck.ValueChanged += new EventHandler(numThck_ValueChanged);


            if (model.getFigure(0) == null)
            {
                Invalidate();
                return;
            }

            if (model.handles_set() == false)
            {
                AFigure figure = model.getFigure(0);


                if (figure is Section || figure is Triangle)
                {
                    numPosX.ValueChanged -= new EventHandler(numP1_ValueChanged);
                    numPosY.ValueChanged -= new EventHandler(numP1_ValueChanged);
                    nump2X.ValueChanged -= new EventHandler(numP2_ValueChanged);
                    nump2Y.ValueChanged -= new EventHandler(numP2_ValueChanged);

                    numPosX.Value = figure.getP1().X;
                    numPosY.Value = figure.getP1().Y;
                    nump2X.Value = figure.getP2().X;
                    nump2Y.Value = figure.getP2().Y;

                    nump2X.ValueChanged += new EventHandler(numP2_ValueChanged);
                    nump2Y.ValueChanged += new EventHandler(numP2_ValueChanged);
                    numPosX.ValueChanged += new EventHandler(numP1_ValueChanged);
                    numPosY.ValueChanged += new EventHandler(numP1_ValueChanged);

                    flpP1.Visible = true;
                    flpP2.Visible = true;

                    if (figure is Triangle)
                    {
                        nump3X.ValueChanged -= new EventHandler(numP3_ValueChanged);
                        nump3Y.ValueChanged -= new EventHandler(numP3_ValueChanged);

                        nump3X.Value = figure.getP3().X;
                        nump3Y.Value = figure.getP3().Y;
                        flpP3.Visible = true;

                        nump3X.ValueChanged += new EventHandler(numP3_ValueChanged);
                        nump3Y.ValueChanged += new EventHandler(numP3_ValueChanged);
                    }
                }
                else if (!(figure is Group))
                {
                    numWdt.ValueChanged -= new EventHandler(setSize);
                    numHgh.ValueChanged -= new EventHandler(setSize);

                    numWdt.Value = Math.Abs(figure.getP2().X - figure.getP1().X);
                    numHgh.Value = Math.Abs(figure.getP2().Y - figure.getP1().Y);

                    numWdt.ValueChanged += new EventHandler(setSize);
                    numHgh.ValueChanged += new EventHandler(setSize);

                    flpSz.Visible = true;
                }
            }


            Invalidate();

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
            storage.paint(e.Graphics);
        }
        private void canvas_Click(object sender, EventArgs e)
        {
            Point mousePos = PointToClient(new Point(Cursor.Position.X - (sender as Panel).Location.X, Cursor.Position.Y - (sender as Panel).Location.Y));
            model.mouseClickGetting(mousePos);    
        }


        private void btnArw_Click(object sender, EventArgs e)
        {
            model.setBtn((sender as Button).Name);
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
            model.setCol((sender as Button).BackColor);
        }
        private void btnBlue_Click(object sender, EventArgs e)
        {
            model.setCol((sender as Button).BackColor);
        }
        private void btnGrn_Click(object sender, EventArgs e)
        {
            model.setCol((sender as Button).BackColor);
        }
        private void numThck_ValueChanged(object sender, EventArgs e)
        {
            model.setThick((int)(sender as NumericUpDown).Value);
        }

        private void numP1_ValueChanged(object sender, EventArgs e)
        {
            model.setPos(new Point((int)numPosX.Value, (int)numPosY.Value), '1');
        }
        private void numP2_ValueChanged(object sender, EventArgs e)
        {
            model.setPos(new Point((int)nump2X.Value, (int)nump2Y.Value), '2');
        }
        private void numP3_ValueChanged(object sender, EventArgs e)
        {
            model.setPos(new Point((int)nump3X.Value, (int)nump3Y.Value), '3');
        }
        private void setSize(object sender, EventArgs e)
        {
            model.setSize((int)(numWdt.Value), (int)(numHgh.Value));
        }



        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            model.buttonPressedGetting(e.KeyCode);
            e.Handled = true;
        }
        private void btnTrsh_Click(object sender, EventArgs e)
        {
            model.delete();
        }
        private void btnGroup_Click(object sender, EventArgs e)
        {
            model.group();
        }
        private void btnUngroup_Click(object sender, EventArgs e)
        {
            model.ungroup();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            model.clear();
        }
        private void setCanvasSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopupCanvSetSize popup = new PopupCanvSetSize(new PopupDelegate (popupFunc));
            popup.Owner = this;
            model.canvSetSizeSend(popup);
            popup.ShowDialog();
        }
        public void popupFunc(PopupCanvSetSize popup)
        {
            model.canvSetSizeReceive(popup);
        }


    }
}