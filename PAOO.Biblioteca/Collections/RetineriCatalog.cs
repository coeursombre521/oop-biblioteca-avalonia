using System.Data.Common;
using PAOO.Biblioteca.Models;

namespace PAOO.Biblioteca.Collections
{
    public class RetineriCatalog: BaseCatalog<Retinere, Guid>
    {
        private static RetineriCatalog? _instance;
        
        private RetineriCatalog() { }

        public static RetineriCatalog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RetineriCatalog();
            }
            return _instance;
        }

        public void Add(Retinere? retinere) => base.Add(retinere);

        public bool Remove(Guid id) => base.Remove(id);

        public Retinere? GetOneByIdentifier(Guid id) => base.GetOneByIdentifier(id);

        public List<Retinere> GetByItemId(Guid carteId) => catalog.FindAll(x => x.BorrowableItem.Id == carteId);

        public bool RemoveByMemberId(Guid membruId) => catalog.RemoveAll(x => x.Membru.Id == membruId) > 0;

        public int Count => base.Count;
    }
}
