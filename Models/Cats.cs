using System.ComponentModel.DataAnnotations;

namespace meowtown_api.Models
{
  public class Cat : ICatInputModel{
    public long Id {get;set;}
    [Required()]
    public string Name {get;set;}
    public byte Lives {get; set;} 
  }
  public interface ICatInputModel {
    public string Name {get;set;}
  }
}
