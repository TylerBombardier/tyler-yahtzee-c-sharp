using Tyler_Assignment8.DisplayLogic;
using Tyler_Assignment8.GameLogic;

/**
 * Name: Tyler Bombardier
 * Student Number: 0870190
 */
namespace Tyler_Assignment8
{
    namespace GameFlow
    { 
        internal class Program
        {
            static void Main(string[] args)
            {
                YahtzeeBoard board = new YahtzeeBoard();
                YahtzeeDisplay display = new YahtzeeDisplay();
                Random random = new Random();
                Die[] dice = Die.GetXNumberOfDie(5);

                for(int round = 1; round <= 13; round++)
                {
                    int[] diesToRoll = { 0, 1, 2, 3, 4 }; // Always roll 5 dice at the start of a round

                    int rollsLeft = 3; // Always start with 3 rolls at the start of a round

                    display.RoundNumber(round);

                    while (rollsLeft > 0)
                    {
                        // Displays which die was rolled
                        display.RollingDie(diesToRoll);

                        // Rolls each selected die
                        foreach(int dieIndex in diesToRoll)
                        {
                            dice[dieIndex].Roll(random);
                        }

                        // Displays what number the dices are
                        display.DiceOutcomes(dice);

                        // Displays the yahtzee board categories with points
                        display.CategoryScores(board.getBoard(), dice, board);

                        bool isInputting = true;

                        while (isInputting)
                        {
                            // Get user input
                            // Check if they chose a category, reroll, or show scoreboard
                            if(rollsLeft > 1)
                            {
                                display.InputPrompt();
                            }
                            else
                            {
                                display.ChooseCategoryPrompt();
                            }

                            string input = Console.ReadLine();

                            if(input == null || input.Trim().Length == 0)
                            {
                                Console.WriteLine("You didn't input anything.");
                            }
                            else if (input == "show")
                            {
                                // Show scoreboard, doesn't end round, doesn't stop inputting.
                                display.CategoryScores(board.getBoard());
                                display.TotalScore(board.getTotalScore());
                                Console.ReadLine();
                            }
                            else if ((input.Contains(',') || int.TryParse(input, out _)) && rollsLeft > 1)
                            {
                                try
                                {
                                    // Rolls the selected dice, doesn't end round, stops inputting.
                                    diesToRoll = input.Split(',').Select(s => int.Parse(s.Trim()) - 1).ToArray();

                                    if (diesToRoll.All(index => index >= 0 && index < 5))
                                    {
                                        isInputting = false;
                                        rollsLeft--;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nPlease select a die between 1-5");
                                    }

                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nInvalid Input: Please use only numbers and commas.");
                                }
                            }
                            else
                            {
                                // Selects the chosen category, calculates points, goes to next round, stops inputting.
                                if (board.getBoard().ContainsKey(input))
                                {
                                    if (board.isCategoryScored(input))
                                    {
                                        // calculate score to be added to that category
                                        board.getBoard()[input] = board.calculateCategoryScore(input, dice);
                                        isInputting = false;
                                        rollsLeft = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nThat category has already been scored");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nThat category doesn't exist");
                                }
                            }
                        }
                    }         
                }
            }
        }
    }
}
