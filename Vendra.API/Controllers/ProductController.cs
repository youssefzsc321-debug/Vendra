using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWorkRepo unitOfWorkRepo, IMapper mapper)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _mapper = mapper;
        }
        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAll()
        {
            //var res = await _unitOfWorkRepo.Products.GetAllAsync();
            var spec = new ProductWithTypeAndBrand();
            var res =await _unitOfWorkRepo.Products.GetAllAsyncWithSpec(spec);
            var modelDto = _mapper.Map<IEnumerable<GetAllDto>>(res);

            return Success<IEnumerable<GetAllDto>>(modelDto, "Success!");
        }

        [HttpGet("Get-Product/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var spec = new ProductWithTypeAndBrand(id);
            var product=await _unitOfWorkRepo.Products.GetByIdWithSpec(spec);
            if (product is null) return BadRequestResponse("Not Found!");
            
            var modelDto = _mapper.Map<GetAllDto>(product);

            return Success<GetAllDto>(modelDto,"Success");
        }


    }
}
