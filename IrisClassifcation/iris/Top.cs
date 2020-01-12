namespace iris
{
    internal class Top<T>
    {
        private Top<T> _lChild;
        private Top<T> _rChild;

        public Top(Top<T> parent, T value, Top<T> lChild, Top<T> rChild)
        {
            Parent = parent;
            Value = value;
            _lChild = lChild;
            _rChild = rChild;
        }

        public T Value { get; }

        public Top<T> Parent { get; }

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