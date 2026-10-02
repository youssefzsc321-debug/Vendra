using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Core.Interfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetCustomerBasket(string Id);
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket);

        Task<bool>DeleteCustomerBasket(string  Id);
         

    }
}
