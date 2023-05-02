using Albelli.OrderManagement.Models;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.DAL.Contracts.Repositories
{
    public interface IProductInfoRepository
    {
        Task<ProductInfo> Get(ProductType productType);
    }
}
