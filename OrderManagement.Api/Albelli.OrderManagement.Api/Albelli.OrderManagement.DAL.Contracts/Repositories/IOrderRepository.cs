
using Albelli.OrderManagement.Models;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.DAL.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        Task<Order> Get(int orderId);
    }
}
