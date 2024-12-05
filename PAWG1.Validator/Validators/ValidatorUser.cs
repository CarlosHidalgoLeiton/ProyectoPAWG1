using PAWG1.Service.Services;
using PAWG1.Models.EFModels;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using PAWG1.Architecture.Providers;
using System.Text;
using System.Security.Cryptography;


namespace PAWG1.Validator.Validators;

public interface IValidatorUser
{
    Task<bool?> ValidatorCreate(User user, ITempDataDictionary tempData);
    Task<bool?> ValidatorEdit(int id, User user, ITempDataDictionary tempData);
}

public class ValidatorUser : IValidatorUser
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public ValidatorUser(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool?> ValidatorCreate(User user, ITempDataDictionary tempData)
    {
        tempData.Remove("ErrorUsername");
        tempData.Remove("ErrorEmail");
        tempData.Remove("ErrorPassword");


        if (string.IsNullOrEmpty(user.Username))
        {
            tempData["ErrorUsername"] = "The username cannot be empty.";
            return false;
        }

        var data = await _userService.GetAllUsersAsync();

        if (data.Any(existingUser => existingUser.Username == user.Username))
        {
            tempData["ErrorUsername"] = $"The username '{user.Username}' already exists.";

            return false;
        }

        if (user.Email == null)
        {
            tempData["ErrorEmail"] = $"Email cannot be empty";
            return false;
        }

        if (user.Password == null)
        {
            tempData["ErrorPassword"] = $"The password cannot be empty.";
            return false;
        }
        if (user.Password.Length < 8 || user.Password.Length > 16)
        {
            tempData["ErrorPassword"] = $"The password must be between 8 and 16 characters.";

            return false;
        }
        else
        {
            user.Password = HashPassword(user.Password);

        }
        if (user.IdRole == 0)
        {
            tempData["ErrorRole"] = $"The role cannot be empty";
            return false;
        }

        return true;
    }



    public async Task<bool?> ValidatorEdit(int id, User user, ITempDataDictionary tempData)
    {


        if (string.IsNullOrEmpty(user.Username))
        {
            tempData["ErrorUsername"] = "El nombre de usuario no puede estar vacío.";
            return false;
        }


        var data = await _userService.GetUserAsync(id);
        if (data.Username != user.Username)
        {
            var datas = await _userService.GetAllUsersAsync();

            if (datas.Any(existingUser => existingUser.Username == user.Username))
            {
                tempData["ErrorUsername"] = $"The username '{user.Username}' already exists.";

                return false;
            }
        }
        if (user.Password == null)
        {
            var dataPassword = await _userService.GetUserAsync(id);
            user.Password = dataPassword.Password;
            tempData["Password"] = $"Password";
        }
        else
        {
            if (user.Password.Length < 8 || user.Password.Length > 16)
            {
                tempData["ErrorPassword"] = $"The password must be between 8 and 16 characters.";
                return false;
            }
            user.Password = HashPassword(user.Password);
        }
        if (user.Email == null)
        {
            tempData["ErrorEmail"] = $"Email cannot be empty";
            return false;
        }
        if (user.IdRole == 0)
        {
            tempData["ErrorRole"] = $"The role cannot be empty";
            return false;
        }

        return true;

    }




    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

