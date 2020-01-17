namespace Panda.Services
{
    using Panda.Data.Models;
    using System.Linq;

    public interface IReceiptsService
    {
        void CreateFromPackage(decimal weight, string packageId, string receipientId);

        IQueryable<Receipt> GetAll();
    }
}