using PAWG1.Service.Services;
using PAWG1.Models.EFModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;
using System.Security.Cryptography;
using PAWG1.Architecture.Helpers;

namespace PAWG1.Validator.Validators;

/// <summary>
/// Interface for validating user data during creation and editing operations.
/// </summary>
public interface IValidatorUser
{
    /// <summary>
    /// Validates the user data for creating a new user.
    /// </summary>
    /// <param name="user">The user data to be validated.</param>
    /// <param name="tempData">The TempData dictionary for storing validation error messages.</param>
    /// <returns>A task that represents the result of the validation.</returns>
    Task<bool?> ValidatorCreate(User user, ITempDataDictionary tempData);

    /// <summary>
    /// Validates the user data for editing an existing user.
    /// </summary>
    /// <param name="id">The ID of the user being edited.</param>
    /// <param name="user">The user data to be validated.</param>
    /// <param name="tempData">The TempData dictionary for storing validation error messages.</param>
    /// <returns>A task that represents the result of the validation.</returns>
    Task<bool?> ValidatorEdit(int id, User user, ITempDataDictionary tempData);
}

/// <summary>
/// A class that implements user validation logic for creating and editing user data.
/// </summary>
public class ValidatorUser : IValidatorUser
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    /// <summary>
    /// Initializes a new instance of the ValidatorUser class.
    /// </summary>
    /// <param name="userService">The service for user-related operations.</param>
    public ValidatorUser(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Validates user data for creating a new user.
    /// </summary>
    /// <param name="user">The user data to be validated.</param>
    /// <param name="tempData">The TempData dictionary for storing validation error messages.</param>
    /// <returns>A task representing the result of the validation, true if valid, false if invalid.</returns>
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
            user.Password = UserHelper.HashPassword(user.Password);

        }
        if (user.IdRole == 0)
        {
            tempData["ErrorRole"] = $"The role cannot be empty";
            return false;
        }

        return true;
    }

    /// <summary>
    /// Validates user data for editing an existing user.
    /// </summary>
    /// <param name="id">The ID of the user being edited.</param>
    /// <param name="user">The user data to be validated.</param>
    /// <param name="tempData">The TempData dictionary for storing validation error messages.</param>
    /// <returns>A task representing the result of the validation, true if valid, false if invalid.</returns>
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
            user.Password = UserHelper.HashPassword(user.Password);
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
}

