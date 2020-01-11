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
            // model.AskParameters(true);
            double[] newIris = {4.9, 3.0, 1.4, 0.2};
            var model1 = new Model(file, 1);
            model1.Build();
            Console.WriteLine("prediction for type 1 : " + model1.Predict(newIris));
             Console.WriteLine("*******************");
             model1.DisplayTree(2);
             var model2 = new Model(file, 2);
             model2.Build();
             
             Console.WriteLine("prediction for type 2 : " + model2.Predict(newIris));
             Console.WriteLine("*******************");
             model2.DisplayTree(2);
             var model3 = new Model(file, 3);
             model3.Build();
             Console.WriteLine("prediction for type 3 : " + model3.Predict(newIris));
             model3.DisplayTree(2);
            // var newIris = Model.AskTestIris();
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
