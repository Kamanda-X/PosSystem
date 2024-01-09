using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;

namespace POS.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected PosDbContext _context;

        public BaseController(PosDbContext context)
        {
            _context = context;
        }
    }
}
