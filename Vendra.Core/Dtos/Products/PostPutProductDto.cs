using Vendra.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;
using Vendra.Core.Enities;

namespace Vendra.Core.Dtos.Products
{
    public class PostPutProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage =Errors.MaxLength)]
        public string Name { get; set; }
        [MaxLength(200,ErrorMessage =Errors.MaxLength)]
        public string? Description { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage =Errors.maxValueError)]
        public decimal Price { get; set; }
        [RequiredIf("Id", 0, ErrorMessage = "Image is required for new products.")]
        public IFormFile Image { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }

    }
}

 



