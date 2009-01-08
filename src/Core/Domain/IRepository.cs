using System;
using CodeCampServer.Core.Domain.Model;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain
{
	public interface IRepository<T> where T : PersistentObject
	{
		T GetById(Guid id);
		void Save(T entity);
		T[] GetAll();
	}

	public interface IKeyedRepository<T> : IRepository<T> where T : KeyedObject
	{
		T GetByKey(string key);
	}
}