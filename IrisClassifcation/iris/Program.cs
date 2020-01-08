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
        private const string MenuString = "(1) : Display tree height\n" +
                                          "(2) : Display tree width\n" +
                                          "(3) : Display tree";

        private static void Main(string[] args)
        {
            // AskParameters();
            // var choice = CheckInt(min: 1, max: 3, err: "Wrong choice", hook: () => Console.WriteLine(MenuString));
            // AskTestIris();
            var file = Path.Combine(Directory.GetCurrentDirectory(), "iris.txt");
            var model = new Model(file);
            foreach(var a in model.ResizedTree(1))
            {
                Console.WriteLine(a);
            }
            // var r = new readFile(file);
            // Console.WriteLine("test "+r.GetFile(121,5)[2,0]);
            // Tree<double> ab = new Tree<double>();
            //
            // Node<double> n1 = new Node<double>(1, null, null);
            // Console.WriteLine(ab.Height(n1));
            // Console.WriteLine(IsSampleDiv(ab, _maxTreeSize, n1, _minIndividuals, r.getNbLine(), _maxAccuracy, _minAccuracy, _irisType, r));
             Console.ReadLine();
        }
    }
}
