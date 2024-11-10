using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Third_App.DAL;

namespace Third_App.BL;

public class GenaricRep<E> : IGenaricRep<E> where E : BaseModel
{

    AppDbContext _appDbContext;
    DbSet<E> dbSet;


    public GenaricRep()
    {
        _appDbContext = new AppDbContext();
        dbSet = _appDbContext.Set<E>();
    }
    public void Add(E entity)
    {
        dbSet.Add(entity);
    }

    public void Delete(E entity)
    {
        throw new NotImplementedException();
    }

    public void HardDelete(E entity)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        _appDbContext.SaveChanges();
    }

    public void SaveExclude(E entity, params string[] props)
    {
        var local = dbSet.Local.FirstOrDefault(x => x.ID == entity.ID);
        EntityEntry entityEntry= null;

        if (local == null){
            entityEntry = _appDbContext.Entry(entity);
        }else{
            entityEntry = _appDbContext.ChangeTracker.Entries<E>().FirstOrDefault(x => x.Entity.ID == entity.ID);
        }

        
        foreach (var property in entityEntry.Properties)
        {
            if (!props.Contains(property.Metadata.Name) && !property.Metadata.IsPrimaryKey())
            {
                property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                property.IsModified = true;
            }
        }
    }

    public void SaveInclude(E entity, params string[] props)
    {

        var local = dbSet.Local.FirstOrDefault(x => x.ID == entity.ID);
        EntityEntry entityEntry= null;

        if (local == null){
            entityEntry = _appDbContext.Entry(entity);
        }else{
            entityEntry = _appDbContext.ChangeTracker.Entries<E>().FirstOrDefault(x => x.Entity.ID == entity.ID);
        }
        
            
        foreach (var property in entityEntry.Properties)
        {
            if (props.Contains(property.Metadata.Name) && !property.Metadata.IsPrimaryKey())
            {
                property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                property.IsModified = true;
            }
        }

        
    }

    public IQueryable<E> Get(Expression<Func<E, bool>> filter)
    {
        return dbSet.Where(filter);
    }

    public IQueryable<E> Get()
    {    
        return dbSet;
    }

    
}  
