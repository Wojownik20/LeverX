using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Platform.Repositories.Interfaces;
using MusicStore.Core.Db;
using MusicStore.Core.Data;
using Dapper;
using System.Data;

namespace MusicStore.Platform.Repositories
{
    public class OrderRepository : IOrderRepository //Dependency Inversion Principle
    {
        private readonly IDbConnection _dbConnection;

        public OrderRepository(IDbConnection DbConnection) // DB injection, thats what we work on
        {
            _dbConnection = DbConnection;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var sql = "SELECT * FROM Orders";
            return await _dbConnection.QueryAsync<Order>(sql);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Orders WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Order order)
        {
            var sql = "INSERT INTO Orders (ProductId, CustomerId, EmployeeId, TotalPrice, PurchaseDate) VALUES (@ProductId, @CustomerId, @EmployeeId, @TotalPrice, @PurchaseDate); SELECT CAST(SCOPE_IDENTITY() as int)";
            var newId = await _dbConnection.QuerySingleAsync<int>(sql, order);
            return newId;
        }

        public async Task<int> UpdateAsync(Order order)
        {
            var sql = "UPDATE Orders SET ProductId = @ProductId, CustomerId = @CustomerId, EmployeeId = @EmployeeId, TotalPrice = @TotalPrice, PurchaseDate = @PurchaseDate WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, order);
            return order.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Orders WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }



}