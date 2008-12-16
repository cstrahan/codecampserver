using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Type;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Infrastructure.DataAccess
{
	public class IdCacheInterceptor : EmptyInterceptor
	{
		private static readonly Dictionary<string, Guid> _idBag = new Dictionary<string, Guid>();
		private static readonly Dictionary<Type, string> _keyNames = new Dictionary<Type, string>();
		public static bool Enabled = true;

		public static void ResetState()
		{
			_idBag.Clear();
			_keyNames.Clear();	
		}

		private static string GetKey(PersistentObject obj)
		{
			Type type = obj.GetType();
			object propValue = type.GetProperty(_keyNames[type]).GetGetMethod().Invoke(obj, new object[0]);
			return string.Format("{0}|{1}", type.FullName, propValue);
		}

		public override void PostFlush(ICollection entities)
		{
			if(!Enabled) return;

			foreach (var ent in entities)
			{
				var obj = ent as PersistentObject;
				string key = GetKey(obj);
				if (obj != null && !_idBag.ContainsKey(key))
				{
					_idBag.Add(key, obj.Id);
				}
			}

			base.PostFlush(entities);
		}

		public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
		{
			if (Enabled)
			{
				if (entity is PersistentObject)
				{
					Type type = entity.GetType();
					if (!_keyNames.ContainsKey(type))
					{
						foreach (var info in type.GetProperties())
						{
							object propValue = info.GetGetMethod().Invoke(entity, new object[0]);
							if (propValue == id)
							{
								_keyNames.Add(type, info.Name);
								break;
							}
						}
					}

					var o = (PersistentObject) entity;
					if (_idBag.ContainsKey(GetKey(o)))
					{
						o.Id = _idBag[GetKey(o)];
					}
				}
			}

			return base.OnLoad(entity, id, state, propertyNames, types);
		}
	}
}