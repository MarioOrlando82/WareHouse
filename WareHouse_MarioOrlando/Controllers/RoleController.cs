using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WareHouse_MarioOrlando.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
