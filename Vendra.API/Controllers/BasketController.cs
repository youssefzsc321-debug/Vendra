using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepo _basketRepo;
        public BasketController(IBasketRepo _basketRepo)
        {
            this._basketRepo = _basketRepo;
        }

        [HttpGet("Get-Basket/{id}")]
        public async Task<IActionResult> GetBasket(string id)
        {
            var basket = await _basketRepo.GetCustomerBasket(id);
            if (basket == null) basket = new CustomerBasket(id);
            return Success(basket,"Success!");
        }

        [HttpPost("Update-Basket")]
        public async Task<IActionResult> UpdateBasket(CustomerBasket basket)
        {
            var customerBasket=await _basketRepo.UpdateCustomerBasket(basket);
            if (customerBasket == null) return BadRequestResponse("A problem has occurred with updating the basket");
            return Success(customerBasket, "Update successful.!");
        }

        [HttpDelete("Delete-Basket")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var deleted = await _basketRepo.DeleteCustomerBasket(id);

            if (!deleted)
                return BadRequestResponse("Basket not found or could not be deleted.");

            return Success(true, "Basket deleted successfully.");

        }
    }
}
