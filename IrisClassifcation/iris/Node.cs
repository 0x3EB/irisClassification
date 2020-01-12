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

        /// <summary>
        ///     Getter for the value of the node
        /// </summary>
        public T[,] Array { get; }

        /// <summary>
        ///     Number of the column used for the best division
        /// </summary>
        public int DivisionVar { get; set; }

        /// <summary>
        ///     Getter and Setter for the Left Child of the Node
        /// </summary>
        public Node<T> LChild { get; set; }

        /// <summary>
        ///     Getter and Setter for the Right Child of the Node
        /// </summary>
        public Node<T> RChild { get; set; }
    }
}