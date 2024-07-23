using EmployeeTestAPI.Models;
using EmployeeTestAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeeAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return Ok(employees);
        }
        //https://localhost:1111/api/employees/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeById(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent(); 
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
        }

    }
}
