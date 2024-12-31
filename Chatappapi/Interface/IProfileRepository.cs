using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Interface
{
    public interface IProfileRepository
    {
        public  Task<Users?> GetProfileData(Guid model);

        public Task<int?> UpdateProfileData(updateProfile model, string? profileUrl);
    }
}
