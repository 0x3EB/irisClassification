using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClassifcation
{
    class Arbre<T>
    {
        private Noeud<T> racine;

        public Arbre(Noeud<T> racine)
        {
            this.racine = racine;
        }
        public Arbre()
        {
            this.racine = null;
        }
        public Noeud<T> Racine
        {
            get { return racine; }
            set { racine = value; }
        }
        public Noeud<T> CreationNoeud(T val)
        {
            return new Noeud<T>(val, null, null);
        }
        public bool AssocierFilsG(Noeud<T> parent, Noeud<T> enfant)
        {
            bool association = false;
            if (parent != null && enfant != null)
            {
                if (parent.FilsG == null)
                {
                    parent.FilsG = enfant;
                    association = true;
                }
            }
            return association;
        }
        public bool AssocierFilsD(Noeud<T> parent, Noeud<T> enfant)
        {
            bool association = false;
            if (parent != null && enfant != null)
            {
                if (parent.FilsD == null)
                {
                    parent.FilsD = enfant;
                    association = true;
                }
            }
            return association;
        }
        public bool EstFeuille(Noeud<T> noeud)
        {
            bool feuille = false;
            if (noeud != null) // ou un seul if
            {
                if (noeud.FilsD == null && noeud.FilsG == null)
                {
                    feuille = true;
                }
            }
            return feuille;
        }
        public void AffichagePrefix(Noeud<T> noeud)
        {
            if (noeud != null)
            {
                Console.Write(noeud.Valeur + " ");
                AffichagePrefix(noeud.FilsG);
                AffichagePrefix(noeud.FilsD);
            }
        }
        public void AffichageInfix(Noeud<T> noeud)
        {
            if (noeud != null)
            {
                AffichageInfix(noeud.FilsG);
                Console.Write(noeud.Valeur + " ");
                AffichageInfix(noeud.FilsD);
            }
        }
        public void AffichagePostfix(Noeud<T> noeud)
        {
            if (noeud != null)
            {
                AffichagePostfix(noeud.FilsG);
                AffichagePostfix(noeud.FilsD);
                Console.Write(noeud.Valeur + " ");
            }
        }
        public void AffichageArborescence(Noeud<T> noeud, int nbDecalage)
        {

            for (int i = 0; i < nbDecalage; i++)
            {
                Console.Write(" ");
            }
            if (noeud != null)
            {
                if (nbDecalage != 0)
                {
                    Console.WriteLine("|-" + noeud.Valeur);
                }
                else
                {
                    Console.WriteLine(noeud.Valeur);
                }
                if (!EstFeuille(noeud))
                {
                    nbDecalage++;
                    AffichageArborescence(noeud.FilsG, nbDecalage);
                    AffichageArborescence(noeud.FilsD, nbDecalage);
                }
            }
            else
            {
                Console.WriteLine("|-X");
            }
        }
        public int NbEnfants(Noeud<T> parent)
        {
            int nb = 0;
            if (parent != null)
            {
                if (parent.FilsG != null)
                {
                    nb++;
                }
                if (parent.FilsD != null)
                {
                    nb++;
                }
            }
            return nb;
        }
        public int NbDescendant(Noeud<T> parent)
        {
            if (parent != null)
            {
                return NbEnfants(parent) + NbDescendant(parent.FilsD) + NbDescendant(parent.FilsG);
            }
            else
            {
                return 0;
            }
        }
        public int NbFeuille(Noeud<T> parent)
        {
            if (EstFeuille(parent))
            {
                return 1;
            }
            if (parent != null)
            {
                return NbFeuille(parent.FilsD) + NbFeuille(parent.FilsG);
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
        public int Hauteur(Noeud<T> noeud)
        {
            if (EstFeuille(noeud))
            {
                return 1;
            }
            if (noeud != null)
            {
                return 1 + Max(Hauteur(noeud.FilsG), Hauteur(noeud.FilsD));
            }
            else
            {
                return 0;
            }
        }
    }
}
