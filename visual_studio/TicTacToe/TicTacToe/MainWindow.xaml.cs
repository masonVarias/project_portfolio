using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//mason varias
//tic tac toe assignment
//
//ec: did the smart single player
namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[,] board;
        int turns;
        bool p1Turn;
        bool won;
        bool singlePLayer= true;

        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }
        
        private void clearBoard()
        {
            board = new char[3, 3];
            TL.Children.Clear();
            TC.Children.Clear();
            TR.Children.Clear();

            ML.Children.Clear();
            MC.Children.Clear();
            MR.Children.Clear();

            BL.Children.Clear();
            BC.Children.Clear();
            BR.Children.Clear();

            TurnImage.Children.Clear();
        }
        private void newGame()
        {
            clearBoard();
            turns = 0;
            won = false;
            if (OneGoesFirst())
            {
                p1Turn = true;
                drawX(TurnImage);
            }
            else
            {
                if (!singlePLayer)
                {
                    p1Turn = false;
                    drawO(TurnImage);
                }
                else
                    AIPlace(); 
                
            }
        }

        private bool OneGoesFirst()
        {
            Random rnd = new Random();
            int temp = rnd.Next(101);
            if(temp % 2 == 0)
                return true;
            return false;
        }

        private void TL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(TL,0,0);
        }

        public void place(Canvas curr,int x, int y)
        {
            
            if (board[x, y] == '\0')
            {
                TurnImage.Children.Clear();
                if (p1Turn)
                {
                    drawX(curr);
                    drawO(TurnImage);
                    board[x, y] = 'x';
                }

                else if(!singlePLayer)
                {
                    drawO(curr);
                    drawX(TurnImage);
                    board[x, y] = 'o';
                }
                turns++;
                won = checkResults();
                p1Turn = !p1Turn;
                
                if (won)
                    disableBoard();
            }
            if (singlePLayer && !won)
                AIPlace();
          
        }

        private void AIPlace()
        {
            Canvas[] choices = { TL, TC, TR, ML, MC, MR, BL, BC, BR };
            
            OutcomeTree smart = new OutcomeTree(board, turns);
            drawO(choices[smart.RowChoice + smart.ColChoice * 3]);
            board[smart.RowChoice, smart.ColChoice] = 'o';

            p1Turn = true;
            turns++;
            won = checkResults();
            if (won)
                disableBoard();
        }
   

        private void disableBoard()
        {
            for(int i =0; i < 3; i ++)
            {
                for (int j = 0; j < 3; j++)
                    board[i, j] = 'z';
            }
        }
        private bool checkResults()
        {
            if (isWinner('x',board))
            {
                MessageBox.Show("X is the winner");
                return true;
            }
            else if(isWinner('o',board))
            {
                MessageBox.Show("0 is the winner");
                return true;
            }
            else if(turns ==8)
            {
                MessageBox.Show("DRAW");
                return true;
            }
            return false;
        }

        public bool isWinner(char passed,char[,]board)
        {
            if (board[0, 0]== passed && board[1, 1] == passed && board[2, 2] == passed)
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

        public void drawO(Canvas location)
        {
            Ellipse o = new Ellipse();
            o.Stroke = Brushes.Red;
            o.HorizontalAlignment = HorizontalAlignment.Stretch;
            o.VerticalAlignment = VerticalAlignment.Stretch;
            o.Width = 50;
            o.Height = 50;

            location.Children.Add(o);
        }

        public void drawX(Canvas location)
        {
            Line part1 = new Line();
            part1.Stroke = Brushes.DarkBlue;
            part1.X1 = 1;
            part1.X2 = 59;
            part1.Y1 = 1;
            part1.Y2 = 59;

            part1.StrokeThickness = 2;

            Line part2 = new Line();
            part2.Stroke = Brushes.DarkBlue;
            part2.X1 = 1;
            part2.X2 = 59;
            part2.Y1 = 59;
            part2.Y2 = 1;

            part2.StrokeThickness = 2;

            location.Children.Add(part1);
            location.Children.Add(part2);
        }

        private Color bgColor()
        {
            if (p1Turn)
                return Colors.LightBlue;
            return Colors.MistyRose;
        }

        private void highlightSquare(Canvas curr,int x,int y)
        {
            if(board[x,y]=='\0')
            {
                curr.Background = new SolidColorBrush(bgColor());
                curr.Opacity = .5;
            } 
        }

        private void TL_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(TL,0,0);
        }


        private void TC_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(TC, 1, 0);
        }


        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            Canvas curr = (Canvas) (e.Source);
            curr.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void TR_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(TR, 2, 0);
        }

        private void ML_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(ML, 0, 1);
        }

        private void MC_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(MC, 1, 1);
        }

        private void MR_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(MR, 2, 1);
        }

        private void BL_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(BL, 0, 2);
        }

        private void BC_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(BC, 1, 2);
        }

        private void BR_MouseEnter(object sender, MouseEventArgs e)
        {
            highlightSquare(BR, 2, 2);
        }

        private void TC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(TC,1,0);
        }

        private void TR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(TR,2,0);
        }

        private void ML_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(ML,0,1);
        }

        private void MC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(MC,1,1);
        }

        private void MR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(MR,2,1);
        }

        private void BL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(BL,0,2);
        }

        private void BC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(BC,1,2);
        }

        private void BR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            place(BR,2,2);
        }

        private void mnu_NP2_Click(object sender, RoutedEventArgs e)
        {
            singlePLayer = false;
            newGame();
        }

        private void btn_newGame_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(P1Choice.IsChecked))
                singlePLayer = true;
            else
                singlePLayer = false;

            newGame();
        }

        private void mnu_NP1_Click(object sender, RoutedEventArgs e)
        {
            singlePLayer = true;
            newGame();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("writen by mason varias\n" +
                            "verison: 1.0\n"+
                            "fetures:\n"+
                            "-unbeatable single player mode\n"+
                            "-two player mode");
        }
    }
}
