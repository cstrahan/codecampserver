using System;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IRepository<T> where T : PersistentObject
	{
		T GetById(Guid id);
		void Save(T entity);
		T[] GetAll();
	}
}