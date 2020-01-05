using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Program
    {
        // Default values
        private static int _irisType = 2;
        private static int _minAccuracy = 10;
        private static int _maxAccuracy = 95;
        private static int _minIndividuals = 65;
        private static int _maxTreeSize = 300;
        private const string MenuString = "(1) : Display tree height\n" +
                                          "(2) : Display tree width\n" +
                                          "(3) : Display tree";

        private static void Main(string[] args)
        {
            // AskParameters();
            // var choice = CheckInt(min: 1, max: 3, err: "Wrong choice", hook: () => Console.WriteLine(MenuString));
            // AskTestIris();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "iris.txt");
            var r = new readFile(path);
            Console.WriteLine("test "+r.GetFile(121,5)[2,0]);
            Tree<double> ab = new Tree<double>();
            
            Node<double> n1 = new Node<double>(1, null, null);
            Console.WriteLine(ab.Height(n1));
            Console.WriteLine(IsSampleDiv(ab, _maxTreeSize, n1, _minIndividuals, r.getNbLine(), _maxAccuracy, _minAccuracy, _irisType, r));
            Console.ReadLine();
        }

        private static void AskParameters()
        {
            _irisType = CheckInt(positive:true, err:"Enter positive number", message:"Y value to predict?");
            _minAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Minimum accuracy?");
            _maxAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Maximum accuracy?");
            _minIndividuals = CheckInt(positive:true, err:"Enter positive number", message:"Minimum number of individuals?");
            _maxTreeSize = CheckInt(positive:true, err:"Enter positive number", message:"Maximum tree size?");
        }
        
        private static int CheckInt(string err = "Wrong value",int min = 0, int max = 0, bool negative = false, bool positive = false, string message=null)
        {
            do
            {
                if (message != null)
                {
                    Console.WriteLine(message);
                }
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

        private static double[] AskTestIris()
        {
            var features = Enum.GetNames(typeof(IrisFeatures));
            var iris = new double[features.Length];
            for (var i = 0; i<features.Length; ++i)
            {
                var name = features[i];
                iris[i] = CheckInt(positive: true,
                    err: "Enter a positive number",
                    message: "Enter iris " + name);
            }
            return iris;
        }

        private static Boolean IsSampleDiv(Tree<double> tree, int maxTreeSize, Node<double> node, int nbMinIndivuals, int nbIndivuals, int maxAccuracy, int minAccuracy, int irisType, readFile r)
        {
            if (!IsMaxHeightReached(tree, maxTreeSize, node))
                return false;
            if (!MoreIndividuals(nbMinIndivuals, nbIndivuals))
                return false;
            if (!SampleAccuracy(maxAccuracy, minAccuracy, irisType, r))
                return false;
            return true;
        }

        private static Boolean IsMaxHeightReached(Tree<double> tree, int maxTreeSize, Node<double> node)
        {
            return tree.Height(node) < maxTreeSize;
        }

        private static Boolean MoreIndividuals(int nbMinIndivuals, int nbIndivuals)
        {
            return nbIndivuals > nbMinIndivuals;
        }

        private static Boolean SampleAccuracy(int maxAccuracy, int minAccuracy, int irisType, readFile r)
        {
            Convert.ToDouble(irisType);
            double[] tabIrisType = r.getDoubleCol(0);
            int nbIrisType = 0;
            for (int i=0;i<tabIrisType.Length-1;i++)
            {
                if (tabIrisType[i] == irisType)
                    nbIrisType++;
            }
            double accuracy = (double)(nbIrisType / (double)(tabIrisType.Length-1))*100;
            return (accuracy >= minAccuracy && accuracy <= maxAccuracy);
        }
    }
}
