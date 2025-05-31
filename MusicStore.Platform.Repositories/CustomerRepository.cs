using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Platform.Repositories.Interfaces;
using MusicStore.Core.Db;
using MusicStore.Core.Data;
using Dapper;
using System.Data;

namespace MusicStore.Platform.Repositories
{
    public class CustomerRepository : ICustomerRepository //Dependency Inversion Principle
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection DbConnection) // DB injection, thats what we work on
        {
            _dbConnection = DbConnection;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var sql = "SELECT * FROM Customers";
            return await _dbConnection.QueryAsync<Customer>(sql);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Customers WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Customer customer)
        {
            var sql = "INSERT INTO Customers (Name, BirthDate) VALUES (@Name, @BirthDate); SELECT CAST(SCOPE_IDENTITY() as int)"; // Lets return fresh new Id
            var newId = await _dbConnection.QuerySingleAsync<int>(sql, customer);
            return newId; // We create new Customer and return the freshly created Id <- great !
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            var sql = "UPDATE Customers SET Name = @Name, BirthDate=@BirthDate WHERE Id = @Id"; 
            await _dbConnection.ExecuteAsync(sql, customer);
            return customer.Id; 
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Customers WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }

}