
namespace HelloWorld
{
    internal class Compare:IEqualityComparer<Alpha>
    {
        private static Compare _instatnce;
        private Compare() { }

        public static Compare Instatnce =>
            _instatnce ?? (_instatnce = new Compare());

        public bool Equals(Alpha? x, Alpha? y) 
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x._size.Equals(y._size);
        }

        public int GetHashCode(Alpha? obj)
        {
            return obj._size.GetHashCode();
        }

    }
}
