namespace Panda.Services
{

    using Panda.Data.Models;
    using System.Linq;

    public interface IPackagesService
    {
        void Create(string description,
            decimal weight, string shippingAdress, string receipientName);

        IQueryable<Package> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}