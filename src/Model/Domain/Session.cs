using Iesi.Collections.Generic;

namespace CodeCampServer.Model.Domain
{
	public class Session : EntityBase
	{
		private Speaker _speaker;
		private string _title;
		private string _abstract;
		private ISet<OnlineResource> _onlineResources = new HashedSet<OnlineResource>();

		public Session()
		{
		}

		public Session(Speaker speaker, string title, string @abstract)
		{
			_speaker = speaker;
			_title = title;
			_abstract = @abstract;
		}

		//TODO:  Please do not expose internal data structures in public interfaces.
		//Argument should either be of type OnlineResource[] or IEnumerable<OnlineResource>
		public Session(Speaker speaker, string title, string @abstract, ISet<OnlineResource> onlineResources)
		{
			_speaker = speaker;
			_title = title;
			_abstract = @abstract;
			_onlineResources = onlineResources;
		}

		public virtual string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		public virtual string Abstract
		{
			get { return _abstract; }
			set { _abstract = value; }
		}

		public virtual Speaker Speaker
		{
			get { return _speaker; }
			set { _speaker = value; }
		}

		//TODO: Convert to method GetResources() : OnlineResource[]
		public virtual ISet<OnlineResource> Resources
		{
			get { return _onlineResources; }
		}

		public virtual void AddResource(OnlineResource resource)
		{
			_onlineResources.Add(resource);
		}
	}
}