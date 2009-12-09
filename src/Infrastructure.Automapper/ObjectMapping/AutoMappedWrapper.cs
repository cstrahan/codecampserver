using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace CodeCampServer.Infrastructure.ObjectMapping
{
	public class AutoMappedWrapper
	{
		public static object Map(object source, Type sourceType, Type destinationType)
		{
			if (source is IEnumerable)
			{
				IEnumerable<object> input = ((IEnumerable) source).OfType<object>();
				Array a = Array.CreateInstance(destinationType.GetElementType(), input.Count());

				int index = 0;
				foreach (object data in input)
				{
					a.SetValue(Mapper.Map(data, data.GetType(), destinationType.GetElementType()), index);
					index++;
				}
				return a;
			}
			else
			{
				return Mapper.Map(source, sourceType, destinationType);
			}
		}
	}
}