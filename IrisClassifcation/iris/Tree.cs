using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Tree<T>
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
        public Node<T> CreateNode(T val) => new Node<T>(val, null, null);

        public bool AddLChild(Node<T> parent, Node<T> child)
        {
            bool association = false;
            if (parent != null && child != null)
            {
                if (parent.Lchild == null)
                {
                    parent.Lchild = child;
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
                if (parent.Rchild == null)
                {
                    parent.Rchild = child;
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
                if (node.Rchild == null && node.Lchild == null)
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
                Console.Write(node.Value + " ");
                PrefixPrint(node.Lchild);
                PrefixPrint(node.Rchild);
            }
        }
        public void InfixPrint(Node<T> node)
        {
            if (node != null)
            {
                InfixPrint(node.Lchild);
                Console.Write(node.Value + " ");
                InfixPrint(node.Rchild);
            }
        }
        public void PostfixPrint(Node<T> node)
        {
            if (node != null)
            {
                PostfixPrint(node.Lchild);
                PostfixPrint(node.Rchild);
                Console.Write(node.Value + " ");
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
                    Console.WriteLine("|-" + node.Value);
                }
                else
                {
                    Console.WriteLine(node.Value);
                }
                if (!isLeafNode(node))
                {
                    gap++;
                    HierarchyPrint(node.Lchild, gap);
                    HierarchyPrint(node.Rchild, gap);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }
        }
        public int NbChildren(Node<T> parent)
        {
            int nb = 0;
            if (parent != null)
            {
                if (parent.Lchild != null)
                {
                    nb++;
                }
                if (parent.Rchild != null)
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
                return NbChildren(parent) + NbDescendant(parent.Rchild) + NbDescendant(parent.Lchild);
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
                return NbLeaf(parent.Rchild) + NbLeaf(parent.Lchild);
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
                return 1 + Max(Height(node.Lchild), Height(node.Rchild));
            }
            else
            {
                return 0;
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
    }
}
