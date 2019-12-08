using System.ComponentModel.DataAnnotations;

namespace meowtown_api.Models
{
  public class Cat{
    public long Id {get;set;}
    public string Name {get;set;}
    public byte Lives {get; set;} 
  }
  public class CatInputModel {
    [Required()]
    public string Name {get;set;}
  }
}
