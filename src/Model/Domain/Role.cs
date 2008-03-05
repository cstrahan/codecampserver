namespace CodeCampServer.Model.Domain
{
    public class Role : EntityBase
    {
        Person _person = new Person();
        public Role()
        {
        }

        public Role(Person person)
        {
            _person = person;
        }

        public virtual Person Person
        {
            get { return _person; }
            set { _person = value; }
        }
    }
}
