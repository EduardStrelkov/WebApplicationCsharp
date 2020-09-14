using ReAgent.Models.BO;
using ReAgent.Models.Enums;
using System;
using System.Collections.Generic;

namespace ReAgent.Services.API
{
    public interface IAccountServ
    {
        IEnumerable<UserBO> GetAllUsers();

        UserRole GetUserRole(Guid userId);

        UserBO Login(string email, string password, out string errorMsg);

        UserBO GetUserByEmail(string email);

        UserBO GetUserByStrId(string strUserId);

        UserBO GetUserById(Guid id);

        Guid Register(UserBO user);

        bool UserEdit(UserBO user, out string errorMsg);

        Guid? VerifyUser(Guid token);

        bool ConfirmRegistration(Guid userId);

        bool RemoveUser(Guid userId);
    }
}
