namespace CountryVATCalculator.Services
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("test") && password.Equals("test");
        }
    }

}