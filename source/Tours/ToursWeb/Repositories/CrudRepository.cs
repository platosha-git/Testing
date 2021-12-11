using System.Collections.Generic;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface CrudRepository<T, ID>
    {
        List<T> FindAll();
        T FindByID(ID id);
        ExitCode Add(T obj);
        ExitCode Update(T obj);
        ExitCode DeleteByID(ID id);
    }
}
