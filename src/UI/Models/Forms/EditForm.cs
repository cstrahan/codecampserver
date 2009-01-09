using System;
using CodeCampServer.Core.Messages;

namespace CodeCampServer.UI.Models.Forms
{
	public abstract class EditForm
	{
		public abstract Guid Id { get; set; }

		public EditMode GetEditMode()
		{
			if (Guid.Empty.Equals(Id))
			{
				return EditMode.Add;
			}

			return EditMode.Edit;
		}
	}
}