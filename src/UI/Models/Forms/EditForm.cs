using System;
using CodeCampServer.Core;
using CodeCampServer.UI.Helpers.Mappers;

namespace CodeCampServer.UI.Models.Forms
{
	public abstract class EditForm<T> : ValueObject<T> where T : class
	{
		public abstract Guid Id { get; set; }

		public virtual EditMode GetEditMode()
		{
			if (Guid.Empty.Equals(Id))
			{
				return EditMode.Add;
			}

			return EditMode.Edit;
		}
	}
}