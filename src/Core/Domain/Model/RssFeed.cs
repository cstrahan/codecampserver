using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
    public class RssFeed:PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
    }
}
