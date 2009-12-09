using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Infrastructure.DataAccess.Mappings
{
	public class SponsorMap : AuditedEntityClassMap<Sponsor>
	{
		public SponsorMap()
		{
			Table("Sponsors");
			Map(x => x.Name).Length(100);
			Map(x => x.Url).Length(255);
			Map(x => x.BannerUrl).Length(255);
			Map(x => x.Level);
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