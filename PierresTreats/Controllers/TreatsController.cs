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
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
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
        Treat thisTreat = _db.Treats
            .Include(t => t.JoinEntities)
            .ThenInclude(j => j.Flavor)
            .FirstOrDefault(t => t.TreatId == id);
        List<string> names = new List<string>();
        foreach (FlavorTreat join in _db.FlavorTreats)
        {
          if (join.TreatId == thisTreat.TreatId)
          {
            names.Add(join.Flavor.Name);
          }
        }
        ViewBag.Names = names;
        return View (thisTreat);
      }
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }
    [Authorize]
    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      if (!ModelState.IsValid)
      {
        return View(treat);
      }
      else
      {        
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
      }
    }
    [Authorize]
    public ActionResult Details (int id)
    {      
      Treat thisTreat = _db.Treats
            .Include(tr => tr.JoinEntities)
            .FirstOrDefault(tr => tr.TreatId == id);      
      List<Flavor> select = new List<Flavor>();
      foreach (Flavor flavor in _db.Flavors)
      {          
        select.Add(flavor);        
      }
      foreach (FlavorTreat join in thisTreat.JoinEntities)
      {
        select.Remove(join.Flavor);
      }      
      ViewBag.FlavorId = new SelectList(select, "FlavorId", "Name");      
      return View(thisTreat);
    }
    [Authorize]
    [HttpPost, ActionName("Details")]
    public ActionResult AddJoin( int flavorId, Treat treat)
    {
      #nullable enable
      FlavorTreat ? check = _db.FlavorTreats.FirstOrDefault(j => (j.FlavorId == flavorId && j.TreatId == treat.TreatId)); 
      #nullable disable
      if (check == null && flavorId !=0)
      { 
        FlavorTreat join = new FlavorTreat {TreatId = treat.TreatId, FlavorId = flavorId};
        _db.FlavorTreats.Add(join); 
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = treat.TreatId});
    }          [HttpPost]
    public ActionResult DeleteJoin (int joinId, int trId)
    {
      FlavorTreat join = _db.FlavorTreats.FirstOrDefault(j => j.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(join); 
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = trId});
    }
  }
}
