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
            //var choice = Model.CheckInt(err: "Wrong choice", min: 1, max: 3, message: MenuString) ;
            // AskTestIris();
            var file = Path.Combine(Directory.GetCurrentDirectory(), "iris.txt");
            var model = new Model(file);
            model.AskParameters(true);
            model.Build();
            // double[] newIris = {0.5, 1.2, 4.1, 0.6};
            var newIris = Model.AskTestIris();
            Console.WriteLine(model.Predict(newIris));
            // model.GetTree().HierarchyPrint(model.GetTree().Root, 2);
            // var r = new readFile(file);
            // Console.WriteLine("test "+r.GetFile(121,5)[2,0]);
            // Tree<double> ab = new Tree<double>();
            //
            // Node<double> n1 = new Node<double>(1, null, null);
            // Console.WriteLine(ab.Height(n1));
            // Console.WriteLine(IsSampleDiv(ab, _maxTreeSize, n1, _minIndividuals, r.getNbLine(), _maxAccuracy, _minAccuracy, _irisType, r));
            // Console.ReadLine();
        }
    }
}
