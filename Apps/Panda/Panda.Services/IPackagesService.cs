using Panda.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public interface IPackagesService
    {
        void Create(string description, 
            decimal weight, string shippingAdress, string receipientName);

        IQueryable<Package> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}