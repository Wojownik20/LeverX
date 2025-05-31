using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Core.Data;
using MusicStore.Platform.Services.Interfaces;
using MusicStore.Platform.Repositories.Interfaces;

namespace MusicStore.Platform.Services
{
    public class EmployeeService : IEmployeeService //Dependency Inversion Principle
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id); // Async getting list of customers
        }
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            return employee.Id;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
            return employee.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }



}