using Hygie.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hygie.Infrastructure.Common.Interfaces;
using Hygie.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Hygie.Core.Entities;
using Microsoft.SqlServer.Server;
using System.Web;
using System.Threading.Tasks;

namespace Hygie.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName) ?? throw new NotFoundException("User not found");
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }


        // Return multiple value
        public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, string? role)
        {
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            if (role != null)
            {
                var addUserRole = await _userManager.AddToRoleAsync(user, role);

                if (!addUserRole.Succeeded)
                {
                    throw new ValidationException(addUserRole.Errors);
                }
            }
            return (result.Succeeded, user.Id);
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId) ?? throw new NotFoundException("Role not found");
            if (roleDetails.Name == "Administrator")
            {
                throw new BadRequestException("You can not delete Administrator Role");
            }
            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User not found");
            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<(string id, string? fullName, string? userName, string? email)>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.Email
            }).ToListAsync();

            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return roles.Select(role => (role.Id, role.Name!)).ToList();
        }

        public async Task<(string userId, string? fullName, string? UserName, string? email,bool? emailVerified,string? phoneNumber, IList<string>? roles,Adress? adress, ProfilePicture? profileImage)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users
                .Include(u => u.ProfilePicture)
                .Include(u => u.Adress)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User not found");
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, user.EmailConfirmed,user.PhoneNumber, roles, user.Adress, user.ProfilePicture);
        }

        public async Task<(string userId, string? fullName, string? UserName, string? email, IList<string>? roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName) ?? throw new NotFoundException("User not found");
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user == null ? throw new NotFoundException("User not found") : await _userManager.GetUserIdAsync(user);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user == null
                ? throw new NotFoundException("User not found")
                : await _userManager.GetUserNameAsync(user) ?? throw new NullReferenceException("UserName");
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new NotFoundException("User not found");
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user == null ? throw new NotFoundException("User not found") : await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;


        }

        public async Task<bool> UpdateUserProfile(string id, string? fullName, string? email, IList<string>? roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.FullName = fullName;
                user.Email = email;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> UpdateAdress(string id, string Number, string Street, string? Complement, string City, string ZipCode)
        {
            var user = await _userManager.Users.Include(u => u.Adress).FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("User not found");
            if (user != null)
            {
                if (user.Adress == null)
                {
                    user.Adress = new Adress
                    {
                        CreatedDate = DateTime.Now,
                        Number = Number,
                        Street = Street,
                        Complement = Complement,
                        City = City,
                        ZipCode = ZipCode
                    };
                }
                else
                {
                    user.Adress.Number = Number;
                    user.Adress.Street = Street;
                    user.Adress.Complement = Complement;
                    user.Adress.City = City;
                    user.Adress.ZipCode = ZipCode;
                }

                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdatePhoneNumber(string id, string phoneNumber)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.PhoneNumber = phoneNumber;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateProfilePictureCommand(string id, IFormFile file)
        {
            var user = await _userManager.Users.Include(u => u.ProfilePicture).FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("User not found");
            if (user != null)
            {
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                if (user.ProfilePicture == null)
                {
                    user.ProfilePicture = new ProfilePicture
                    {
                        CreatedDate = DateTime.Now,
                        Data = fileBytes,
                        Name = file.FileName,
                        Size = file.Length,
                        Type = file.ContentType
                    };
                }
                else
                {
                    user.ProfilePicture.Data = fileBytes;
                    user.ProfilePicture.Name = file.FileName;
                    user.ProfilePicture.Size = file.Length;
                    user.ProfilePicture.Type = file.ContentType;
                }

                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                throw new NotFoundException(nameof(role), id);

            return (role.Id, role.Name!);
        }

        public async Task<bool> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                    throw new NotFoundException(nameof(role), id);

                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName) ?? throw new NotFoundException($"{userName} is not registered.");
            var existingRoles = await _userManager.GetRolesAsync(user);
            _ = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            IdentityResult? result = await _userManager.AddToRolesAsync(user, usersRole);

            return result.Succeeded;
        }

        public async Task<bool> IsExistByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null ? false : true;
        }

        public async Task<bool> ChangePassword(string id, string token, string password, string confirmPassword)
        {
            if(password != confirmPassword)
                throw new BadHttpRequestException("Les mots de passes ne correspondent pas");

            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                var securityToken =token.Replace(" ", "+");
                var result = await _userManager.ResetPasswordAsync(user, securityToken, password);
                if(result.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new BadRequestException($"An error occurred " + result.Errors.FirstOrDefault()?.Description);
                }
            }

            return false;
        }

        public async Task<bool> ConfirmEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var securityToken = token.Replace(" ", "+");
                var result = await _userManager.ConfirmEmailAsync(user, securityToken);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new BadRequestException($"An error occurred " + result.Errors.FirstOrDefault()?.Description);
                }
            }

            return false;
        }
    }
}
