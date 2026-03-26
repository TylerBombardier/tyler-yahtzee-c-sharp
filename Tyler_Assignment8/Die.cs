using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyler_Assignment8.GameLogic
{
    // This class is for rolling die
    internal class Die
    {
        const int NumberOfSides = 6;

        public int Id { get; set; }
        public int Value { get; set; }

        public Die(int id)
        {
            Id = id;
            Value = 0;
        }

        public static Die[] GetXNumberOfDie(int num)
        {
            Die[] dice = new Die[num];

            for(int i = 0; i < num; i++)
            {
                dice[i] = new Die(i);
            }

            return dice;
        }

        public void Roll(Random random)
        {
            Value = random.Next(1, NumberOfSides + 1);
        }

        public static Die operator +(Die d1, Die d2)
        {
            Die d = new Die(0);
            d.Value = d1.Value + d2.Value;
            return d;
        }

        public override string ToString()
        {
            return $"{Value} ";
        }
    }
}
