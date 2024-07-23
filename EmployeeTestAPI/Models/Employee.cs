using System.ComponentModel.DataAnnotations;

namespace EmployeeTestAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
