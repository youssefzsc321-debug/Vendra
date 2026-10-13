using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Repositories;
using Vendra.Infrastructure.Specifications.ProductSpec;

namespace Vendra.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAdminController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;

        public ProductsAdminController(IMapper mapper, IImageService imageService, IUnitOfWorkRepo unitOfWorkRepo)
        {
            _mapper = mapper;
            _imageService = imageService;
            _unitOfWorkRepo = unitOfWorkRepo;
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProductAsync([FromForm] PostPutProductDto model)
        {
            var product = _mapper.Map<Product>(model);
            if (model.Image != null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                var imageName = $"{Guid.NewGuid()}{extension}";
                var folderName = "Images/products";

                var (isUploaded, errorMessage) = await _imageService.UploadAsync(
                    model.Image,
                     imageName,
                    folderName,
                    true
                );

                if (isUploaded)
                {
                    product.Image = $"/{folderName}/{imageName}";
                    product.ImageThumnail = $"/{folderName}/Thumb/{imageName}";
                }
                else
                {
                    return BadRequestResponse(errorMessage);
                }
            }

            await _unitOfWorkRepo.Products.AddAsync(product);
            await _unitOfWorkRepo.SaveChangesAsync();

            var spec = new ProductWithTypeAndBrand(product.Id);
            var createdProduct = await _unitOfWorkRepo.Products.GetByIdWithSpec(spec);
            var modelDto = _mapper.Map<GetProductlDto>(createdProduct);

            return Created(
                actionName: nameof(ProductController.GetById),
                value: new { id = product.Id },
                Data: modelDto,
                controllerName: "Product",
                message: "Product created successfully!"
                );
        }


        [HttpPut("Edit-Product/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PostPutProductDto model)
        {
            var product = await _unitOfWorkRepo.Products.GetByIdAsync(id);
            if (product == null) return NotFoundResponse("Product Not Found.!");
            var oldImagePath = product.Image;
            var oldImageThumPath = product.ImageThumnail;
            _mapper.Map(model, product);

            if (model.Image != null)
            {


                var extension = Path.GetExtension(model.Image.FileName);
                var imageName = $"{Guid.NewGuid()}{extension}";
                var folderName = "Images/products";
                var (isUploaded, erromessage) = await _imageService.UploadAsync(model.Image, imageName, folderName, hasThumbnail: true);
                if (isUploaded)
                {
                    if (!string.IsNullOrEmpty(oldImagePath))
                        _imageService.Delete(oldImagePath, oldImageThumPath);
                    product.Image = $"/{folderName}/{imageName}";
                    product.ImageThumnail = $"/{folderName}/Thumb/{imageName}";
                }
                else
                    return BadRequestResponse(erromessage);
            }
            else if (!string.IsNullOrEmpty(oldImagePath))
            {
                product.Image = oldImagePath;
                product.ImageThumnail = oldImageThumPath;
            }
            _unitOfWorkRepo.Products.Update(product);
            await _unitOfWorkRepo.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("Delete-Product/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWorkRepo.Products.GetByIdAsync(id);
            if (product is null) return NotFoundResponse("Product Not Found.!");
            if (!string.IsNullOrEmpty(product.Image))
                _imageService.Delete(product.Image, product.ImageThumnail);
             _unitOfWorkRepo.Products.Delete(product);
            await _unitOfWorkRepo.SaveChangesAsync();
            return NoContent();
        }


    }
}
