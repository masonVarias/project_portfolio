using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace tetris
{
    class BlockSquare : ParentBlock
    {
       
       public BlockSquare(Point piv, int or) : base(piv,or)
        {
            this.color= Colors.Aqua;
            this.Val = 4;
        }

       public static Color wcSybil()
       {
           return Colors.Aqua;
       }

       public override Point[] getBlocks()
       {
           Point piv = this.Pivot;
           Point[] blocks = new Point[4];
           blocks[0] = piv;
           blocks[1] = new Point(piv.X, piv.Y + 1);
           blocks[2] = new Point(piv.X + 1, piv.Y);
           blocks[3] = new Point(this.Pivot.X + 1, piv.Y + 1);

           return blocks;
       }

       public override Point[] prevdown()
       {
           return previewChange(1, 0);
       }

       public override Point[] prevLeft()
       {
           return previewChange(0, -1);
       }

        public override Point[] prevRight()
       {
           return previewChange(0, 1);
       }

        private Point[] previewChange(int x, int y)
       {
           Point[] prev = this.getBlocks();
           for (int i = 0; i < 4; i++)
           {
               prev[i] = new Point(prev[i].X + x, prev[i].Y + y);
           }
            return prev;
       }

        public override void rotateClock()
        { }

        public override void rotateCounter()
        { }
        
        public override Point[] prevRotateClock()
        {
            return this.getBlocks();
        }
        public override Point[] prevRotateCounter()
        {
            return this.getBlocks();
        }

    }
}
