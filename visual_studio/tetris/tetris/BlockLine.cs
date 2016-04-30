using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tetris
{
    class BlockLine : RotatingBlock
    {

        public BlockLine(Point passed,int or) :base(passed,or)
        {
           
            this.color = Colors.Red;
            this.Val = 3;
        }

        public static Color wcSybil()
        {
            return Colors.Red;
        }

        public override Point[] getVert1(Point passed)
        {
            Point[] vert1 = new Point[4]{
                        new Point(passed.X-2,passed.Y),
                        new Point(passed.X-1,passed.Y), 
                        passed, 
                        new Point(passed.X+1,passed.Y)
                    };
            return vert1;
        }

        public override Point[] getVert2(Point passed)
        {
            return this.getVert1(passed);
        }

        public override Point[] getHor1(Point passed)
        {
            Point[] hor1 = new Point[4]{
                    new Point(passed.X,passed.Y-2),
                    new Point(passed.X,passed.Y-1),
                    passed,
                    new Point(passed.X,passed.Y+1)
                    };
            return hor1;
        }

        public override Point[] getHor2(Point passed)
        {
            return this.getHor1(passed);
        }

    }
}
