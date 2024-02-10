using System.Collections;

namespace PAOO.Biblioteca.Collections
{
    public class BaseCatalog<T, K>: IEnumerable<T> where T : IComparable<K>
    {
        protected readonly List<T> catalog = new();

        public BaseCatalog() {}
        
        public void Add(T? item)
        {
            if (item != null)
            {
                catalog.Add(item);
            }
        }

        public bool Remove(K id) {
            T? item = catalog.Find(x => x.CompareTo(id) == 0);
            if (item == null)
            {
                return false;
            }
            else
            {
                catalog.Remove(item);
                return true;
            }
        }

        public T? GetOneByIdentifier(K id) => catalog.Find(x => x.CompareTo(id) == 0);

        public List<T> GetAll() => catalog;

        public IEnumerator<T> GetEnumerator()
        {
            return catalog.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get => catalog.Count;
        }
    }
}
