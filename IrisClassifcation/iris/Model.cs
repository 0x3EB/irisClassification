using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iris
{
    public class Model
    {
        private Tree<double> _tree;

        // Default values
        private int _irisType = NoIrisType;
        private double _minAccuracy = 0.1;
        private double _maxAccuracy = 0.9;
        private int _minIndividuals = 10;
        private int _maxTreeSize = 50;
        private const double Tolerance = 0.000001;
        private const int NoColumn = -1;
        private const int NoIrisType = -1;
        private int Individuals { get; }
        private int Features { get; }

        /// <summary>
        /// Constructor of Model
        /// </summary>
        /// <param name="fileName"></param>
        public Model(string fileName)
        {
            var file = new File(fileName);
            Individuals = file.GetNbLine();
            Features = file.GetNbCol();
            _tree = new Tree<double>(new Node<double>(file.GetFile(), null, null));
        }

        /// <summary>
        /// Constructor of Model
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="irisType"></param>
        public Model(string fileName, int irisType) : this(fileName)
        {
            _irisType = irisType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>return _irisType</returns>
        public double GetIrisType()
        {
            return this._irisType;
        }

       /// <summary>
       /// Return the model tree
       /// </summary>
       /// <returns></returns>
        public Tree<double> GetTree()
        {
            return _tree;
        }

        /// <summary>
        /// Prompt the user to enter parameters in order to build the tree
        /// </summary>
        /// <param name="defaults"></param>
        public void AskParameters(bool defaults = false)
        {
            if (defaults)
            {
                _irisType = CheckInt(positive: true, err: "Enter positive number", message: "Y value to predict?");
            }
            else
            {
                _minAccuracy = CheckInt(min: 0, max: 100, err: "Wrong percentage", message: "Minimum accuracy?");
                _maxAccuracy = CheckInt(min: 0, max: 100, err: "Wrong percentage", message: "Maximum accuracy?");
                _minIndividuals = CheckInt(positive: true, err: "Enter positive number",
                    message: "Minimum number of individuals?");
                _maxTreeSize = CheckInt(positive: true, err: "Enter positive number", message: "Maximum tree size?");
            }
        }

        /// <summary>
        ///  Prompt the user to enter test iris data
        /// </summary>
        /// <returns></returns>
        public static double[] AskTestIris()
        {
            var features = Enum.GetNames(typeof(IrisFeatures));
            var iris = new double[features.Length];
            for (var i = 0; i < features.Length; ++i)
            {
                var name = features[i];
                iris[i] = CheckDouble(positive: true,
                    err: "Enter a positive number",
                    message: "Enter iris " + name);
            }

            return iris;
        }

        /// <summary>
        /// Build the tree according to the class properties
        /// </summary>
        public void Build()
        {
            Build(_tree.Root);
        }

        private double Predict(double[] newIris)
        {
            return Predict(newIris, _tree.Root);
        }

        /// <summary>
        /// Split a node in two children
        /// </summary>
        /// <param name="node"></param>
        /// <exception cref="IrisTypeNotSetException"></exception>
        private void Build(Node<double> node)
        {
            if (_irisType == NoIrisType) throw new IrisTypeNotSetException();
            // Console.WriteLine("acc " + SampleAccuracy(node.Array) +
            //                   ", individuals " + node.Array.GetLength(0));
            if (!IsSampleDiv(node)) return;

            var subSamples = BestSubSamples(node);
            if (subSamples == null) return;
            node.DivisionVar = subSamples.Item1;
            node.LChild = new Node<double>(subSamples.Item2, null, null);
            node.RChild = new Node<double>(subSamples.Item3, null, null);
            Build(node.LChild);
            Build(node.RChild);
        }

        private double Predict(IReadOnlyList<double> newIris, Node<double> node)
        {
            var median = CorrectedMedian(GetColumn(node.Array, node.DivisionVar));

            return !_tree.IsLeafNode(node)
                ? Predict(newIris, newIris[node.DivisionVar - 1] <= median ? node.LChild : node.RChild)
                : SampleAccuracy(node.Array);
        }

        /// <summary>
        /// Split 'node.Value' using the observing variable offering the best division
        /// Return a Tuple containing :
        /// - column number used to split the node,
        /// - left sub-sample
        /// - right sub-sample
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Tuple<int, double[,], double[,]> BestSubSamples(Node<double> node)
        {
            var column = NoColumn;
            double accuracy = 0;
            double[,] left = null;
            double[,] right = null;

            for (var i = 1; i < Features; ++i)
            {
                var currentSubSamples = Split2DArray(i, node);
                // Update best sub-samples given their accuracy
                var accuracyLeft = SampleAccuracy(currentSubSamples[0]);
                var accuracyRight = SampleAccuracy(currentSubSamples[1]);
                if (Math.Max(accuracyLeft, accuracyRight) > accuracy)
                {
                    accuracy = Math.Max(accuracyLeft, accuracyRight);
                    left = currentSubSamples[0];
                    right = currentSubSamples[1];
                    column = i;
                }
            }

            return Math.Abs(accuracy) < Tolerance
                ? null
                : new Tuple<int, double[,], double[,]>(column, left, right);
        }

        /// <summary>
        /// Split 2D array of node.Value in two subsets :
        /// Item1 : 2D array with all the values of 'nbCol' <= to the corrected median of this column
        /// Item2 : The remaining 2D array with all the values of 'nbCol' >= to the corrected median of this column
        /// </summary>
        /// <param name="nbCol"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static double[][,] Split2DArray(int nbCol, Node<double> node)
        {
            var column = GetColumn(node.Array, nbCol);
            var median = CorrectedMedian(column);
            var countLeft = column.Count(d => d <= median);
            var subSamples = new double[2][,];
            subSamples[0] = new double[countLeft, node.Array.GetLength(1)];
            subSamples[1] = new double[node.Array.GetLength(0) - countLeft, node.Array.GetLength(1)];
            for (int i = 0, iLeft = 0, iRight = 0; i < node.Array.GetLength(0); ++i)
            {
                if (node.Array[i, nbCol] <= median)
                {
                    for (var j = 0; j < node.Array.GetLength(1); ++j)
                    {
                        subSamples[0][iLeft, j] = node.Array[i, j];
                    }

                    ++iLeft;
                }
                else
                {
                    for (var j = 0; j < node.Array.GetLength(1); ++j)
                    {
                        subSamples[1][iRight, j] = node.Array[i, j];
                    }

                    ++iRight;
                }
            }

            return subSamples;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnNumber"></param>
        /// <returns>Return the columnNumber column of matrix</returns>
        public static double[] GetColumn(double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>Return the corrected median of tab sample</returns>
        public static double CorrectedMedian(double[] tab)
        {
            var copyTab = new double[tab.Length];
            Array.Copy(tab, copyTab, tab.Length);
            Array.Sort(copyTab);

            var midIndex = (copyTab.Length % 2 == 0)
                ? (copyTab.Length / 2) - 1
                : ((copyTab.Length + 1) / 2) - 1;

            var median = (copyTab.Length % 2 == 0)
                ? copyTab[midIndex] + copyTab[midIndex + 1] / 2
                : copyTab[midIndex];

            return
                (Math.Abs(median - copyTab.Last()) > Tolerance)
                    ? median
                    : tab[copyTab.Length - 2];
        }

        public static int CheckInt(string err = "Wrong value", int min = 0, int max = 0, bool negative = false,
            bool positive = false, string message = null)
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

        private static double CheckDouble(string err = "Wrong value", int min = 0, int max = 0, bool negative = false,
            bool positive = false, string message = null)
        {
            do
            {
                if (message != null)
                {
                    Console.WriteLine(message);
                }

                var res = Convert.ToDouble(Console.ReadLine());
                if ((positive && res < 0) || (negative && res > 0) ||
                    ((!positive && !negative) && (res < min || res > max)))
                {
                    Console.WriteLine(err);
                    continue;
                }

                return res;
            } while (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Return true if node.Value can divided, false otherwise</returns>
        private bool IsSampleDiv(Node<double> node)
        {
            var sampleAccuracy = SampleAccuracy(node.Array);
            if (IsMaxHeightReached())
                return false;
            if (node.Array.GetLength(0) < _minIndividuals)
                return false;
            if (sampleAccuracy < _minAccuracy || sampleAccuracy > _maxAccuracy)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return true if the max tree size has been reached</returns>
        private bool IsMaxHeightReached()
        {
            return _tree.Height(_tree.Root) >= _maxTreeSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns>Return accuracy of tab sample</returns>
        private double SampleAccuracy(double[,] tab)
        {
            var nbIrisType = 0;
            for (var i = 0; i < tab.GetLength(0); i++)
            {
                if (Math.Abs(tab[i, 0] - _irisType) < Tolerance)
                    nbIrisType++;
            }

            return nbIrisType / (double) (tab.GetLength(0));
        }

        /// <summary>
        /// Method for displaying the all tree with the accuracy, nb of individuals, 
        /// </summary>
        /// <param name="gap"></param>
        public void DisplayTree(int gap)
        {
            _tree.HierarchyPrint(_tree.Root, gap, x => SampleAccuracy(x.Array));
        }

        /// <summary>
        /// Method for displaying the tree height
        /// </summary>
        public void DisplayTreeHeight()
        {
            Console.WriteLine("The height of the tree is : " + _tree.Height(_tree.Root));
        }

        /// <summary>
        /// Method for displaying the tree width
        /// </summary>
        public void DisplayTreeWidth()
        {
            Console.WriteLine("The width of the tree is : " + _tree.NbLeaf(_tree.Root));
        }

        /// <summary>
        /// Method for displaying the prediction of the selected type
        /// </summary>
        /// <param name="newIris"></param>
        public void DisplayPredict(double[] newIris)
        {
            Console.WriteLine("The prediction for your selected type is : " + Predict(newIris));
        }

        /// <summary>
        /// Can display all the Leafs (with the path,Leaf Accuracy, and individuals count for each leaf)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="medianOfParent"></param>
        /// <param name="root"></param>
        private void Leafs(Node<double> node, double medianOfParent = 0, bool root = true)
        {
            if (node != null)
            {
                if (!_tree.IsLeafNode(node))
                {
                    if (!root)
                    {
                        Console.Write(" X" + (node.DivisionVar + 1) + CharOfOperation(medianOfParent, node) +
                                      medianOfParent + " - ");
                    }
                    else
                    {
                        Console.Write("PATH : ROOT - ");
                    }

                    Leafs(node.LChild, CorrectedMedian(GetColumn(node.Array, node.DivisionVar)), false);
                    Leafs(node.RChild, CorrectedMedian(GetColumn(node.Array, node.DivisionVar)), false);
                }
                else
                {
                    Console.Write("X" + (node.DivisionVar + 1) + CharOfOperation(medianOfParent, node) +
                                  medianOfParent);
                    Console.WriteLine(" | Leaf Accuracy : " + SampleAccuracy(node.Array) +
                                      ", individuals count :" + node.Array.GetLength(0));
                    Console.WriteLine("******************************");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medianParent"></param>
        /// <param name="node"></param>
        /// <returns>return the char of math comparison </returns>
        public static string CharOfOperation(double medianParent, Node<double> node)
        {
            if (medianParent > CorrectedMedian(GetColumn(node.Array, node.DivisionVar)))
                return ">";
            else if (medianParent >= CorrectedMedian(GetColumn(node.Array, node.DivisionVar)))
                return ">=";
            else if (medianParent < CorrectedMedian(GetColumn(node.Array, node.DivisionVar)))
                return "<";
            else
                return "=<";
        }

        /// <summary>
        /// Method for displaying all the leafs 
        /// </summary>
        public void DisplayLeafs()
        {
            Leafs(_tree.Root);
        }
    }
}