namespace Hygie.Infrastructure.Common.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJWTToken((string userId, string userName, IList<string>? roles) userDetails);
    }
}
