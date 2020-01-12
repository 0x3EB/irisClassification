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
        private Top<T> _lchild;
        private Top<T> _rchild;
        private Top<T> _parent;

        public Top(Top<T> parent, T value, Top<T> Lchild, Top<T> Rchild)
        {
            this._parent = parent;
            this.value = value;
            this._lchild = Lchild;
            this._rchild = Rchild;
        }
        public T Value
        {
            get { return value; }
        }
        public Top<T> Parent
        {
            get { return _parent; }
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
