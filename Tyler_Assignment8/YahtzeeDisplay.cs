using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyler_Assignment8.GameLogic;

namespace Tyler_Assignment8.DisplayLogic
{
    // This class handles the display logic for yahtzee
    internal class YahtzeeDisplay
    {
        public YahtzeeDisplay() { }

        // Displays the round number at the start of each round
        public void RoundNumber(int roundNumber)
        {
            Console.Write($"\n--- Round {roundNumber} ---\n");
        }

        // Displays which die was rolled
        public void RollingDie(int[] rolledDie)
        {
            Console.Write("\nRolling Dice... ");

            foreach (int i in rolledDie)
            {
                Console.Write($"{i+1}, ");
            }

            Console.WriteLine();
        }

        // Displays the outcome of the rolled die
        public void DiceOutcomes(Die[] dice)
        {
            Console.Write("\nYou rolled the following:\n");
            foreach (Die die in dice)
            {
                Console.Write($"D{die.Id+1}: {die.Value} ");
            }
            Console.WriteLine();
        }

        // Displays the score-card with the current scores for each category
        public void CategoryScores(Dictionary<string, int?> scores, Die[] dice = null, YahtzeeBoard logic = null)
        {
            Console.WriteLine($"\n{"Category:",-12} {"Points:",-12}");

            foreach (var entry in scores)
            {

                string category = entry.Key;
                int points = entry.Value ?? 0;

                if (dice != null && logic != null)
                {
                    Console.Write($"{category,-12} {points,-3} ({logic.calculateCategoryScore(entry.Key, dice)})");
                } else
                {
                    Console.Write($"{category,-12} {points,-3}");
                }

                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 15));
        }

        // Displays the prompt for the user to choose a category at the end of their rolls
        public void ChooseCategoryPrompt()
        {
            Console.WriteLine("\nChoose a category, or 'show' for score-card");
            Console.WriteLine("Choice (<category>/show): ");
        }

        // Displays the prompt for the user to choose a category, pick dice to re-roll, or show the score-card
        public void InputPrompt()
        {
            Console.WriteLine("\nSelect a category, pick the dice to re-roll (Ex. 1,2,3) or 'show' for score-card");
            Console.WriteLine("Choice (<category>/#,#,#/show): ");
        }

        // Displays the total score at the end of the game
        public void TotalScore(int totalScore)
        {
            Console.WriteLine($"Total: {totalScore}");
        }
    }
}
