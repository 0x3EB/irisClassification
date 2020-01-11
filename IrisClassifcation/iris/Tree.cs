using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    public class Tree<T>
    {
        private Node<T> root;

        public Tree(Node<T> aa)
        {
            this.root = aa;
        }
        public Tree()
        {
            this.root = null;
        }
        public Node<T> Root
        {
            get { return root; }
            set { root = value; }
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
        public bool isLeafNode(Node<T> node)
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
        public void HierarchyPrint(Node<T> node, int gap)
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
                        Print2DArrays(node.Array);
                    }
                    else
                        Console.WriteLine("|-" + node.Array);
                }
                else
                {
                    Console.WriteLine(node.Array);
                }
                if (!isLeafNode(node))
                {
                    gap++;
                    HierarchyPrint(node.LChild, gap);
                    HierarchyPrint(node.RChild, gap);
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
        public int NbLeaf(Node<T> parent)
        {
            if (isLeafNode(parent))
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
        public int Max(int a, int b)
        {
            int max = a;
            if (a < b)
            {
                max = b;
            }
            return max;
        }
        public int Height(Node<T> node)
        {
            if (isLeafNode(node))
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
