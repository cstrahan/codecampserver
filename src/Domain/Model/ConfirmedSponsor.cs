namespace CodeCampServer.Domain.Model
{
    public class ConfirmedSponsor
    {
        private Sponsor _sponsor;
        private SponsorLevel _level;

        private ConfirmedSponsor()
        {
        }

        public ConfirmedSponsor(Sponsor sponsor, SponsorLevel level)
        {
            _sponsor = sponsor;
            _level = level;
        }

        public virtual Sponsor Sponsor
        {
            get { return _sponsor; }
            set { _sponsor = value; }
        }

        public virtual SponsorLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }
}