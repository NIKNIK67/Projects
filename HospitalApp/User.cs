namespace HospitalApp
{
    public abstract class User : IUser
    {
        public int id { get; set; }
        public UserRole role { get; set ; }
        public  string name { get ; set; }
    }


}
