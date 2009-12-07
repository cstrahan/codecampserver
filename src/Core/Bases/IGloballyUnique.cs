using System;

namespace CodeCampServer.Core.Domain.Model
{
	public interface IGloballyUnique
	{
		Guid Id { get; }
	}
}