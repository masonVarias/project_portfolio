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
using System.ComponentModel;
//mason varias
//notes:
//-minimum bet of 10
//-to enter the starting bank start the game, put the number (>10) into the bank textbox and press set money
namespace WPFCraps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool FirstRoll;
        int pnt = -1;
        int playerCount = 0;
        int houseCount = 0;
        int bank = 0;
        int bet = 0;

        public MainWindow()
        {
            InitializeComponent();
 //           this.Closing += new System.ComponentModel.CancelEventHandler(Closing);
        }

        private void menu_start_Click(object sender, RoutedEventArgs e)
        {
            if (menu_start.IsEnabled)
            {
                menu_reset.IsEnabled = true;
                FirstRoll = true;
                prepareBank();
                menu_start.IsEnabled = false;
            }
        }

        private void prepareBank()
        {
            btn_setMoney.IsEnabled = true;
            txt_player_bank.Text = "enter amount";
            txt_player_bank.Focusable = true;
            txt_player_bank.IsReadOnly = false;
        }

        private void finalizeBank()
        {
            btn_setMoney.IsEnabled = false;
            txt_player_bank.Text = bank.ToString();
            txt_player_bank.Focusable = false;
        }

        private void activateBets()
        {
            if(bank > 9)
                btn_bet_10.IsEnabled = true;
            
            if(bank > 99)
                btn_bet_100.IsEnabled = true;

            if(bank > 999)
                btn_bet_1000.IsEnabled = true;

            if(bank > 9999)
            btn_bet_10000.IsEnabled = true;
        }

        private void disableBets()
        {    
                btn_bet_10.IsEnabled = false;
                btn_bet_100.IsEnabled = false;
                btn_bet_1000.IsEnabled = false;
                btn_bet_10000.IsEnabled = false;
        }

        private void btn_roll_Click(object sender, RoutedEventArgs e)
        {
            if (btn_roll.IsEnabled)
            {
                disableBets();
                Random rand = new Random();

                int first = rand.Next(1, 7);
                txt_d1.Text = first.ToString();

                int second = rand.Next(1, 7);
                txt_d2.Text = second.ToString();

                int sum = first + second;
                txt_total.Text = sum.ToString();

                if (FirstRoll)
                    calcFirstRoll(sum);

                else
                    calcRoll(sum);
            }
        }


        private void calcFirstRoll(int sum)
        {
            btn_playAgain.IsEnabled = false;

            if (sum == 7 || sum == 11)
                playerWon();

            else if (sum == 2 || sum == 3 || sum == 12)
                playerLose();

            else
            {
                pnt = sum;
                txt_point.Text = pnt.ToString();
            }
            FirstRoll = false;
        }

        private void calcRoll(int sum)
        {

            if (sum == pnt)
                playerWon();

            else if (sum == 7)
                playerLose();

        }

        private void playerWon()
        {
            txt_outcome.Text = "Player Wins";
            btn_playAgain.IsEnabled = true;
            btn_roll.IsEnabled = false;
            playerCount++;
            txt_player_wins.Text = playerCount.ToString();

            bank = bank + bet * 2;
            bet = 0;
            txt_bet.Text = bet.ToString();
            txt_player_bank.Text = bank.ToString();

        }
        private void playerLose()
        {
            txt_outcome.Text = "House Wins";
            btn_roll.IsEnabled = false;
            houseCount++;
            txt_house_wins.Text = houseCount.ToString();

            bet = 0;
            txt_bet.Text = bet.ToString();

            if(bank < 10)
                MessageBox.Show("uh oh, you ran out of money!");
            
            else
                btn_playAgain.IsEnabled = true;
            
        }

        private void btn_playAgain_Click(object sender, RoutedEventArgs e)
        {
            if (btn_playAgain.IsEnabled)
            {
                btn_playAgain.IsEnabled = false;
                activateBets();
                txt_point.Text = "";
                btn_roll.IsEnabled = true;
                FirstRoll = true;
                txt_outcome.Text = "";
                txt_d1.Text = "";
                txt_d2.Text = "";
                txt_total.Text = "";
            }
        }

        private void menu_exit_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
            
        }

        private void menu_about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("author: mason\nversion:1.0\n.NET Framework version: 4.5\n32 bit prefered");
        }

        private void menu_rules_Click(object sender, RoutedEventArgs e)
        {
           string temp = "A player rolls two dice where each die has six faces in the usual way (values 1 through 6).\n";
            temp += "A player rolls two dice where each die has six faces in the usual way (values 1 through 6).\n";
            temp += "After the dice have come to rest the sum of the two upward faces is calculated.\n";
            temp+="The first roll\n";
            temp+="If the sum is 7 or 11 on the first throw the roller/player wins.\n";
            temp+="If the sum is 2, 3 or 12 the roller/player loses, that is the house wins.\n";
            temp+="If the sum is 4, 5, 6, 8, 9, or 10, that sum becomes the roller/player's \"point\".\n";
            temp+="Continue given the player's point\n";
            temp+="Now the player must roll the \"point\" total before rolling a 7 in order to win.\n";
            temp += "If they roll a 7 before rolling the point value they got on the first roll the roller/player loses (the 'house' wins).)\n";
            MessageBox.Show(temp);
        }

        private void menu_reset_Click(object sender, RoutedEventArgs e)
        {
            if (menu_reset.IsEnabled)
            {
                playerCount = 0;
                txt_player_wins.Text = playerCount.ToString();

                houseCount = 0;
                txt_house_wins.Text = houseCount.ToString();

                btn_playAgain_Click(new object(), new RoutedEventArgs());

                bank = 0;
                bet = 0;
                txt_bet.Text = bet.ToString();
                prepareBank();
                disableBets();
                btn_roll.IsEnabled = false;
            }
        }

        private void btn_setMoney_Click(object sender, RoutedEventArgs e)
        {
            string temp =txt_player_bank.Text;
            int val;
            bool isNum = int.TryParse(temp,out val);

            if (isNum && val > 9)
            {
                bank = val;
                finalizeBank();
                activateBets();
                btn_roll.IsEnabled = true;
                txt_player_bank.IsReadOnly = true;
                txt_player_bank.Focusable = false;
                btn_setMoney.IsEnabled = false;
            }
            else
                prepareBank();
        }

        private void btn_bet_10_Click(object sender, RoutedEventArgs e)
        {
            bank = bank - 10;
            bet = bet + 10;
            txt_bet.Text = bet.ToString();
            txt_player_bank.Text = bank.ToString();
            disableBets();
            activateBets();
        }

        private void btn_bet_100_Click(object sender, RoutedEventArgs e)
        {
            bank = bank - 100;
            bet = bet + 100;
            txt_bet.Text = bet.ToString();
            txt_player_bank.Text = bank.ToString();
            disableBets();
            activateBets();
        }

        private void btn_bet_1000_Click(object sender, RoutedEventArgs e)
        {
            bank = bank - 1000;
            bet = bet + 1000;
            txt_bet.Text = bet.ToString();
            txt_player_bank.Text = bank.ToString();
            disableBets();
            activateBets();
        }

        private void btn_bet_10000_Click(object sender, RoutedEventArgs e)
        {
            bank = bank - 10000;
            bet = bet + 10000;
            txt_bet.Text = bet.ToString();
            txt_player_bank.Text = bank.ToString();
            disableBets();
            activateBets();
        }

        private void window_close(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("are you sure you want to quit?", "Confirm exit", MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel=true;
        }
    }

}
