using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;
using Vendra.Core.Dtos.OrderDtos;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Interfaces;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentService paymentService, IConfiguration configuration, IMapper mapper)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Create-Update-Payment-intent/{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket=await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequestResponse("Problem in your basket!");
            return Ok(basket);
        }

        [HttpPost("webhook")] //api/Payments/webhook
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var endpointSecret = _configuration["StripeSettings:WhSecret"];

                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );
                var order=new Order();
                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                   order= await _paymentService.UpdateOrderPaymentState(paymentIntent.Id, OrderStatus.PaymentReceived);

                }
                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    order= await _paymentService.UpdateOrderPaymentState(paymentIntent.Id, OrderStatus.PaymentFailed);


                }
                else
                {
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                }

                var orderDto = _mapper.Map<GetOrderDto>(order);
                return Success(orderDto,"Success!");
            }
            catch (StripeException e)
            {
                return BadRequestResponse(e.Message);
            }
        }
    }
}
