using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class ResearchTree<T>
    {
        private Top<T> root;
        private Top<T> child;

        public ResearchTree(Top<T> root)
        {
            this.root = root;
        }
        public ResearchTree()
        {
            this.root = null;
        }
        public Top<T> Root
        {
            get { return root; }
            set { root = value; }
        }
        public Top<T> CreationSommet(T val)
        {
            return new Top<T>(null, val, null, null);
        }
        public bool AssocierFilsG(Top<T> parent, Top<T> child)
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
        public bool AddRChild(Top<T> parent, Top<T> child)
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
        public bool isLeafTop(Top<T> top)
        {
            bool leaf = false;
            if (top != null)
            {
                if (top.RChild == null && top.LChild == null)
                {
                    leaf = true;
                }
            }
            return leaf;
        }
        public void PrefixPrint(Top<T> top)
        {
            if (top != null)
            {
                Console.Write(top.Value + " ");
                PrefixPrint(top.LChild);
                PrefixPrint(top.RChild);
            }
        }
        public void InfixPrint(Top<T> top)
        {
            if (top != null)
            {
                InfixPrint(top.LChild);
                Console.Write(top.Value + " ");
                InfixPrint(top.RChild);
            }
        }
        public void PostfixPrint(Top<T> top)
        {
            if (top != null)
            {
                PostfixPrint(top.LChild);
                PostfixPrint(top.RChild);
                Console.Write(top.Value + " ");
            }
        }
        public void HierarchyPrint(Top<T> top, int gap)
        {

            for (int i = 0; i < gap; i++)
            {
                Console.Write(" ");
            }
            if (top != null)
            {
                if (gap != 0)
                {
                    Console.WriteLine("|-" + top.Value);
                }
                else
                {
                    Console.WriteLine(top.Value);
                }
                if (!isLeafTop(top))
                {
                    gap++;
                    HierarchyPrint(top.LChild, gap);
                    HierarchyPrint(top.RChild, gap);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }
        }
        public int NbChildren(Top<T> parent)
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
        public int NbDescendant(Top<T> parent)
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
        public int NbFeuille(Top<T> parent)
        {
            if (isLeafTop(parent))
            {
                return 1;
            }
            if (parent != null)
            {
                return NbFeuille(parent.RChild) + NbFeuille(parent.LChild);
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
        public int Hauteur(Top<T> top)
        {
            if (isLeafTop(top))
            {
                return 1;
            }
            if (top != null)
            {
                return 1 + Max(Hauteur(top.LChild), Hauteur(top.RChild));
            }
            else
            {
                return 0;
            }
        }
        public void InsertionSommet(Top<int> top, int val)
        {
            if (top == null)
            {
                top = new Top<int>(null, val,null,null);
            }
            else
            {
                if (val > top.Value)
                {
                    InsertionSommet(top.RChild, val);
                }
                else
                {
                    InsertionSommet(top.LChild, val);
                }
            }
        }
    }
}
