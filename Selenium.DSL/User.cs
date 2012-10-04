namespace Selenium.DSL
{
    public class User:UserBase
    {
        public override string UserName
        {
            get { return "User"; }
        }

        public override string Password
        {
            get { return "testing"; }
        }
    }
}