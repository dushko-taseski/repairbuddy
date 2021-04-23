using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace App.Web.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        public int LoggedInUserId
        {
            get
            {
                int.TryParse(HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value, out var userId);
                return userId;
            }
        }
    }
}
