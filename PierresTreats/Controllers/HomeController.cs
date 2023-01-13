using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers;

public class HomeController : Controller 
{
  private readonly PierresTreatsContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public HomeController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
  {
    _userManager = userManager;
    _db = db;
  }

  public ActionResult Index() 
  {
    return View();
  }

}
