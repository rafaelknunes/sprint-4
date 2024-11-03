namespace Sprint4.Services { 
public interface IFirebaseService
{
    Task<string> AuthenticateUserAsync(string email, string password);
}

}
