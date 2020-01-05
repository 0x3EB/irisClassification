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
        private const string MenuString = "(1) : Display tree height\n" +
                                          "(2) : Display tree width\n" +
                                          "(3) : Display tree";

        private static void Main(string[] args)
        {
            // AskUserValues();
            var choice = CheckInt(min: 1, max: 3, err: "Wrong choice", hook: () => Console.WriteLine(MenuString));
        }

        private static void AskUserValues()
        {
            Console.WriteLine("Y value to predict?");
            _yValue = CheckInt(positive:true, err:"Enter positive number");
            Console.WriteLine("Minimum accuracy?");
            _minAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage");
            Console.WriteLine("Maximum accuracy?");
            _maxAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage");
            Console.WriteLine("Minimum number of individuals?");
            _minIndividuals = CheckInt(positive:true, err:"Enter positive number");
            Console.WriteLine("Maximum tree size?");
            _maxTreeSize = CheckInt(positive:true, err:"Enter positive number");
        }
        
        private static int CheckInt(string err = "Wrong value",int min = 0, int max = 0, bool negative = false, bool positive = false, Action hook=null)
        {
            do
            {
                hook?.Invoke();
                var res = Convert.ToInt32(Console.ReadLine());
                if ((positive && res < 0) || (negative && res > 0) ||
                    ((!positive && !negative) && (res < min || res > max)))
                {
                    Console.WriteLine(err);
                    continue;
                }
                return res;
            } while (true);
        }
        
    }
}
