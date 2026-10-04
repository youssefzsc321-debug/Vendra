using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        public BasketController(IBasketRepo _basketRepo, IMapper mapper, IUnitOfWorkRepo unitOfWorkRepo)
        {
            this._basketRepo = _basketRepo;
            _mapper = mapper;
            _unitOfWorkRepo = unitOfWorkRepo;
        }

        [HttpGet("Get-Basket/{id}")]
        public async Task<IActionResult> GetBasket(string id)
        {
            var basket = await _basketRepo.GetCustomerBasket(id);
            if (basket == null) basket = new CustomerBasket(id);
            return Success(basket,"Success!");
        }

        [HttpPost("Update-Basket")]
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto model)
        {
            var basket=_mapper.Map<CustomerBasket>(model);
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWorkRepo.Products.GetByIdAsync(item.Id);
                if(product == null) return BadRequestResponse($"Product with ID {item.Id} was not found.");
                item.Price = product.Price;
                item.ProductName = product.Name;
                item.ProductImage = product.Image;
                item.Brand = product.ProductBrandId;
                item.Type = product.ProductTypeId;
            }

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
