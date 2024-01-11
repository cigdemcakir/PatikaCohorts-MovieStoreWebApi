namespace MovieStore.WebApi.Application.TokenOperations.Commands.RequestCommanModel
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
