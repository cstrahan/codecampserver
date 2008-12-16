using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using CodeCampServer.Core;

namespace CodeCampServer.UI.Models.AutoMap
{
	public class PropertyMap
	{
		private readonly LinkedList<TypeMember> _modelMemberChain = new LinkedList<TypeMember>();
		private readonly IList<Type> _valueFormattersToSkip = new List<Type>();
		private readonly IList<IValueFormatter> _valueFormatters = new List<IValueFormatter>();
		private string _nullSubstitute;
		private IValueResolver _valueResolver;
		private bool _hasMembersToResolveForCustomResolver;

		public PropertyMap(PropertyInfo dtoProperty)
		{
			DtoProperty = dtoProperty;
		}

		public PropertyMap(PropertyInfo dtoProperty, IEnumerable<TypeMember> typeMembers)
		{
			DtoProperty = dtoProperty;
			ChainTypeMembers(typeMembers);
		}

		public bool HasMembersToResolveForCustomResolver
		{
			get { return _hasMembersToResolveForCustomResolver; }
		}

		public PropertyInfo DtoProperty { get; set; }

		public ReadOnlyCollection<TypeMember> GetModelMemberChain()
		{
			return new ReadOnlyCollection<TypeMember>(_modelMemberChain.ToList());
		}

		public void RemoveLastModelProperty()
		{
			_modelMemberChain.RemoveLast();
		}

		public void ChainTypeMember(TypeMember typeMember)
		{
			_modelMemberChain.AddLast(typeMember);
		}

		public TypeMember GetLastModelMemberInChain()
		{
			return _modelMemberChain.Last.Value;
		}

		public void ChainTypeMembers(IEnumerable<TypeMember> typeMembers)
		{
			typeMembers.ForEach(ChainTypeMember);
		}

		public void AddFormatterToSkip<TValueFormatter>() where TValueFormatter : IValueFormatter
		{
			_valueFormattersToSkip.Add(typeof(TValueFormatter));
		}

		public bool FormattersToSkipContains(Type valueFormatterType)
		{
			return _valueFormattersToSkip.Contains(valueFormatterType);
		}

		public void AddFormatter(IValueFormatter valueFormatter)
		{
			_valueFormatters.Add(valueFormatter);
		}

		public IValueFormatter[] GetFormatters()
		{
			return _valueFormatters.ToArray();
		}

		public void FormatNullValueAs(string nullSubstitute)
		{
			_nullSubstitute = nullSubstitute;
		}

		public object GetNullValueToUse()
		{
			return _nullSubstitute;
		}

		public void AssignCustomValueResolver(IValueResolver valueResolver)
		{
			_valueResolver = valueResolver;
		}

		public IValueResolver GetCustomValueResolver()
		{
			return _valueResolver;
		}

		public void ChainTypeMembersForResolver(IEnumerable<TypeMember> typeMembers)
		{
			ChainTypeMembers(typeMembers);
			_hasMembersToResolveForCustomResolver = true;
		}
	}
}