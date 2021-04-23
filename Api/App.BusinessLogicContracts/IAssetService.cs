using App.Entities;
using App.Entities.Results;
using System.Collections.Generic;

namespace App.BusinessLogicContracts
{
    public interface IAssetService
    {
        MethodResult<List<Asset>> GetAllForCustomer(int customerId);

        MethodResult<Asset> GetById(int assetId);

        MethodResult<Asset> GetBySerialNumberForCustomer(string serialNumber, int customerId);

        MethodResult<Asset> AddAsset(string serialNumber, string name, string description, int customerId);

        MethodResult<Asset> UpdateAsset(int id, string serialNumber, string name, string description);

        VoidResult DeleteAsset(int id);
    }
}
