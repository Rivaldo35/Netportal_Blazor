using Netportal.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netportal.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}