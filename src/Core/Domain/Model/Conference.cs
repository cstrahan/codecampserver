using System;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
    public class Conference:PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string LocationName { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string PhoneNumber { get; set; }        
    }
}