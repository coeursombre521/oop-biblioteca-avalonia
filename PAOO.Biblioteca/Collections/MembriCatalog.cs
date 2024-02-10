using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Collections
{
    public class MembriCatalog: BaseCatalog<Membru, Guid>
    {
        private static MembriCatalog? _instance;

        private MembriCatalog() { }

        public static MembriCatalog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MembriCatalog();
            }
            return _instance;
        }

        public void Add(Membru? membru) => base.Add(membru);

        public bool Remove(Guid id) => base.Remove(id);

        public Membru? GetOneByIdentifier(Guid id) => base.GetOneByIdentifier(id);

        public int Count => base.Count;
    }
}
