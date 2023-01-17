using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
        return View(flavor);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        flavor.User = currentUser;
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
      }
    }

    public async Task<ActionResult> Details (int id)
    {      
      Flavor thisFlavor = _db.Flavors
            .Include(flav => flav.JoinEntities)
            .FirstOrDefault(flav => flav.FlavorId == id);

      List<Treat> select = new List<Treat>();
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      foreach (Treat treat in _db.Treats)
      {
        if (treat.User == currentUser)
        {
          select.Add(treat);
        }
      }
      foreach (FlavorTreat join in thisFlavor.JoinEntities)
      {
        select.Remove(join.Treat);
      }
      
      ViewBag.TreatId = new SelectList(select, "TreatId", "Name");
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Details")]
    public ActionResult AddJoin( int treatId, Flavor flavor)
    {
      #nullable enable
      FlavorTreat ? check = _db.FlavorTreats.FirstOrDefault(j => (j.TreatId == treatId && j.FlavorId == flavor.FlavorId)); 
      #nullable disable
      if (check == null && treatId !=0)
      { 
        FlavorTreat join = new FlavorTreat {TreatId = treatId, FlavorId = flavor.FlavorId};
        _db.FlavorTreats.Add(join); 
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = flavor.FlavorId});
    }  

    [HttpPost]
    public ActionResult DeleteJoin (int joinId, int flavId)
    {
      FlavorTreat join = _db.FlavorTreats.FirstOrDefault(j => j.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(join); 
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = flavId});
    }
  }
}
