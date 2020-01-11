using System;
using NUnit.Framework;
using iris;

namespace IrisTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Node<int> racine = new Node<int>(new int [1,1],null,null);
            Tree<int> ab = new Tree<int>(racine);
            Node<int> n1 = ab.CreateNode(new int [1,1]);
            Node<int> n7 = ab.CreateNode(new int [1,1]);
            Node<int> n91 = ab.CreateNode(new int [1,1]);
            Node<int> n67 = ab.CreateNode(new int [1,1]);
            Node<int> n82 = ab.CreateNode(new int [1,1]);
            Node<int> n61 = ab.CreateNode(new int [1,1]);
            ab.AddLChild(ab.Root, n1);
            ab.AddRChild(ab.Root, n7);
            ab.AddLChild(ab.Root.LChild, n91);
            ab.AddRChild(ab.Root.LChild, n67);
            ab.AddRChild(ab.Root.RChild, n82);
            ab.AddLChild(ab.Root.RChild.RChild, n61);
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
            // readFile r = new readFile();
            // Console.WriteLine("test "+r.GetFile(121,5)[0,0]);
            // Console.ReadLine();
        }
    }
}