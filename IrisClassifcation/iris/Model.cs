using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iris
{
    public class Model
    {
        private static Tree<double> _tree;
        // Default values
        private static int _irisType = 3;
        private static int _minAccuracy = 60;
        private static int _maxAccuracy = 95;
        private static int _minIndividuals = 10;
        private static int _maxTreeSize = 50;
        private const double Tolerance = 0.00000000000001;
        private const int NoColumn = -1;
        private int Individuals { get; }
        private int Feature { get; }
        
        public Model(string fileName)
        {
            var file = new readFile(fileName);
            Individuals = file.getNbLine();
            Feature = file.getNbCol();
            _tree = new Tree<double>(new Node<double>(file.GetFile(), null, null));
        }

        // Return the model tree
        public Tree<double> GetTree()
        {
            return _tree;
        }

        // Prompt the user to enter parameters in order to build the tree
        public void AskParameters()
        {
            _irisType = CheckInt(positive:true, err:"Enter positive number", message:"Y value to predict?");
            _minAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Minimum accuracy?");
            _maxAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Maximum accuracy?");
            _minIndividuals = CheckInt(positive:true, err:"Enter positive number", message:"Minimum number of individuals?");
            _maxTreeSize = CheckInt(positive:true, err:"Enter positive number", message:"Maximum tree size?");
        }

        // Prompt the user to enter test iris data
        public double[] AskTestIris()
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

        // Build the tree according to the class properties
        public void Build()
        {
            Split(_tree.Root);
            Console.WriteLine(_tree.NbDescendant(_tree.Root));
        }
        
        // Split a node in two children
        private void Split(Node<double> node)
        {
            if (!IsSampleDiv(node, node.Value.Length)) return;
            
            var subSamples = BestSubSamples(node);
            node.Lchild = new Node<double>(subSamples.Item2, null, null);
            node.Rchild = new Node<double>(subSamples.Item3, null, null);
            Console.WriteLine(SampleAccuracy(node.Value));
            Split(node.Lchild);
        }

        // Split 'node.Value' using the observing variable offering the best division
        // Return a Tuple containing :
        // - column number used to split the node,
        // - left sub-sample
        // - right sub-sample
        private static Tuple<int, double[,], double[,]> BestSubSamples(Node<double> node)
        {
            var colNumber = NoColumn;
            double accuracy = 0;
            double[,] left = null;
            double[,] right = null;

            for (var i = 0; i < node.Value.GetLength(1); ++i)
            {
                var subSamples = Split2DArray(i, node);
                // Update best sub-samples given their accuracy
                var currentAccuracy = SampleAccuracy(subSamples.Item1); 
                if (currentAccuracy > accuracy)
                {
                    accuracy = currentAccuracy;
                    left = subSamples.Item1;
                    right = subSamples.Item2;
                    colNumber = i;
                }
            }
            
            return new Tuple<int, double[,], double[,]>(colNumber, left, right);
        }
        
        // Split 2D array of node.Value in two subsets :
        // Item1 : 2D array with all the values of 'nbCol' <= to the corrected median of this column
        // Item2 : The remaining 2D array with all the values of 'nbCol' >= to the corrected median of this column
        private static Tuple<double[,], double[,]> Split2DArray(int nbCol, Node<double> node)
        {
            var column = GetColumn(node.Value, nbCol);
            var median = CorrectedMedian(column);
            var countLeft = column.Count(d => d <= median);
            var left = new double[countLeft, node.Value.GetLength(1)];
            var right = new double[node.Value.GetLength(0) - countLeft, node.Value.GetLength(1)];

            for (int i = 0, iLeft = 0, iRight = 0; i < node.Value.GetLength(0); ++i)
            {
                if (node.Value[i, nbCol] <= median)
                {
                    for (var j = 0; j < node.Value.GetLength(1); ++j)
                    {
                        left[iLeft, j] = node.Value[i, j];
                    }
                    ++iLeft;
                }
                else
                {
                    for (var j = 0; j < node.Value.GetLength(1); ++j)
                    {
                        right[iRight, j] = node.Value[i, j];
                    }
                    ++iRight;
                }
            }
            return new Tuple<double[,], double[,]>(left, right);
        }

        // Return the columnNumber column of matrix
        private static double[] GetColumn(double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        // Return the corrected median of tab sample
        private static double CorrectedMedian(double[] tab)
        {
            var median = Median(tab);
            return 
                (Math.Abs(median - tab.Last()) < Tolerance) 
                ? median 
                : tab[tab.Length - 2];
        }

        // Return the statistic median of tab sample
        private static double Median(double[] tab) {
            var copyTab = new double[tab.Length];
            Array.Copy(tab, copyTab, tab.Length);
            Array.Sort(copyTab);
            var mid = copyTab.Length / 2;
            return (copyTab.Length % 2 != 0) ? copyTab[mid] : copyTab[mid] + copyTab[mid + 1] / 2;
        }
        
        public static int CheckInt(string err = "Wrong value",int min = 0, int max = 0, bool negative = false, bool positive = false, string message=null)
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
        
        // Return true if node.Value can divided, false otherwise
        private bool IsSampleDiv(Node<double> node, int nbIndividuals)
        {
            var sampleAccuracy = SampleAccuracy(node.Value);
            if (IsMaxHeightReached())
                return false;
            if (nbIndividuals < _minIndividuals)
                return false;
            if (sampleAccuracy >= _minAccuracy && sampleAccuracy <= _maxAccuracy)
                return false;
            return true;
        }
        
        // Return true if the max tree size has been reached
        private static bool IsMaxHeightReached()
        {
            return _tree.Height(_tree.Root) >= _maxTreeSize;
        }

        // Return accuracy of tab sample
        private static double SampleAccuracy(double[,] tab)
        {
            var nbIrisType = 0;
            for (var i=0; i < tab.GetLength(0); i++)
            {
                if (Math.Abs(tab[i, 0] - _irisType) < Tolerance)
                    nbIrisType++;
            }
            return nbIrisType / (double)(tab.GetLength(0) - 1) * 100;
        }
    }
}