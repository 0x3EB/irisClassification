using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Program
    {
        private static int _yValue;
        private static int _minAccuracy;
        private static int _maxAccuracy;
        private static int _minIndividuals;
        private static int _maxTreeSize;

        private static void Main(string[] args)
        {
            AskUserValues(); 
        }

        private static void AskUserValues()
        {
            Console.WriteLine("Y value to predict?");
            _yValue = CheckPositive();
            Console.WriteLine("Minimum accuracy?");
            _minAccuracy = CheckPercentage();
            Console.WriteLine("Maximum accuracy?");
            _maxAccuracy = CheckPercentage();
            Console.WriteLine("Minimum number of individuals?");
            _minIndividuals = CheckPositive();
            Console.WriteLine("Maximum tree size?");
            _maxTreeSize = CheckPositive();
        }
       
        // Check for user to enter a valid percentage
        private static int CheckPercentage()
        {
            do
            {
                var percentage = Convert.ToInt32(Console.ReadLine());
                if (percentage < 0 || percentage > 100)
                {
                    Console.WriteLine("Wrong percentage");
                }

                return percentage;
            } while (true);
        }

        // Check for user to enter a positive number
        private static int CheckPositive()
        {
            do
            {
                var positive = Convert.ToInt32(Console.ReadLine());
                if (positive < 0)
                {
                    Console.WriteLine("Enter a positive number");
                }

                return positive;
            } while (true);
        }
    }
}
