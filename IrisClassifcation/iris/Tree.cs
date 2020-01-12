using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    public class Tree<T>
    {
        private Node<T> _root;

        public Tree(Node<T> aa)
        {
            this._root = aa;
        }

        public Tree()
        {
            this._root = null;
        }

        public Node<T> Root
        {
            get { return _root; }
            set { _root = value; }
        }
        
        public Node<T> CreateNode(T[,] val) => new Node<T>(val, null, null);

        public bool AddLChild(Node<T> parent, Node<T> child)
        {
            bool association = false;
            if (parent != null && child != null)
            {
                if (parent.LChild == null)
                {
                    parent.LChild = child;
                    association = true;
                }
            }

            return association;
        }

        public bool AddRChild(Node<T> parent, Node<T> child)
        {
            bool association = false;
            if (parent != null && child != null)
            {
                if (parent.RChild == null)
                {
                    parent.RChild = child;
                    association = true;
                }
            }

            return association;
        }

        public bool IsLeafNode(Node<double> node)
        {
            bool leaf = false;
            if (node != null)
            {
                if (node.RChild == null && node.LChild == null)
                {
                    leaf = true;
                }
            }

            return leaf;
        }

        public void PrefixPrint(Node<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Array + " ");
                PrefixPrint(node.LChild);
                PrefixPrint(node.RChild);
            }
        }

        public void InfixPrint(Node<T> node)
        {
            if (node != null)
            {
                InfixPrint(node.LChild);
                Console.Write(node.Array + " ");
                InfixPrint(node.RChild);
            }
        }

        public void PostfixPrint(Node<T> node)
        {
            if (node != null)
            {
                PostfixPrint(node.LChild);
                PostfixPrint(node.RChild);
                Console.Write(node.Array + " ");
            }
        }

        public void HierarchyPrint(Node<double> node, int gap, Func<Node<double>, double> fnAccuracy)
        {
            for (int i = 0; i < gap; i++)
            {
                Console.Write(" ");
            }

            if (node != null)
            {
                if (gap != 0)
                {
                    if (node.Array is double[,])
                    {
                        var median = Model.CorrectedMedian(Model.GetColumn(node.Array, node.DivisionVar));
                        Console.WriteLine("|- Accuracy : " + fnAccuracy(node) + " / Individuals count :" +
                                          node.Array.GetLength(0) + " / X" + node.DivisionVar +
                                          Model.CharOfOperation(median, node) + median);
                    }
                }
                else
                {
                    Console.WriteLine(fnAccuracy(node));
                }

                if (!IsLeafNode(node))
                {
                    gap++;
                    HierarchyPrint(node.LChild, gap, fnAccuracy);
                    HierarchyPrint(node.RChild, gap, fnAccuracy);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }
        }


        public static void Print2DArrays(T[,] tab)
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j]);
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public int NbChildren(Node<T> parent)
        {
            int nb = 0;
            if (parent != null)
            {
                if (parent.LChild != null)
                {
                    nb++;
                }

                if (parent.RChild != null)
                {
                    nb++;
                }
            }

            return nb;
        }

        public int NbDescendant(Node<T> parent)
        {
            if (parent != null)
            {
                return NbChildren(parent) + NbDescendant(parent.RChild) + NbDescendant(parent.LChild);
            }
            else
            {
                return 0;
            }
        }

        public int NbLeaf(Node<double> parent)
        {
            if (IsLeafNode(parent))
            {
                return 1;
            }

            if (parent != null)
            {
                return NbLeaf(parent.RChild) + NbLeaf(parent.LChild);
            }
            else
            {
                return 0;
            }
        }

        private int Max(int a, int b)
        {
            int max = a;
            if (a < b)
            {
                max = b;
            }

            return max;
        }

        public int Height(Node<double> node)
        {
            if (IsLeafNode(node))
            {
                return 1;
            }

            if (node != null)
            {
                return 1 + Max(Height(node.LChild), Height(node.RChild));
            }
            else
            {
                return 0;
            }
        }
    }
}