using Employee1.api.Data;
using Employee1.api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee1.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allDepartment = dbContext.Departments.ToList();
            return Ok(allDepartment);
        }
        [HttpPost]
        public IActionResult Post(AddDepartmentDto department)
        {
            var DepatmentEntity = new Department()
            {
                DepartmentName = department.DepartmentName,
                Discrtipion = department.Discrtipion
            };
            dbContext.Departments.Add(DepatmentEntity);
            dbContext.SaveChanges();
            return Ok(DepatmentEntity);    
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var Department = dbContext.Departments.Find(id);
            if(Department == null)
            {
                return NotFound();
            }
            return Ok(Department);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var department = dbContext.Departments.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            dbContext.Departments.Remove(department);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
