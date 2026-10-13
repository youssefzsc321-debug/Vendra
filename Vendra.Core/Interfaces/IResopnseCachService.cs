using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Interfaces
{
    public interface IResopnseCachService
    {
        Task CachResponseAsync(string cachKey, object response, TimeSpan timeToLive);

        Task<string>GetCachedRespone(string cachKey);
    }
}
