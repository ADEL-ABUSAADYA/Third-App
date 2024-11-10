using System.Linq.Expressions;

namespace Third_App.BL;

public interface IGenaricRep<E> 
{
    void Add(E entity);
    void Delete(E entity);
    void HardDelete(E entity);
    void SaveInclude(E entity, params string[] props);
    void SaveExclude(E entity, params string[] props);
    IQueryable<E> Get(Expression<Func<E, bool>> filter);
    IQueryable<E> Get();
    void Save();
}
