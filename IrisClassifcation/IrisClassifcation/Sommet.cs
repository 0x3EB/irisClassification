using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisClassifcation
{
    class Sommet<T>
    {
        private T valeur;
        private Sommet<T> filsG;
        private Sommet<T> filsD;
        private Sommet<T> parent;

        public Sommet(Sommet<T> parent, T valeur, Sommet<T> filsG, Sommet<T> filsD)
        {
            this.parent = parent;
            this.valeur = valeur;
            this.filsG = filsG;
            this.filsD = filsD;
        }
        public T Valeur
        {
            get { return valeur; }
        }
        public Sommet<T> Parent
        {
            get { return parent; }
        }
        public Sommet<T> FilsG
        {
            get { return filsG; }
            set { filsG = value; }
        }
        public Sommet<T> FilsD
        {
            get { return filsD; }
            set { filsD = value; }
        }
    }
}
