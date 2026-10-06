using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Vendra.Core.Dtos.OrderDtos;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Interfaces;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWorkRepo unitOfWorkRepo, IOrderService orderService, IMapper mapper)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("Create-Order")]
        public async Task<IActionResult> Create(OrderDto model)
        {
            var address = _mapper.Map<Address>(model.Address);
            var email=User.FindFirstValue(ClaimTypes.Email);
            var order =await _orderService.CreateOrderAsync(email, model.BasketId, model.DeliveryMethodId, address);
            if (order == null) return BadRequestResponse("Problem creating order!");
            var orderDto=_mapper.Map<GetOrderDto>(order);
            return Success(orderDto, "Succsee!");
            
        }
    }
}
