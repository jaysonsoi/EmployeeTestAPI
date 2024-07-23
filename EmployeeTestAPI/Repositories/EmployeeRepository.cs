using EmployeeTestAPI.Data;
using EmployeeTestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EmployeeTestAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee id {id} not found");
            }

            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
           _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
