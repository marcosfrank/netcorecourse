using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.FirstExample.WebApp.DataAccess;

namespace NetCoreCourse.FirstExample.WebApp.Controllers.EF
{
    [Route("api/[controller]")]
    public class UnitOfWorkController  : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult TestingUom()
        {
            var things = unitOfWork.ThingRepository.GetAll();
            return Ok(things);
        }
    }
}
