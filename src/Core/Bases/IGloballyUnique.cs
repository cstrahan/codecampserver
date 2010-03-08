using System;

namespace CodeCampServer.Core.Bases
{
	public interface IGloballyUnique
	{
		Guid Id { get; }
	}
}