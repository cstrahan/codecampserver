using System;

namespace CodeCampServer.UI.Models.AutoMap
{
	public class ResolutionContext
	{
		public ResolutionContext(TypeMap typeMap, string memberName, PropertyMap propertyMap, Type modelMemberType, bool isModelMemberValueNull)
		{
			TypeMap = typeMap;
			MemberName = memberName;
			PropertyMap = propertyMap;
			ModelMemberType = modelMemberType;
			IsModelMemberValueNull = isModelMemberValueNull;
		}

		public string MemberName { get; private set; }
		public TypeMap TypeMap { get; private set; }
		public PropertyMap PropertyMap { get; private set; }
		public Type ModelMemberType { get; private set; }
		public bool IsModelMemberValueNull { get; private set; }
	}
}