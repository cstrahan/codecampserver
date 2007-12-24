namespace CodeCampServer.Domain.Model
{
    public class ConfirmedSponsor
    {
        private Sponsor _sponsor;
        private SponsorLevel _level;

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