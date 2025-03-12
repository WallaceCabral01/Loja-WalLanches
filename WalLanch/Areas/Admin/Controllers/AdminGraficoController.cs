using Microsoft.AspNetCore.Mvc;

namespace WalLanches.Areas.Admin.Controllers
{
    public class AdminGraficoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
