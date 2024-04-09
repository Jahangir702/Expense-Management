using Microsoft.AspNetCore.Mvc;

/*
 * Created by   : Jahangir
 * Date created : 30.03.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  : 
 * Date reviewed:
 */
namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}