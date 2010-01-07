using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Bases
{
    public interface IRepository<T> where T : PersistentObject
    {
        T GetById(object id);
        void Save(T entity);
        T[] GetAll();
        void Delete(T entity);
    }
}