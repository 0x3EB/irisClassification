using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Top<T>
    {
        private T value;
        private Top<T> Lchild;
        private Top<T> Rchild;
        private Top<T> parent;

        public Top(Top<T> parent, T value, Top<T> Lchild, Top<T> Rchild)
        {
            this.parent = parent;
            this.value = value;
            this.Lchild = Lchild;
            this.Rchild = Rchild;
        }
        public T Value
        {
            get { return value; }
        }
        public Top<T> Parent
        {
            get { return parent; }
        }
        public Top<T> LChild
        {
            get { return LChild; }
            set { LChild = value; }
        }
        public Top<T> RChild
        {
            get { return RChild; }
            set { RChild = value; }
        }
    }
}
