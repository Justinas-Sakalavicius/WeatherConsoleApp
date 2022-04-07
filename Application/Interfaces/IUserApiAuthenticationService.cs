namespace Application.Interfaces
{
    public interface IUserApiAuthenticationService
    {
        Task<string> RetrieveToken();
    }
}
