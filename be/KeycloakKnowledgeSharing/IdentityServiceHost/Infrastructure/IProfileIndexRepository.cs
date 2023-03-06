using IdentityServiceHost.DTOs;

namespace IdentityServiceHost.Infrastructure;

public interface IProfileIndexRepository
{ 
    Task CreateProfileIndexAsync(ProfileIndex profileIndex);
}