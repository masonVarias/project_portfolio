using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tetris
{
    abstract class ParentBlock
    {
        private Color blockcolor;
        private Point pivot;
        private int val;
        public ParentBlock(Point piv,int or)
        {
            pivot = piv;
        }

        public abstract Point[] prevLeft();
        public abstract Point[] prevRight();
        public abstract Point[] prevdown();
        public abstract Point[] prevRotateClock();
        public abstract Point[] prevRotateCounter();
        public abstract void rotateClock();
        public abstract void rotateCounter();

        public  void move(int x, int y)
        {
            this.Pivot = new Point(this.Pivot.X + x, this.Pivot.Y + y);
        }

        public abstract Point[] getBlocks();

        public Point Pivot
        { get { return this.pivot; } set { this.pivot = value; } }

        public Color color
        { get { return this.blockcolor; } set { this.blockcolor = value; } }

        public int Val
        { get { return this.val; } set { this.val = value; } }

    }
}
