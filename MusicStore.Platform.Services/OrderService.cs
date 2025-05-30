using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Shared.Models;
using MusicStore.Platform.Services.Interfaces;
using MusicStore.Core.Data;
using MusicStore.Platform.Repositories.Interfaces;

namespace MusicStore.Platform.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository; //Private Repo injected in here

        public OrderService(IOrderRepository orderRepository) // ICustomerRepo injected into Service
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            return order.Id;
        }

        public async Task<int> UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
            return order.Id;
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}