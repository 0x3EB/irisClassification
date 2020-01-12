using System.IO;

namespace iris
{
    internal class Program
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
            var model = new Model(file, 2);
            model.AskParameters(true);
            //model.AskParameters();
            model.Build();
            var exit = false;
            // double[] iris1 = {7.7, 3.0, 6.1, 2.3};
            // model.DisplayPredict(iris1);
            // double[] iris2 = {6.2, 2.8, 4.8, 1.8};
            // model.DisplayPredict(iris2);
            // double[] iris3 = {5.5, 2.5, 4.0, 1.3};
            // model.DisplayPredict(iris3);
            // double[] iris4 = {6.7, 3.3, 5.7, 2.5};
            // model.DisplayPredict(iris4);
            // double[] iris5 = {6.0, 2.2, 5.0, 1.5};
            // model.DisplayPredict(iris5);
            // double[] iris6 = {6.0, 2.7, 5.1, 1.6};
            // model.DisplayPredict(iris6);
            // double[] iris7 = {5.7, 2.6, 3.5, 1.0};
            // model.DisplayPredict(iris7);
            // double[] iris8 = {5.8, 2.6, 4.0, 1.2};
            // model.DisplayPredict(iris8);
            // double[] iris9 = {5.1, 3.4, 1.5, 0.2};
            // model.DisplayPredict(iris9);
            // double[] iris10 = {5.4, 3.9, 1.3, 0.4};
            // model.DisplayPredict(iris10);
            do
            {
                var choice = Model.CheckInt("Wrong choice", 0, 5, message: MenuString);
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
                }
            } while (!exit);
        }
    }
}