using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Enities.Identity
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
