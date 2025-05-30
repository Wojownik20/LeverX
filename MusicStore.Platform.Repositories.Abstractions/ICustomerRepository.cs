using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Core.Data;

namespace MusicStore.Platform.Repositories.Interfaces;

public interface ICustomerRepository // This guy is the one and only talking to the DB
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<int> AddAsync (Customer customer);
    Task<int> UpdateAsync (Customer customer); 
    Task DeleteAsync (int id);
}