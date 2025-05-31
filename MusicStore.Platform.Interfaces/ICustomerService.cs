using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Core.Data;

namespace MusicStore.Platform.Services.Interfaces
{
    public interface ICustomerService // Reminder : Interface is a bunch of essential methods
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<int> CreateCustomerAsync (Customer customer);
        Task<int> UpdateCustomerAsync (Customer customer);
        Task DeleteCustomerAsync (int id);  
    }
}
