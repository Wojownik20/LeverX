using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MusicStore.Core.Data;
using MusicStore.Core.Db;
using MusicStore.Platform.Repositories.Interfaces;

namespace MusicStore.Platform.Repositories
{
    public class EmployeeRepository : IEmployeeRepository //Dependency Inversion Principle
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection DbConnection) // DB injection, thats what we work on
        {
            _dbConnection = DbConnection;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var sql = "SELECT * FROM Employees";
            return await _dbConnection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Employees WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Employee employee)
        {
            var sql = "INSERT INTO Employees (Name, BirthDate, Salary) VALUES (@Name, @BirthDate, @Salary); SELECT CAST(SCOPE_IDENTITY() as int)";
            var newSql = await _dbConnection.QuerySingleAsync<int>(sql, employee);
            return newSql; 
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            var sql = "UPDATE Employees SET Name = @Name, BirthDate = @BirthDate, Salary = @Salary WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, employee);
            return employee.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Employees WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }



}