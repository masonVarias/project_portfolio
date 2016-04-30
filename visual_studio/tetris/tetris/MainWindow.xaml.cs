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
using System.Windows.Threading;
using System.IO;

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] board = new int[18, 10];
        Rectangle[,]viewBoard;
        ParentBlock currBlock;
        ParentBlock nextBlock;
        DispatcherTimer timer;
        int score = 0;
        int highScore = 0;
        int rowsComp = 10;
        bool inGame;

        public MainWindow()
        {
            InitializeComponent();
            loadViewRec();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            startGame();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
            
        }
        public void startGame()
        {
            inGame = true;
            currBlock = getNewBlock();
            nextBlock = getNewBlock();
            colorCurr(currBlock.color);
            tb_score.Text = score.ToString();
            tb_level.Text = (rowsComp / 10).ToString();
 //           clearBoard();
                 
        }

        private void clearBoard()
        {
            board = new int[18, 10];
            for(int i =0; i < 18;i++)
            {
                for (int j = 0; j < 10; j++)
                    viewBoard[i, j].Fill = new SolidColorBrush(Colors.White);
            }
        }
        public void colorCurr(Color passed)
        {
            Point[] temp = currBlock.getBlocks();

             for(int i=0; i < 4; i++)
            {
                int x = (int)(temp[i].X);
                int y = (int)(temp[i].Y);
                 if(x > -1 && y> -1 && x < 18 && y < 10)
                viewBoard[x, y].Fill = new SolidColorBrush(passed);
            }
 
        }

        private void loadViewRec()
        {
            Rectangle[,] temp = { 
                                {rec_a, rec_a1, rec_a2, rec_a3, rec_a4, rec_a5, rec_a6, rec_a7, rec_a8, rec_a9 }, 
                                {rec_b, rec_b1, rec_b2, rec_b3, rec_b4, rec_b5, rec_b6, rec_b7, rec_b8, rec_b9 },
                                {rec_c,rec_c1,rec_c2,rec_c3,rec_c4,rec_c5,rec_c6,rec_c7,rec_c8,rec_c9},
                                {rec_d,rec_d1,rec_d2,rec_d3,rec_d4,rec_d5,rec_d6,rec_d7,rec_d8,rec_d9},
                                {rec_e,rec_e1,rec_e2,rec_e3,rec_e4,rec_e5,rec_e6,rec_e7,rec_e8,rec_e9},
                                {rec_f,rec_f1,rec_f2,rec_f3,rec_f4,rec_f5,rec_f6,rec_f7,rec_f8,rec_f9},
                                {rec_g,rec_g1,rec_g2,rec_g3,rec_g4,rec_g5,rec_g6,rec_g7,rec_g8,rec_g9},
                                {rec_h,rec_h1,rec_h2,rec_h3,rec_h4,rec_h5,rec_h6,rec_h7,rec_h8,rec_h9},
                                {rec_i,rec_i1,rec_i2,rec_i3,rec_i4,rec_i5,rec_i6,rec_i7,rec_i8,rec_i9},
                                {rec_j,rec_j1,rec_j2,rec_j3,rec_j4,rec_j5,rec_j6,rec_j7,rec_j8,rec_j9},
                                {rec_k,rec_k1,rec_k2,rec_k3,rec_k4,rec_k5,rec_k6,rec_k7,rec_k8,rec_k9},
                                {rec_l,rec_l1,rec_l2,rec_l3,rec_l4,rec_l5,rec_l6,rec_l7,rec_l8,rec_l9},
                                {rec_m,rec_m1,rec_m2,rec_m3,rec_m4,rec_m5,rec_m6,rec_m7,rec_m8,rec_m9},
                                {rec_n,rec_n1,rec_n2,rec_n3,rec_n4,rec_n5,rec_n6,rec_n7,rec_n8,rec_n9},
                                {rec_o,rec_o1,rec_o2,rec_o3,rec_o4,rec_o5,rec_o6,rec_o7,rec_o8,rec_o9},
                                {rec_p,rec_p1,rec_p2,rec_p3,rec_p4,rec_p5,rec_p6,rec_p7,rec_p8,rec_p9},
                                {rec_q,rec_q1,rec_q2,rec_q3,rec_q4,rec_q5,rec_q6,rec_q7,rec_q8,rec_q9},
                                {rec_r,rec_r1,rec_r2,rec_r3,rec_r4,rec_r5,rec_r6,rec_r7,rec_r8,rec_r9}
                                };
            viewBoard = temp;     
        }

        private void me_restart(object sender, RoutedEventArgs e)
        {
            me_song.Position = TimeSpan.FromMilliseconds(1);
        }

        private bool isValid(Point[]blocks)
        {
            foreach(Point curr in blocks)
            {
                if (curr.X < 0 || curr.X > 17 || curr.Y > 9 ||  curr.Y < 0)
                    return false;

                else if (board[(int)curr.X, (int)(curr.Y)] != 0)
                    return false;
            }
            return true;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
         {
             if (this.isValid(currBlock.prevdown()))
             {
                 colorCurr(Colors.White);
                 currBlock.move(1, 0);
                 colorCurr(currBlock.color);
             }

             else
             {
                 getNextBlock();
                 checkRows();
             }
        }

        private void checkRows()
        {
            int rowCount = 0;
            for (int i = 17; i > -1; i--)
            {
                if (rowFull(i))
                {
                    clearRow(i);
                    rowCount++;
                    i++;
                }
            }

            score += rowCount * (100 * (rowsComp/10) + ((rowCount -1) * 50));

            if (((rowsComp + rowCount) / 10) > (rowsComp / 10))
            {
                timer.Stop();
                timer.Interval= timer.Interval.Subtract(TimeSpan.FromMilliseconds(timer.Interval.TotalMilliseconds*.25));//increase speed--------------------
                timer.Start();
            }
            
            rowsComp += rowCount;
            tb_score.Text = score.ToString();
            tb_level.Text = (rowsComp / 10).ToString();
        }

        private void clearRow(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                board[row, i] = 0;
                viewBoard[row, i].Fill = new SolidColorBrush(Colors.White);
            }

            for(int x = row; x > 0; x--)
            {
                for(int y = 0; y < 10; y++)
                {
                    board[x, y] = board[x - 1, y];
                    viewBoard[x, y].Fill = viewBoard[x - 1, y].Fill;
                }
            }
        }
        private bool rowFull(int row)
        {
            for (int i = 0; i < 10; i++ )
            {
                if (board[row, i] == 0)
                    return false;
            }
            return true;
        }

        private void getNextBlock()
        {
            lockBlocks();
            currBlock = nextBlock;
            nextBlock = getNewBlock();
        }

        private void lockBlocks()
        {
            Point[] toLock = currBlock.getBlocks();
           
            foreach (Point cur in toLock)
            {
                if(cur.X < 0)
                {
                    endGame();
                }
                else
                    board[(int)cur.X, (int)cur.Y] = currBlock.Val;
            }

        }
        private void endGame()
        {
            inGame = false;
            timer.Stop();

            tb_status.Text = "Game Over";

            if (score > highScore)
                highScore = score;
        }
        private ParentBlock getNewBlock()
        {

            int tot = 7;
            Random rand = new Random();
            int val =rand.Next(0, tot);
            int config = rand.Next(0,5);
            int location = rand.Next(2,8);
            if(val ==0)
                return new BlockSquare(new Point(0, location),0);
            if (val == 1)
            {
                if (config == 1 || config == 3)
                    return new BlockLine(new Point(0, location), config);

                else
                    return new BlockLine(new Point(1, location), 2);
            }

            if (val == 2)
            {
                if (config == 0)
                    return new BlockT(new Point(0, location), config);
                else
                    return new BlockT(new Point(1, location), config);
            }

            //l1
            if(val ==3)
            {
                if (config == 3)
                    return new BlockL1(new Point(0, location), config);

                else
                    return new BlockL1(new Point(1, location), config);
            }

            //l2
            if(val==4)
            {
                if (config == 1)
                    return new BlockL2(new Point(0, location), config);

                else
                    return new BlockL2(new Point(1, location), config);
            }

            //z1
            if(val==5)
            {
                if (config % 2 == 0)
                    return new BlockZ1(new Point(0, location), config);

                else
                    return new BlockZ1(new Point(1, location), config);
            }

            //z2
            if(val==6)
            {
                if (config % 2 == 0)
                    return new BlockZ2(new Point(0, location), config);

                else
                    return new BlockZ2(new Point(1, location), config);
            }

            return new BlockSquare(new Point(-1 ,-1), -1);//----------never happen

        }

        private void shift_left(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                if (isValid(currBlock.prevLeft()) && timer.IsEnabled)
                {
                    colorCurr(Colors.White);
                    currBlock.move(0, -1);
                    colorCurr(currBlock.color);
                }
            }
        }

        private void shift_right(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                if (isValid(currBlock.prevRight()) && timer.IsEnabled)
                {
                    colorCurr(Colors.White);
                    currBlock.move(0, +1);
                    colorCurr(currBlock.color);
                }
            }
        }

        private void rotate_clock(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                if (isValid(currBlock.prevRotateClock()) && timer.IsEnabled)
                {
                    colorCurr(Colors.White);
                    currBlock.rotateClock();
                    colorCurr(currBlock.color);
                }
            }
        }

        private void rotate_counter(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                if (isValid(currBlock.prevRotateCounter()) && timer.IsEnabled)
                {
                    colorCurr(Colors.White);
                    currBlock.rotateCounter();
                    colorCurr(currBlock.color);
                }
            }

        }
        private void drop_block(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                if (timer.IsEnabled)
                    dispatcherTimer_Tick(new object(), new EventArgs());
            }
        }
        private void cheat(object sender, ExecutedRoutedEventArgs e)
        {
            rowsComp += 10;

            timer.Stop();
            timer.Interval = timer.Interval.Subtract(TimeSpan.FromMilliseconds(timer.Interval.TotalMilliseconds * .25));//increase speed--------------------
            timer.Start();
            tb_level.Text = (rowsComp / 10).ToString();
        }
        private void load_game(object sender, ExecutedRoutedEventArgs e)
        {
            if(!timer.IsEnabled)
            {
                Color[] colors = new Color[8] { Colors.White, BlockL1.wcSybil(), 
                                                BlockL2.wcSybil(), BlockLine.wcSybil(), 
                                                BlockSquare.wcSybil(), BlockT.wcSybil(), 
                                                BlockZ1.wcSybil(), BlockZ1.wcSybil() };

                if(File.Exists("save.txt"))
                {
                    String[] lines = File.ReadAllLines("save.txt");

                    for(int i = 0; i < 18; i++)
                    {
                        string[]temp = lines[i].Split(',');
                        for(int j = 0; j< 10; j++)
                        {
                            board[i, j] = int.Parse(temp[j]);
                            viewBoard[i, j].Fill = new SolidColorBrush(colors[board[i,j]]);
                        }
                    }
                    rowsComp=int.Parse(lines[18]);
                    score = int.Parse(lines[19]);
                    startGame();
                    timer.Stop();
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    for (int i = rowsComp / 10; i > 1; i-- )
                    {
                        timer.Interval = timer.Interval.Subtract(TimeSpan.FromMilliseconds(timer.Interval.TotalMilliseconds * .25));
                    }
                    tb_status.Text = "";    
                    timer.Start();                
                }
                
            }
        }
        private void save_game(object sender, ExecutedRoutedEventArgs e)
        {
            if(inGame && !timer.IsEnabled)
            {
                StreamWriter fout = new StreamWriter("save.txt");
                for(int i=0; i < 18;i++)
                {
                    for(int j=0; j< 10; j++)
                    {
                        fout.Write(board[i,j] + ",");
                    }
                    fout.WriteLine();
                }

                fout.WriteLine(rowsComp);
                fout.WriteLine(score);
                fout.Close();
            }
        }

        private void mute(object sender, RoutedEventArgs e)
        {
            me_song.Volume=0;
        }

        private void sound(object sender, RoutedEventArgs e)
        {
            me_song.Volume=.5;
        }

        private void pause_game(object sender, ExecutedRoutedEventArgs e)
        {
            if (inGame)
            {
                timer.IsEnabled = !timer.IsEnabled;
                if (timer.IsEnabled)
                    tb_status.Text = "";
                else
                    tb_status.Text = "Puased";
            }
        }

        private void mnu_controls_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("controls:\n" +
                            "left: shift left\n"+
                            "right: shift right\n" +
                            "z: rotate counter clockwise\n" +
                            "c: rotate clockwise\n"+
                            "p: pause\n" +
                            "down: drop blocks one space"+
                            "F5: save game when paused"+
                            "F8: load game"+
                            "Ctrl+ N: new game");
        }

        private void mnu_about1_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tetris by Mason Varias\n" +
                            "version 1.0\n" +
                            "high score: " + highScore.ToString());
        }

        private void newGame(object sender, ExecutedRoutedEventArgs e)
        {
            clearBoard();
            startGame();
            tb_status.Text = "";
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void toggle_sound(object sender, ExecutedRoutedEventArgs e)
        {
            if ((bool)(cb_sound.IsChecked))
                mute(new object(), new RoutedEventArgs());
            else
                sound(new object(), new RoutedEventArgs());
        }

        private void mnu_exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
