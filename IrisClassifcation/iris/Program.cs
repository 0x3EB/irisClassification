using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                                          "(3) : Display tree\n" +
                                          "(4) : Display leafs\n" +
                                          "(5) : Predict\n" +
                                          "(0) : Exit";

        private static void Main(string[] args)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "iris.txt");
            var model = new Model(file, 1);
            model.AskParameters(true);
            //model.AskParameters();
            model.Build();
            bool exit = false;
            do
            {
                var choice = Model.CheckInt(err: "Wrong choice", min: 0, max: 5, message: MenuString);
                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        model.DisplayTreeHeight();
                        break;
                    case 2:
                        model.DisplayTreeWidth();
                        break;
                    case 3:
                        model.DisplayTree(1);
                        break;
                    case 4:
                        model.DisplayLeafs();
                        break;
                    case 5:
                        model.DisplayPredict(Model.AskTestIris());
                        break;
                    default:
                        exit = true;
                        break;
                        ;
                }
            } while (!exit);
        }
    }
}