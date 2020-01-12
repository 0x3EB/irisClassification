using System;

namespace iris
{
    internal class ResearchTree<T>
    {
        public ResearchTree(Top<T> root)
        {
            Root = root;
        }

        public ResearchTree()
        {
            Root = null;
        }

        public Top<T> Root { get; set; }

        public Top<T> CreationSommet(T val)
        {
            return new Top<T>(null, val, null, null);
        }

        public bool AssocierFilsG(Top<T> parent, Top<T> child)
        {
            var association = false;
            if (parent != null && child != null)
                if (parent.LChild == null)
                {
                    parent.LChild = child;
                    association = true;
                }

            return association;
        }

        public bool AddRChild(Top<T> parent, Top<T> child)
        {
            var association = false;
            if (parent != null && child != null)
                if (parent.RChild == null)
                {
                    parent.RChild = child;
                    association = true;
                }

            return association;
        }

        public bool IsLeafTop(Top<T> top)
        {
            var leaf = false;
            if (top != null)
                if (top.RChild == null && top.LChild == null)
                    leaf = true;
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
            for (var i = 0; i < gap; i++) Console.Write(" ");
            if (top != null)
            {
                if (gap != 0)
                    Console.WriteLine("|-" + top.Value);
                else
                    Console.WriteLine(top.Value);
                if (!IsLeafTop(top))
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
            var nb = 0;
            if (parent != null)
            {
                if (parent.LChild != null) nb++;
                if (parent.RChild != null) nb++;
            }

            return nb;
        }

        public int NbDescendant(Top<T> parent)
        {
            if (parent != null)
                return NbChildren(parent) + NbDescendant(parent.RChild) + NbDescendant(parent.LChild);
            return 0;
        }

        public int NbFeuille(Top<T> parent)
        {
            if (IsLeafTop(parent)) return 1;
            if (parent != null)
                return NbFeuille(parent.RChild) + NbFeuille(parent.LChild);
            return 0;
        }

        public int Max(int a, int b)
        {
            var max = a;
            if (a < b) max = b;
            return max;
        }

        public int Hauteur(Top<T> top)
        {
            if (IsLeafTop(top)) return 1;
            if (top != null)
                return 1 + Max(Hauteur(top.LChild), Hauteur(top.RChild));
            return 0;
        }

        public void InsertionTop(Top<int> top, int val)
        {
            if (top == null)
            {
                top = new Top<int>(null, val, null, null);
            }
            else
            {
                InsertionTop(val > top.Value ? top.RChild : top.LChild, val);
            }
        }
    }
}