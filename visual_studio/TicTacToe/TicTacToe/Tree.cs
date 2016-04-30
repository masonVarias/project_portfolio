using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
   public class OutcomeTree
   {
       Leaf Head;
       public OutcomeTree(char[,]board,int turn)
       {
           this.Head = new Leaf(turn,board,-1,-1,true);
       }

       public int RowChoice
       {
           get { return Head.RowNext; }
       }
       public int ColChoice
       {
           get { return Head.ColNext; }
       }

   }//end outcometree---------------

    public class Leaf
    {
        char[,]currBoard;
        LinkedList<Leaf> children;
        int row; int rowNext; int col; int colNext;
        int val;
    
        
        public Leaf(int turn,char[,]board,int r, int c, bool AI)
        {
            row =r;
            col = c;
            currBoard = board;
            children = new LinkedList<Leaf>();

            //-------------base
            if (isWinner('x', currBoard))
                val = -10 +turn;

            else if (isWinner('o', currBoard))
                val = 10 -turn;

            else if (turn == 9)
                val = 0;
            //----------------

            else
            {
                char piece = 'x';
                if (AI)
                    piece = 'o';

                for (int i = 0; i < 3 ; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (currBoard[i, j] == '\0')
                        {
                            currBoard[i, j] = piece;
                            AddChild(new Leaf(++turn, currBoard, i, j, !AI));
                            currBoard[i, j] = '\0';
                            turn--;
                        }
                    }
                }
                if (AI)
                    MaxChoice();
                else
                    MinChoice();

                //if -1
                //failed
            }
        }

        public void AddChild(Leaf child)
        {
            children.AddLast(child);
        }

        public void MaxChoice()
        {
            int max = -100;
            int r = -1;
            int c = -1;

            foreach(Leaf cur in children)
            {
                if (cur.Val > max)
                {
                    max = cur.Val;
                    r = cur.Row;
                    c = cur.Col;
                }
            }

                this.Val = max;
                this.RowNext = r;
                this.ColNext = c;
            
        }

        public void MinChoice()
        {
            int min = 100;
            int r = -1;
            int c = -1;

            foreach (Leaf cur in children)
            {
                if (cur.Val < min)
                {
                    min = cur.Val;
                    r = cur.Row;
                    c = cur.Col;
                }
            }

                this.Val = min;
                this.RowNext = r;
                this.ColNext = c;
            
        }


        //---------------props
        public int Val
        { 
            get { return val; } set { val = value; } 
        }

        public int Row
        {
            get { return row; }
        }
        public int Col
        {
            get { return col; }
        }
        public int RowNext
        {
            get { return rowNext; }
            set { this.rowNext = value; }
        }
        public int ColNext
        {
            get { return colNext; }
            set { this.colNext = value; }
        }

        //-------------------------------------------check for winner
        public bool isWinner(char passed, char[,] board)
        {
            if (board[0, 0] == passed && board[1, 1] == passed && board[2, 2] == passed)
                return true;

            if (board[2, 0] == passed && board[1, 1] == passed && board[0, 2] == passed)
                return true;
            //------------------------------------diagonal

            if (board[0, 0] == passed && board[0, 1] == passed && board[0, 2] == passed)
                return true;

            if (board[1, 0] == passed && board[1, 1] == passed && board[1, 2] == passed)
                return true;

            if (board[2, 0] == passed && board[2, 1] == passed && board[2, 2] == passed)
                return true;
            //----------------------------vertical

            if (board[0, 0] == passed && board[1, 0] == passed && board[2, 0] == passed)
                return true;

            if (board[0, 1] == passed && board[1, 1] == passed && board[2, 1] == passed)
                return true;

            if (board[0, 2] == passed && board[1, 2] == passed && board[2, 2] == passed)
                return true;
            //-------------------------------horizontal

            return false;
        }

    }
}
