using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;

namespace PierresTreats.Controllers;

public class HomeController : Controller 
{
  private readonly PierresTreatsContext _db;
  public HomeController(PierresTreatsContext db)
  {
    _db = db;
  }

  [HttpGet("/")]
      public ActionResult Index()
      {
        List<object[]> model = new List<object[]>();
        model.Add(_db.Flavors.ToArray());
        model.Add(_db.Treats.ToArray());       
        return View(model);
      }      
}
