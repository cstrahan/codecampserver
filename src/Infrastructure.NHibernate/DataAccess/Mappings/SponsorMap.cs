using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Bases;
using FluentNHibernate.Mapping;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess.Mappings
{
	public class SponsorMap : ClassMap<Sponsor>
	{
		public SponsorMap()
		{
			Cache.ReadWrite();
			DynamicUpdate();
			Id(x => x.Id).UnsavedValue(0).GeneratedBy.Identity();
			this.ChangeAuditInfo();
			Table("Sponsors");
			Map(x => x.Name).Length(100);
			Map(x => x.Url).Length(255);
			Map(x => x.BannerUrl).Length(255);
			Map(x => x.Level);
			References(x => x.UserGroup);
		}
	}

/*
		<class name="Sponsor" table="Sponsors" dynamic-update="true">
		<id name="Id" column="Id" type="Guid">
			<generator class="guid.comb"/>
		</id>
		<property name="Name" length="100"/>
		<property name="Url" length="255"/>
    <property name="BannerUrl" length="255"/>
    <property name="Level" type="CodeCampServer.Infrastructure.EnumerationType`1[[CodeCampServer.Core.Domain.Model.Enumerations.SponsorLevel, CodeCampServer.Core]], CodeCampServer.Infrastructure"/>
	</class>*/
}