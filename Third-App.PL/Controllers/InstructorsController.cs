using Microsoft.AspNetCore.Mvc;
using Third_App.BL;
using Third_App.DAL;

namespace Third_App.PL;
[ApiController]
[Route("[controller]/[action]")]
public class InstructorsController : ControllerBase
{
    private readonly IGenaricRep<Instructor> _instructors;
    public InstructorsController(){
        _instructors = new GenaricRep<Instructor>();

    }

    [HttpPost]
    public Instructor Add(Instructor instructor){
        instructor.Deleted = false;
        _instructors.Add(instructor);
        _instructors.Save();
        return instructor;
    }

    [HttpGet]
    public IEnumerable<Instructor> GetAll(){
        return _instructors.Get(x => x.Deleted == false);
    }

    [HttpPut("UpdateWithExclude")]
    public void UpdateWithExclude(int id, string email, string name){
        Instructor instructor = new Instructor{ID= id, Email=email, Name= name};

        _instructors.SaveExclude(instructor, nameof(Instructor.CreatedOn), nameof(Instructor.Deleted));
        _instructors.Save();
    }

    [HttpPut("UpdateWithInclude")]
    public void UpdateWithInclude(int id, string email){
        Instructor instructor = new Instructor{ID= id, Email=email};

        _instructors.SaveInclude(instructor, nameof(Instructor.Email));
        _instructors.Save();
    }
}
