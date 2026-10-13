using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;

namespace Vendra.Core.Dtos.Identity
{
    public class AdminRegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression(RegexPatterns.PasswordRegex, ErrorMessage = Errors.PasswordErrorMessage)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = Errors.ConfirmPass)]
        public string ConfirmPassword { get; set; }
    }
}
