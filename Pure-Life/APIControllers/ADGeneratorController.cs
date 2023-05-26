using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Models;
using Pure_Life.Services;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADGeneratorController : ControllerBase
    {

        private readonly IADProductService _adProduct;
        public ADGeneratorController(IADProductService adProduct)
        {
            _adProduct = adProduct;
        }

        [HttpPost]
        public async Task<ActionResult<ADProductResponseModel>> GenerateAD(CustomerRequestModel aDGenerateRequestModel)
        {
            try
            {

                var response = await _adProduct.GenerateAdContent(aDGenerateRequestModel);

                return response;
            }
            catch (System.Exception ex)
            {

                return null;
            }

        }
    }
}