using App.BusinessLogicContracts;
using App.DataAccessContracts;
using App.Entities;
using App.Entities.Results;
using System.Collections.Generic;

namespace App.BusinessLogic
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public MethodResult<Asset> AddAsset(string serialNumber, string name, string description, int customerId)
        {
            var existingAsset = _assetRepository.GetBySerialNumberForCustomer(serialNumber, customerId);
            if (existingAsset != null)
            {
                return MethodResult<Asset>.CreateFaultedResult("The asset is already created.");
            }

            var newAsset = new Asset
            {
                SerialNumber = serialNumber,
                Name = name,
                Description = description,
                CustomerId = customerId
            };

            _assetRepository.Add(newAsset);

            return new MethodResult<Asset>(newAsset);
        }

        public VoidResult DeleteAsset(int assetId)
        {
            var deleteAsset = _assetRepository.GetById(assetId);
            if (deleteAsset != null)
            {
                _assetRepository.Delete(assetId);
                return VoidResult.Ok();
            }
            else
            {
                return VoidResult.CreateFaultedResult("The asset can't be found.");
            }
        }

        public MethodResult<List<Asset>> GetAllForCustomer(int customerId)
        {
            var assets = _assetRepository.GetCustomerAssets(customerId);
            return new MethodResult<List<Asset>>(assets);
        }

        public MethodResult<Asset> GetById(int assetId)
        {

            var asset = _assetRepository.GetById(assetId);
            if (asset != null)
            {
                return new MethodResult<Asset>(asset);
            }

            return MethodResult<Asset>.CreateFaultedResult("The asset with this Id can't be found.");
        }

        public MethodResult<Asset> GetBySerialNumberForCustomer(string serialNumber, int customerId)
        {
            var asset = _assetRepository.GetBySerialNumberForCustomer(serialNumber, customerId);
            if (asset != null)
            {
                return new MethodResult<Asset>(asset);
            }

            return MethodResult<Asset>.CreateFaultedResult("The asset with this serial number can't be found.");
        }

        public MethodResult<Asset> UpdateAsset(int id, string serialNumber, string name, string description)
        {
            var updateAsset= _assetRepository.GetById(id);

            if (updateAsset != null)
            {
                updateAsset.SerialNumber = serialNumber;
                updateAsset.Name = name;
                updateAsset.Description = description;
           
                _assetRepository.Update(updateAsset);

                return new MethodResult<Asset>(updateAsset);
            }
            else
            {
                return MethodResult<Asset>.CreateFaultedResult("The asset with this Id can't be found.");
            }
        }
    }
}
