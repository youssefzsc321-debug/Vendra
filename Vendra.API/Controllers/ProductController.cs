using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vendra.Core.Common;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications;
using Vendra.Infrastructure.Specifications.ProductSpec;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [ProducesResponseType(typeof(APIResponse<IReadOnlyList<GetProductlDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAll([FromQuery]ProductSpecParam specParams)
        {
            //var res = await _unitOfWorkRepo.Products.GetAllAsync();
            var spec = new ProductWithTypeAndBrand(specParams);
            var res =await _unitOfWorkRepo.Products.GetAllAsyncWithSpec(spec);
            var modelDto = _mapper.Map<IEnumerable<GetProductlDto>>(res);
            var specCount = new ProductWiteFiltersForCountSpec(specParams);
            var paginatedData = new PaginatedDto<GetProductlDto>()
            {
                Count =await _unitOfWorkRepo.Products.CountAsyncSpec(specCount),
                PageIndex = specParams.PageIndex,
                PageSize = specParams.Take,
                Data = modelDto.ToList()

            };
            return Success(paginatedData, "Success!");
        }

        [HttpGet("Get-Product/{id}")]
        [ProducesResponseType(typeof(APIResponse<GetProductlDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>GetById(int id)
        {
            var spec = new ProductWithTypeAndBrand(id);
            var product=await _unitOfWorkRepo.Products.GetByIdWithSpec(spec);
            if (product is null) return NotFoundResponse("Not Found!");
            
            var modelDto = _mapper.Map<GetProductlDto>(product);

            return Success<GetProductlDto>(modelDto,"Success");
        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(APIResponse<IReadOnlyList<GetBrands>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _unitOfWorkRepo.ProductBrands.GetAllAsync();
            var modelDto = _mapper.Map<IReadOnlyList<GetBrands>>(brands);
            return Success(modelDto, "Success!");
        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(APIResponse<IReadOnlyList<GetBrands>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypes()
        {
            var types =await _unitOfWorkRepo.ProductTypes.GetAllAsync();
            var modelDto=_mapper.Map<IReadOnlyList<GetTypes>>(types);
            return Success(modelDto, "Success!");
        }




    }
}
