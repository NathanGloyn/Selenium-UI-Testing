namespace Selenium.DSL
{
    public class Admin: UserBase
    {
        public override string UserName
        {
            get
            {
                return "Admin";
            }
        }

        public override string Password
        {
            get
            {
                return "testing";
            }
        }
    }
}
