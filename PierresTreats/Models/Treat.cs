using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models;

public class Treat
{
  public int TreatId { get; set; }
  [Required(ErrorMessage = "The treat must have a name")]
  public string Name { get; set; }
  public List<FlavorTreat> JoinEntities { get; }
  public ApplicationUser User { get; set; }
}
