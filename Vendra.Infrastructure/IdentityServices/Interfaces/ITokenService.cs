using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Identity;

namespace Vendra.Infrastructure.IdentityServices.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user,UserManager<AppUser> userManager);
    }
}
