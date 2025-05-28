using Employee1.api.Data;
using Employee1.api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee1.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DesignationController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allDesignation = dbContext.Designations.ToList();
            return Ok(allDesignation);
        }
        [HttpPost]
        public IActionResult Post(AddDesignationDto designation)
        {
            var designationentity = new Designation()
            {
                DesignationName = designation.DesignationName
            };
            dbContext.Designations.Add(designationentity);
            dbContext.SaveChanges();
            return Ok(designationentity);
        }
        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var designation = dbContext.Designations.Find(id);

            if (designation == null)
            {
                return NotFound();
            }
            return Ok(designation);

        }
        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long id, UpdateDesignationDto designationDto)
        {
            var Designation = dbContext.Designations.Find(id);

            if (Designation == null)
            {
                return NotFound();
            }
            Designation.DesignationName = designationDto.DesignationName;
            dbContext.SaveChanges();

            return Ok(Designation);

        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var designation = dbContext.Designations.Find(id);

            if (designation == null)
            {
                return NotFound();
            }

            dbContext.Designations.Remove(designation);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
