using SLBFE_Employement_API.Models;
using SLBFE_Employement_API.Models.RequestResponseModels;

namespace SLBFE_Employement_API.Extensions
{
    public static class UsersMapper
    {
        public static Users ToModel(this CreateUserRequest request) => new()
        {
            UserType = request.UserType,
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        };

        public static Users ToModel(this CreateCompanyUserRequest request) => new()
        {
            UserType = request.UserType,
            Name = request.CompanyName,
            Email = request.Email,
            Password = request.Password,
            CompanyDetails = new Company() {
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
            },
        };
    }
}
