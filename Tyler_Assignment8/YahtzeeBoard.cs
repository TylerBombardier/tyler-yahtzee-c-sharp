using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyler_Assignment8.GameLogic;

namespace Tyler_Assignment8
{
    // This class handles the logic related to the Yahtzee score board
    internal class YahtzeeBoard
    {
        
        private Dictionary<string, int?> categories = new Dictionary<string, int?>();

        public YahtzeeBoard() 
        { 
            categories = new Dictionary<string, int?>
            {
                { "1s", null }, { "2s", null }, { "3s", null },
                { "4s", null }, { "5s", null }, { "6s", null },
                { "3 of kind", null }, { "4 of kind", null },
                { "full house", null }, { "sm straight", null },
                { "lg straight", null }, { "yahtzee", null },
                { "chance", null }
            };
        }

        // Remove all scores from the categories
        public void removeScores()
        {
            foreach (var key in categories.Keys.ToList())
            {
                categories[key] = null;
            }
        }

        // Gets the categories associated with scores
        public Dictionary<string, int?> getBoard()
        {
            return categories;
        }

        // Calculates the total score of all catagories
        public int getTotalScore()
        {
            int totalScore = categories.Values.Where(score => score.HasValue).Sum(score => score.Value);
            return totalScore;
        }

        // Checks if the category is scored or not and returns a boolean if true or false
        public bool isCategoryScored(string category_name)
        {
            if (categories[category_name] == null)
            {
                return true;
            }
            else // If that category was already used for points, return false
            {
                return false;
            }
        }

        // Checks for a straight in a unique sorted list
        private bool CheckStraight(List<int> uniqueSortedList, int length)
        {
            int count = 1;

            for (int i = 1; i < uniqueSortedList.Count; i++)
            {
                if (uniqueSortedList[i] == uniqueSortedList[i - 1] + 1)
                {
                    count++;
                    if (count >= length)
                        return true;
                }
                else
                {
                    count = 1;
                }
            }

            return false;
        }

        // Calculates a score with a given category and a hand of dice
        public int calculateCategoryScore(string category, Die[] dice)
        {
            var values = dice.Select(d => d.Value).ToList();
            var counts = values.GroupBy(v => v).ToDictionary(g => g.Key, g => g.Count());

            switch (category.ToLower())
            {
                // Upper section
                case "1s": return values.Where(v => v == 1).Sum();
                case "2s": return values.Where(v => v == 2).Sum();
                case "3s": return values.Where(v => v == 3).Sum();
                case "4s": return values.Where(v => v == 4).Sum();
                case "5s": return values.Where(v => v == 5).Sum();
                case "6s": return values.Where(v => v == 6).Sum();

                // 3 of a kind
                case "3 of kind":
                    if (counts.Any(c => c.Value >= 3))
                    {
                        return values.Sum();
                    }  
                    return 0;

                // 4 of a kind
                case "4 of kind":
                    if (counts.Any(c => c.Value >= 4))
                    { 
                        return values.Sum(); 
                    }
                    return 0;

                // Full house (3 + 2)
                case "full house":
                    if (counts.Count == 2 && counts.Any(c => c.Value == 3))
                    {
                        return 25;
                    }
                    return 0;

                // Small straight (4 in a row)
                case "sm straight":
                    var unique = values.Distinct().OrderBy(v => v).ToList();
                    if (CheckStraight(unique, 4))
                    {
                        return 30;
                    }
                    return 0;

                // Large straight (5 in a row)
                case "lg straight":
                    unique = values.Distinct().OrderBy(v => v).ToList();
                    if (CheckStraight(unique, 5))
                    {
                        return 40;
                    }
                    return 0;

                // Yahtzee (5 of a kind)
                case "yahtzee":
                    if (counts.Any(c => c.Value == 5))
                    {
                        return 50;
                    }

                    return 0;

                // Chance
                case "chance":
                    return values.Sum();

                default:
                    return 0;
            }
        }
    }
}
