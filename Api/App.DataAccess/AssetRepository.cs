using App.DataAccessContracts;
using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DataAccess
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DataBaseContext _context;

        public AssetRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Add(Asset asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        public void Update(Asset asset)
        {
            _context.Assets.Update(asset);
            _context.SaveChanges();
        }

        public void Delete(int assetId)
        {
            var result = _context.Assets.FirstOrDefault(x => x.Id == assetId);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }

        public List<Asset> GetCustomerAssets(int customerId)
        {
            return _context.Assets.Where(x => x.CustomerId == customerId).ToList();
        }

        public Asset GetById(int assetId)
        {
            return _context.Assets.FirstOrDefault(x => x.Id == assetId);
        }

        public Asset GetBySerialNumberForCustomer(string serialNumber, int customerId)
        {
            return _context.Assets.FirstOrDefault(x => x.SerialNumber == serialNumber && x.CustomerId ==  customerId);
        }
    }
}
