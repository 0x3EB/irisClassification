using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    public class Node<T>
    {
        public Node(T[,] array, Node<T> lChild, Node<T> rChild)
        {
            Array = array;
            LChild = lChild;
            RChild = rChild;
        }
        public T[,] Array { get; }

        public int DivisionVar { get; set; }

        public Node<T> LChild { get; set; }

        public Node<T> RChild { get; set; }
    }
}
