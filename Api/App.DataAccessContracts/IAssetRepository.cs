using App.Entities;
using System.Collections.Generic;

namespace App.DataAccessContracts
{
    public interface IAssetRepository
    {
        List<Asset> GetCustomerAssets(int customerId);

        Asset GetById(int assetId);

        Asset GetBySerialNumberForCustomer(string serialNumber, int customerId);

        void Add(Asset asset);

        void Update(Asset asset);

        void Delete(int assetId);
    }
}
