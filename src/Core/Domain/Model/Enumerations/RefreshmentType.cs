using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Model.Enumerations
{
    public class RefreshmentType : Enumeration
    {
        public static RefreshmentType Pizza = new RefreshmentType(1, "Pizza and Soda");
        public static RefreshmentType Cookies = new RefreshmentType(2 , "Cookies");


        public RefreshmentType(int value, string displayName) : base(value, displayName) {}
        public RefreshmentType() {}
    }
}