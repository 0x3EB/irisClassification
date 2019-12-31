using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClassifcation
{
    class Noeud<T>
    {
        private T valeur;
        private Noeud<T> filsG;
        private Noeud<T> filsD;

        public Noeud(T valeur, Noeud<T> filsG, Noeud<T> filsD)
        {
            this.valeur = valeur;
            this.filsG = filsG;
            this.filsD = filsD;
        }
        public T Valeur
        {
            get { return valeur; }
        }
        public Noeud<T> FilsG
        {
            get { return filsG; }
            set { filsG = value; }
        }
        public Noeud<T> FilsD
        {
            get { return filsD; }
            set { filsD = value; }
        }
    }
}
