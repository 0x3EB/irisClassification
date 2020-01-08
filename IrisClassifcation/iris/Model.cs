using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iris
{
    public class Model
    {
        private Tree<double[,]> _tree;
        // Default values
        private static int _irisType = 2;
        private static int _minAccuracy = 10;
        private static int _maxAccuracy = 95;
        private static int _minIndividuals = 65;
        private static int _maxTreeSize = 300;
        private const double Tolerance = 0.001;
        private int Individuals { get; }
        private int Feature { get; }
        
        public Model(string fileName)
        {
            var file = new readFile(fileName);
            Individuals = file.getNbLine();
            Feature = file.getNbCol();
            _tree = new Tree<double[,]>(new Node<double[,]>(file.GetFile(), null, null));
        }

        public Tree<double[,]> GetTree()
        {
            return this._tree;
        }

        public void AskParameters()
        {
            _irisType = CheckInt(positive:true, err:"Enter positive number", message:"Y value to predict?");
            _minAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Minimum accuracy?");
            _maxAccuracy = CheckInt(min:0, max:100, err:"Wrong percentage", message:"Maximum accuracy?");
            _minIndividuals = CheckInt(positive:true, err:"Enter positive number", message:"Minimum number of individuals?");
            _maxTreeSize = CheckInt(positive:true, err:"Enter positive number", message:"Maximum tree size?");
        }

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

        public void Prepare()
        {
            if (IsSampleDiv(_tree.Root, _tree.Root.Value.Length))
            {
                Console.WriteLine(median(GetColumn(_tree.Root.Value, 1)));
            }
        }

        // private int BestDivision()
        // {
        //     // Get all columns
        //     for (var i = 0; i < _tree.Root.Value.GetLength(1); ++i)
        //     {
        //         List<double> left, right;
        //         var column = GetColumn(_tree.Root.Value, i);
        //         var median = this.median(column);
        //         foreach (var val in column)
        //         {
        //             if (val <= median)
        //             {
        //                 left.Add(val);
        //             }
        //             right.Add(val);
        //         }
        //     }
        // }

        public double[,] ResizedTree(int indexCol)
        {
            double[,] new2DArray = null;
            for (var i = 0; i < _tree.Root.Value.GetLength(0); ++i)
            {
                double[] tab = null;
                double[] tabWithRemovedValue = null;
                for (var j = 0; j < _tree.Root.Value.GetLength(1); ++j)
                {
                    tab = new double[_tree.Root.Value.GetLength(1)];
                    tab[j] = _tree.Root.Value[i,j];
                    tabWithRemovedValue = RemoveDoubleArrayItem(tab, 2);
                }
                new2DArray = ResizeArray<double>(_tree.Root.Value, _tree.Root.Value.GetLength(0), tabWithRemovedValue.Length, indexCol);
            }
            return new2DArray;
        }

        // supprime la colone du tab avec indexCol et retourne un nouveau tableau
        T[,] ResizeArray<T>(T[,] original, int rows, int cols, int indexCol)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                {
                    if (j >= indexCol)
                        newArray[i, j] = original[i, j+1];
                    else
                        newArray[i, j] = original[i, j];
                }
                    
            return newArray;
        }

        private double[] RemoveDoubleArrayItem(double[] tab, int indexCol)
        {
            ArrayList doubleArrayList = new ArrayList(tab);
            doubleArrayList.RemoveAt(indexCol);
            double[] returnDoubleArray = (double[])doubleArrayList.ToArray(typeof(double));
            return returnDoubleArray;
        }
 
        private double[] GetColumn(double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }
        public double median(double[] tabVal)
        {
            Array.Sort(tabVal);
            if (tabVal.Length < 2 || TabSameValue(tabVal))
            {
                return 0;
            }
            else if (tabVal.Length % 2 != 0)
            {
                int index = (tabVal.Length + 1) / 2;
                if (tabVal.Max() != tabVal[index])
                    return tabVal[index];
                return tabVal[index - 1];
            }
            else
            {
                int index1 = tabVal.Length / 2;
                int index2 = (tabVal.Length / 2) + 1;
                return (tabVal[index1] + tabVal[index2]) / 2;
            }
        }
        
        private Boolean TabSameValue(double[] tab)
        {
            for (int i = 0; i < tab.Length - 1; i++)
            {
                return tab[i] == tab[i + 1];
            }
            return false;
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
        
        private bool IsSampleDiv(Node<double[,]> node, int nbIndividuals)
        {
            if (!IsMaxHeightReached(_tree, _maxTreeSize, node))
                return false;
            if (!MoreIndividuals(nbIndividuals))
                return false;
            if (!SampleAccuracy())
                return false;
            return true;
        }
        
        private bool IsMaxHeightReached(Tree<double[,]> tree, int maxTreeSize, Node<double[,]> node)
        {
            return tree.Height(node) < maxTreeSize;
        }

        private bool MoreIndividuals(int nbIndivuals)
        {
            return nbIndivuals > _minIndividuals;
        }

        private bool SampleAccuracy()
        {
            var tabIrisType = new double[_tree.Root.Value.GetLength(0) - 1];
            for (var i = 0; i < tabIrisType.Length; ++i)
            {
                tabIrisType[i] = _tree.Root.Value[i, 0];
            }
            
            int nbIrisType = 0;
            for (int i=0; i<tabIrisType.Length; i++)
            {
                if (tabIrisType[i] == _irisType)
                    nbIrisType++;
            }
            double accuracy = (double)(nbIrisType / (double)(tabIrisType.Length-1))*100;
            return (accuracy >= _minAccuracy && accuracy <= _maxAccuracy);
        }
    }
}