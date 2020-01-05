using System;

namespace iris
{
    public class Model
    {
        private readFile _file;
        private Tree<double[]> _tree;
        // Default values
        private static int _irisType = 2;
        private static int _minAccuracy = 10;
        private static int _maxAccuracy = 95;
        private static int _minIndividuals = 65;
        private static int _maxTreeSize = 300;
        private const double Tolerance = 0.001;
        
        public Model(string file)
        {
            _file = new readFile(file);
            _tree = new Tree<double[]>();
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

        
        public bool IsSampleDiv(Node<double[]> node, int nbIndividuals)
        {
            if (!IsMaxHeightReached(_tree, _maxTreeSize, node))
                return false;
            if (!MoreIndividuals(nbIndividuals))
                return false;
            if (!SampleAccuracy())
                return false;
            return true;
        }

        private bool IsMaxHeightReached(Tree<double[]> tree, int maxTreeSize, Node<double[]> node)
        {
            return tree.Height(node) < maxTreeSize;
        }

        private bool MoreIndividuals(int nbIndivuals)
        {
            return nbIndivuals > _minIndividuals;
        }

        private bool SampleAccuracy()
        {
            double[] tabIrisType = _file.getDoubleCol(0);
            int nbIrisType = 0;
            for (int i=0;i<tabIrisType.Length-1;i++)
            {
                if (tabIrisType[i] == _irisType)
                    nbIrisType++;
            }
            double accuracy = (double)(nbIrisType / (double)(tabIrisType.Length-1))*100;
            return (accuracy >= _minAccuracy && accuracy <= _maxAccuracy);
        }
    }
}