using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbInvoiceRepository
    {
        private readonly StoreContext db;

        public DbInvoiceRepository()
        {
            db = new StoreContext();
        }

        public DbInvoiceRepository(StoreContext fakeDb)
        {
            db = fakeDb;
        }

        public void Create(Invoice invoice)
        {

            db.Invoices.Add(invoice);

            db.SaveChanges();
        }
    }
}