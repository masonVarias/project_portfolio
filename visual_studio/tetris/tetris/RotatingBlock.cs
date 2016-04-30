using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tetris
{
    abstract class RotatingBlock: ParentBlock
    {
        protected int orientation;

        public abstract Point[] getVert1(Point passed);
        public abstract Point[] getVert2(Point passed);
        public abstract Point[] getHor1(Point passed);
        public abstract Point[] getHor2(Point passed);
        

        public RotatingBlock(Point piv, int or):base(piv,or)
        {
            orientation = or;
        }

        public override Point[] prevLeft()
        {
            Point[] temp = getOrientation();
            for (int i = 0; i < 4; i++)
                temp[i] = new Point(temp[i].X, temp[i].Y - 1);

            return temp;
        }
        public override Point[] prevRight()
        {
            Point[] temp = getOrientation();
            for (int i = 0; i < 4; i++)
                temp[i] = new Point(temp[i].X, temp[i].Y + 1);

            return temp;
        }

        public override Point[] prevdown()
        {
            Point[] temp = getOrientation(); //changing default
            for (int i = 0; i < 4; i++)
                temp[i] = new Point(temp[i].X + 1, temp[i].Y);

            return temp;
        }

        public override Point[] getBlocks()
        {
            Point[] temp = new Point[4];
            getOrientation().CopyTo(temp, 0);
            return temp;
        }

        public override Point[] prevRotateClock()
        {
            Point[] temp;
            this.rotateClock();
            temp = this.getBlocks();
            this.rotateCounter();
            return temp;
        }

        public override Point[] prevRotateCounter()
        {
            Point[] temp;
            this.rotateCounter();
            temp = this.getBlocks();
            this.rotateClock();
            return temp;
        }

        public override void rotateCounter()
        {
            orientation++;
            orientation = orientation % 4;
        }

        public override void rotateClock()
        {
            orientation--;
            if (orientation < 0)
                orientation = 3;
        }

        private Point[] getOrientation()
        {
            if (orientation == 0)
                return getVert1(this.Pivot);
            if (orientation == 1)
                return getHor1(this.Pivot);
            if (orientation == 2)
                return getVert2(this.Pivot);
            else
                return getHor2(this.Pivot);
        }

    }
}
