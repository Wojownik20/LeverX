using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Platform.Repositories.Interfaces;
using MusicStore.Core.Db;
using MusicStore.Core.Data;
using Dapper;
using System.Data;


namespace MusicStore.Platform.Repositories
{
    public class ProductRepository : IProductRepository //Dependency Inversion Principle
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection Dbconnection) // DB injection, thats what we work on
        {
            _dbConnection = Dbconnection;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            return await _dbConnection.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Product product)
        {
            var sql = "INSERT INTO Products (Name, Category, Price, ReleaseDate) VALUES (@Name, @Category, @Price, @ReleaseDate); SELECT CAST(SCOPE_IDENTITY() as int)";
            var newId = await _dbConnection.QuerySingleAsync<int>(sql, product);
            return newId; 
        }

        public async Task<int> UpdateAsync(Product product)
        {
            var sql = "UPDATE Products SET Name = @Name, Category = @Category, Price = @Price, ReleaseDate = @ReleaseDate WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, product);
            return product.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }



}