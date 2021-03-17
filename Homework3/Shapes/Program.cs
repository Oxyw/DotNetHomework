using System;

namespace Shapes
{
    public interface Shape
    {
        double CalcuArea(); //计算面积
        bool IsLegal(); //判断是否合法
        double Area { get; } //获取面积
    }

    class Rectangle : Shape
    {
        private double length;
        private double width;
        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }
        public double CalcuArea()
        {
            if(IsLegal())
                return length * width;
            else
            {
                Console.WriteLine("形状不合法！");
                return 0;
            }
        }
        public bool IsLegal()
        {
            return length > 0 && width > 0;
        }
        public double Area
        {
            get
            {
                return CalcuArea();
            }
        }
    }

    class Square : Rectangle , Shape
    {
        private double side;
        public Square(double s) : base(s, s)
        {
            side = s;
        }
    }

    class Triangle : Shape
    {
        private double a, b, c; //三边长
        public Triangle(double x, double y, double z)
        {
            a = x; b = y; c = z;
        }
        public double CalcuArea()
        {
            if (IsLegal())
            {
                //海伦公式计算面积
                return System.Math.Sqrt((a + b + c) * (a + b - c) * (a + c - b) * (b + c - a)) / 4;
            }
            else
            {
                Console.WriteLine("形状不合法！");
                return 0;
            }
        }
        public bool IsLegal()
        {
            return a > 0 && b > 0 && c > 0 && a+b>c && b+c>a && a+c>b;
        }
        public double Area
        {
            get
            {
                return CalcuArea();
            }
        }
    }

    class ShapeFactory
    {
        public static Shape RandomlyGenerate()
        {
            Random rd = new Random();
            int key = rd.Next(0,3);
            Shape aShape = null;
            switch(key)
            {
                case 0: aShape = new Rectangle(rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10));
                    Console.WriteLine("This is a Rectangle.");
                    break;
                case 1: aShape = new Square(rd.NextDouble() + rd.Next(0, 10));
                    Console.WriteLine("This is a Square.");
                    break;
                case 2: aShape = new Triangle(rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10));
                    Console.WriteLine("This is a Triangle.");
                    break;
            }
            return aShape;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Now let's generate 10 shapes randomly.");
            double sumArea = 0;
            for (int i=1; i<=10; i++)
            {
                Console.Write($"{i}: ");
                Shape s = ShapeFactory.RandomlyGenerate();
                sumArea += s.Area;
                Console.WriteLine("This area is " + s.Area);
            }
            Console.WriteLine("The sum area of all the shapes is " + sumArea);
        }
    }
}
