
namespace Panda.Services
{
    using Panda.Data;
    using Panda.Data.Models;
    using System;
    using System.Linq;

    public class ReceiptsService : IReceiptsService
    {
        private readonly PandaDbContext db;

        public ReceiptsService(PandaDbContext db)
        {
            this.db = db;
        }

        public void CreateFromPackage(decimal weight, string packageId, string receipientId)
        {
            var receipt = new Receipt
            {
                PackageId = packageId,
                RecipientId = receipientId,
                Fee = weight * 2.67M,
                IssuedOn = DateTime.UtcNow
            };

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public IQueryable<Receipt> GetAll()
        {
            return db.Receipts;
        }
    }
}
