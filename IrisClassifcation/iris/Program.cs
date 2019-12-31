using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Program
    {
        static void Main(string[] args)
        {
            Node<int> racine = new Node<int>(12,null,null);
            Tree<int> ab = new Tree<int>(racine);
            Node<int> n1 = ab.CreateNode(1);
            Node<int> n7 = ab.CreateNode(7);
            Node<int> n91 = ab.CreateNode(91);
            Node<int> n67 = ab.CreateNode(67);
            Node<int> n82 = ab.CreateNode(82);
            Node<int> n61 = ab.CreateNode(61);
            ab.AddLChild(ab.Root, n1);
            ab.AddRChild(ab.Root, n7);
            ab.AddLChild(ab.Root.Lchild, n91);
            ab.AddRChild(ab.Root.Lchild, n67);
            ab.AddRChild(ab.Root.Rchild, n82);
            ab.AddLChild(ab.Root.Rchild.Rchild, n61);
            Console.Write("\nAffichage préfixe : ");
            ab.PrefixPrint(racine);
            Console.Write("\nAffichage infixe : ");
            ab.InfixPrint(racine);
            Console.Write("\nAffichage postfixe : ");
            ab.PostfixPrint(racine);
            Console.WriteLine("\nAffichage arborescence : ");
            ab.HierarchyPrint(racine,0);
            Console.Write("Nombre enfant racine : ");
            Console.Write(ab.NbChildren(ab.Root));
            Console.Write("\nNombre descendants racine : ");
            Console.Write(ab.NbDescendant(ab.Root));
            Console.Write("\nNombre feuilles racine : ");
            Console.Write(ab.NbLeaf(ab.Root));
            Console.Write("\nHauteur arbre racine : ");
            Console.Write(ab.Height(ab.Root));
            Console.ReadKey();
        }
    }
}
