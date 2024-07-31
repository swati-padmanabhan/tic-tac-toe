namespace TicTacToe
{
    internal class Program
    {
        //to represent a grid, we use a string array with size 9
        //each element represents a cell in 3*3 grid
        static string[] grid = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the game of Tic Tac Toe!");
            Console.WriteLine("===================================");
            Console.WriteLine("Enter a number between 1 to 9 to make your move");
            Console.WriteLine("Player 1 is X and Player 2 is O.\n");


            //boolean to keep track of whose turn it is
            bool isPlayer1Turn = true;
            int numberOfTurns = 0;

            while (!CheckVictory() && numberOfTurns != 9)
            {
                //print the current state of the grid
                PrintGrid();
                Console.WriteLine(isPlayer1Turn ? "Player 1 Turn!" : "Player 2 Turn");

                try
                {
                    string choice = Console.ReadLine();

                    //convert the choice to an integer index for the grid
                    int gridIndex = Convert.ToInt32(choice) - 1;

                    //check if the choice is within the range
                    if (gridIndex < 0 || gridIndex > 8)
                    {
                        throw new InvalidChoiceException("Invalid Choice. " +
                            "Please Enter a Number Between 1 and 9");
                    }

                    //check if the selected cell is already occupied
                    if (grid[gridIndex] != "X" && grid[gridIndex] != "O")
                    {
                        grid[gridIndex] = isPlayer1Turn ? "X" : "O";
                        numberOfTurns++;
                    }
                    else
                    {
                        throw new InvalidChoiceException("This cell is already taken, " +
                            "Please choose another.");
                    }


                    //switch the turns
                    isPlayer1Turn = !isPlayer1Turn;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine("Please Enter Numbers Only! " + fe.Message);
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                }

            }
            //print the final grid
            PrintGrid();

            DisplayVictory(isPlayer1Turn);


        }
        //method to print the current state of the grid
        public static void PrintGrid()
        {
            //we loop through the rows to print its initial state
            //outer loop iterates over the rows
            for (int i = 0; i < 3; i++)
            {

                //inner loop interates over the columns
                for (int j = 0; j < 3; j++)
                {
                    //print column followed by a seperator, this will initially print 1|2|3
                    Console.Write(grid[i * 3 + j] + "|");
                }
                Console.WriteLine();
                Console.WriteLine("------");
            }
        }
        //method to check for winning combination
        public static bool CheckVictory()
        {
            //check all possible winning combinations
            bool row1 = grid[0] == grid[1] && grid[1] == grid[2];
            bool row2 = grid[3] == grid[4] && grid[4] == grid[5];
            bool row3 = grid[6] == grid[7] && grid[7] == grid[8];

            bool column1 = grid[0] == grid[3] && grid[3] == grid[6];
            bool column2 = grid[1] == grid[4] && grid[4] == grid[7];
            bool column3 = grid[2] == grid[5] && grid[5] == grid[8];

            bool diagonalDown = grid[0] == grid[4] && grid[4] == grid[8];
            bool diagonalUp = grid[6] == grid[4] && grid[4] == grid[2];

            //return true if any of the combinations is found
            return row1 || row2 || row3 || column1 || column2 || column3 || diagonalDown || diagonalUp;
        }

        public static void DisplayVictory(bool isPlayer1Turn)
        {
            if (CheckVictory())
            {
                // Display the winner based on the last player who made a move.
                if (isPlayer1Turn)
                    Console.WriteLine("Player 2 Wins!");
                else
                    Console.WriteLine("Player 1 Wins!");
            }
            else
            {
                Console.WriteLine("It's a Tie!");
            }
        }

    }
}
