using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Identity;
using Vendra.Core.Enities.Identity;

namespace Vendra.Core.Helpers.Mapping
{
    public class UserMapping: Profile
    {
        public UserMapping()
        {
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>new Address 
                {
                    City = src.City,
                    Country = src.Country,
                    Street = src.Street,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                }));

            CreateMap<Address, AddressDto>().ReverseMap();
                    
        }
    }
}
