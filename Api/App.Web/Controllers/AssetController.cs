using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BusinessLogicContracts;
using App.Web.Dtos.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [Route("assets")]
        [HttpGet]
        public IActionResult GetAllForCustomer(int customerId)
        {
            var resullt = _assetService.GetAllForCustomer(customerId);
            return Ok(resullt.Payload);
        }

        [Route("asset/new")]
        [HttpPost]
        public IActionResult AddNewAsset([FromBody] NewAssetDto newAssetDto, int customerId)
        {

            var result = _assetService.AddAsset(newAssetDto.SerialNumber, newAssetDto.Name, newAssetDto.Description, customerId);

            if(!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest(result.ErrorMessage);
        }

        [Route("asset/update")]
        [HttpPost]
        public IActionResult UpdateAsset([FromBody] UpdateAssetDto updateAssetDto)
        {
            var result = _assetService.UpdateAsset(updateAssetDto.Id, updateAssetDto.SerialNumber, updateAssetDto.Name, updateAssetDto.Description);

            if(!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return BadRequest(result.ErrorMessage);
        }

        [Route("asset/delete")]
        [HttpPost]
        public IActionResult DeleteAsset(int assetId)
        {
            var result = _assetService.DeleteAsset(assetId);
            if(!result.IsFaulted)
            {
                return Ok();
            } else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
