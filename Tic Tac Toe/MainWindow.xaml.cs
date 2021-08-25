using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private Marktype[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        private void NewGame()
        {
            mResults = new Marktype[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = Marktype.Free;
            }

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            var textblock = (TextBlock)MainGrid.FindName("ResultText");
                
            textblock.Text = "Player 1";
            textblock.Foreground = Brushes.Blue;

            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var textblock = (TextBlock)MainGrid.FindName("ResultText");

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != Marktype.Free)
            {
                return;
            }

            mResults[index] = mPlayer1Turn ? Marktype.Cross : Marktype.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            if (mPlayer1Turn)
            {
                textblock.Text = "Player 2";
                textblock.Foreground = Brushes.Red;
            }
            else
            {
                textblock.Text = "Player 1";
                textblock.Foreground = Brushes.Blue;
            }

            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            var textblock = (TextBlock)MainGrid.FindName("ResultText");

            #region Horizontal wins
            // Check for horizontal wins
            // Row 0

            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }

            // Row 1

            if (mResults[3] != Marktype.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }

            // Row 2

            if (mResults[6] != Marktype.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }
            #endregion

            #region Vertical wins
            // Check for vertical wins
            // Column 0

            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }

            // Column 1

            if (mResults[1] != Marktype.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }

            // Column 2

            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }
            #endregion

            #region Diagonal wins
            // Check for diagonal wins
            // Diagonal 1

            if (mResults[0] != Marktype.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }

            // Diagonal 1

            if (mResults[2] != Marktype.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;

                textblock.Text = "Winner !!";
                textblock.Foreground = Brushes.Green;
            }
            #endregion

            #region No winners
            // Check for no winner and full board
            if (!mResults.Any(result => result == Marktype.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;

                    textblock.Text = "Draw !!";
                    textblock.Foreground = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
