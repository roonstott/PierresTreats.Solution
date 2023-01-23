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

    [AllowAnonymous]
    public ActionResult ReadOnly(int id)
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Details", new {id = id});
      }
      else
      {
        Flavor thisFlavor = _db.Flavors
            .Include(f => f.JoinEntities)
            .ThenInclude(j => j.Treat)
            .FirstOrDefault(f => f.FlavorId == id);
            
        List<string> names = new List<string>();
        foreach (FlavorTreat join in _db.FlavorTreats)
        {
          if (join.FlavorId == thisFlavor.FlavorId)
          {
            names.Add(join.Treat.Name);
          }
        }
        ViewBag.Names = names;
        return View (thisFlavor);
      }
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
    public ActionResult Details (int id)
    {      
      Flavor thisFlavor = _db.Flavors
            .Include(flav => flav.JoinEntities)
            .FirstOrDefault(flav => flav.FlavorId == id);        
      List<Treat> select = new List<Treat>();
      foreach (Treat treat in _db.Treats)
      {      
        select.Add(treat);        
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

    [HttpPost]
    public ActionResult Delete (int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      _db.Flavors.Remove(thisFlavor); 
      _db.SaveChanges(); 
      return RedirectToAction("Index", "Home");
    }
  }
}
