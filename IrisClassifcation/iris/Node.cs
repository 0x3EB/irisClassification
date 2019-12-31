using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    class Node<T>
    {
        private T value;
        private Node<T> RChild;
        private Node<T> LChild;

        public Node(T value, Node<T> Lchild, Node<T> Rchild)
        {
            this.value = value;
            this.LChild = Lchild;
            this.RChild = Rchild;
        }
        public T Value
        {
            get { return value; }
        }

        public Node<T> Lchild
        {
            get { return LChild; }
            set { LChild = value; }
        }
        public Node<T> Rchild
        {
            get { return RChild; }
            set { RChild = value; }
        }
    }
}
