using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tetris
{
    class BlockL1 : RotatingBlock
    {

        public BlockL1(Point piv, int or): base(piv,or)
        {
            this.color = Colors.LimeGreen;
            this.Val = 1;
        }

        public override Point[] getVert1(Point passed)
        {
            Point[] vert1 = new Point[4]{
                       new Point(passed.X-1,passed.Y),
                       passed,
                       new Point(passed.X+1,passed.Y),
                       new Point(passed.X+1,passed.Y+1)          
                    };
            return vert1;
        }

        public static Color wcSybil()
        {
            return Colors.LimeGreen;
        }

        public override Point[] getVert2(Point passed)
        {
            Point[] vert2 = new Point[4]{
                        new Point(passed.X-1,passed.Y-1),
                        new Point(passed.X-1,passed.Y), 
                        passed, 
                        new Point(passed.X+1,passed.Y)
                    };
            return vert2;
        }

        public override Point[] getHor2(Point passed)
        {
            Point[] hor = new Point[4]{
                    new Point(passed.X,passed.Y+1),
                    passed,
                    new Point(passed.X,passed.Y-1),
                    new Point(passed.X+1,passed.Y-1)
                    };
            return hor;
        }

        public override Point[] getHor1(Point passed)
        {
            Point[] hor = new Point[4]{
                    new Point(passed.X,passed.Y-1),
                    passed,
                    new Point(passed.X,passed.Y+1),
                    new Point(passed.X-1,passed.Y+1)
                    };
            return hor;
        }

    }
}
