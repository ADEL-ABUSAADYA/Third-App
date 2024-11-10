using System.ComponentModel;

namespace Third_App.DAL;

public class BaseModel
{
    public int ID { get; set;}
    public bool Deleted { get; set; }
    
    public DateTime CreatedOn { get;} = DateTime.Now;

    
}
