using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models;

public class Flavor
{
  public int FlavorId { get; set; }
  [Required(ErrorMessage = "The flavor must have a name")]
  public string Name { get; set; }
  public List<FlavorTreat> JoinEntities { get; }
  public ApplicationUser User { get; set; }
}
