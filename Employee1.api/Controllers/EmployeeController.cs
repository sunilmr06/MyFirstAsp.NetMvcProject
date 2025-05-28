using Employee1.api.Data;
using Employee1.api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee1.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allEmployees = dbContext.Employees.Include(e => e.Department).ToList();
            return Ok(allEmployees);
        }

        [HttpPost]
        public IActionResult Add(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                EmpCode = addEmployeeDto.EmpCode,
                Firstname = addEmployeeDto.Firstname,
                Lastname = addEmployeeDto.Lastname,
                Email = addEmployeeDto.Email,
                Contact = addEmployeeDto.Contact,
                DepartmentId = addEmployeeDto.DepartmentId,
                DesignationId = addEmployeeDto.DesignationId
                

            };
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var Employee = dbContext.Employees.Find(id);

            if(Employee == null)
            {
                return NotFound();
            }
            return Ok(Employee); 

        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, UpdateEmployeeDto updateEmployeeDto)
        {
          var Employee =  dbContext.Employees.Find(id);

            if(Employee == null)
            {
                return NotFound();
            }
            Employee.EmpCode = updateEmployeeDto.EmpCode;
            Employee.Firstname = updateEmployeeDto.Firstname;
            Employee.Lastname = updateEmployeeDto.Lastname;
            Employee.Email = updateEmployeeDto.Email;
            Employee.Contact = updateEmployeeDto.Contact;
            Employee.DepartmentId = updateEmployeeDto.DepartmentId;
            Employee.DesignationId = updateEmployeeDto.DesignationId;

            dbContext.SaveChanges();

            return Ok(Employee);
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
