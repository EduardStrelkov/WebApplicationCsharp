using ReAgent.App_Start;
using ReAgent.Models.BO;
using ReAgent.Models.DAO;
using ReAgent.Models.Enums;
using ReAgent.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReAgent.Services.Impl
{
    public class AccountServ : IAccountServ
    {
        public IEnumerable<UserBO> GetAllUsers()
        {
            using(ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IEnumerable<UserBO>>(db.tblUsers);
            }
        }

        public UserRole GetUserRole(Guid userId)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser user = db.tblUsers.FirstOrDefault(x => x.pk_id == userId);
                return user != null ? (UserRole)user.fk_role : UserRole.Client;
            }
        }

        public UserBO Login(string email, string password, out string errorMsg)
        {
            var hash = System.Web.Helpers.Crypto.SHA256(password);
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser user = db.tblUsers.FirstOrDefault(x => x.email == email && x.password_hash == hash);
                if(user != null)
                {
                    if (user.verification_token.HasValue)
                    {
                        errorMsg = "Email не верифицирован";
                        return null;
                    }

                    errorMsg = null;
                    return MappingProfilesConfig.Mapper.Map<UserBO>(user);
                }

                errorMsg = "Имя пользователя или пароль указаны неверно.";
                return null;
            }
        }
        
        public UserBO GetUserByEmail(string email)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<UserBO>(db.tblUsers.FirstOrDefault(x => x.email == email));
            }
        }

        public UserBO GetUserByStrId(string strUserId)
        {
            Guid.TryParse(strUserId, out Guid guid);
            return GetUserById(guid);
        }

        public UserBO GetUserById(Guid id)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<UserBO>(db.tblUsers.FirstOrDefault(x => x.pk_id == id));
            }
        }

        public Guid Register(UserBO user)
        {
            string passwordHash = System.Web.Helpers.Crypto.SHA256(user.Password);

            Guid verificationToken = Guid.NewGuid();
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                db.tblUsers.Add(new tblUser()
                {
                    pk_id = Guid.NewGuid(),
                    first_name = user.FirstName,
                    last_name = user.LastName,
                    password_hash = passwordHash,
                    email = user.Email,
                    address = user.Address,
                    phone = user.Phone,
                    verification_token = verificationToken,
                    fk_role = (int)UserRole.Client,
                    registration_date = DateTime.Now
                });

                db.SaveChanges();
            }

            return verificationToken;
        }

        public bool UserEdit(UserBO user, out string errorMsg)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser tblUser = db.tblUsers.FirstOrDefault(x => x.pk_id == user.Id);
                if(tblUser != null)
                {
                    if(tblUser.email != user.Email && db.tblUsers.Any(x => x.email == user.Email))
                    {
                        errorMsg = "Пользователь с таким email уже существует";
                        return false;
                    }

                    tblUser.email = user.Email;
                    tblUser.first_name = user.FirstName;
                    tblUser.last_name = user.LastName;
                    tblUser.address = user.Address;
                    tblUser.phone = user.Phone;

                    db.SaveChanges();

                    errorMsg = null;
                    return true;
                }

                errorMsg = "Пользователь не найден";
                return false;
            }            
        }

        public Guid? VerifyUser(Guid token)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser user = db.tblUsers.FirstOrDefault(x => x.verification_token == token);
                if (user != null)
                {
                    user.verification_token = null;
                    db.SaveChanges();

                    return user.pk_id;
                }

                return null;
            }
        }

        public bool ConfirmRegistration(Guid userId)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser user = db.tblUsers.FirstOrDefault(x => x.pk_id == userId);

                if(user != null)
                {
                    user.verification_token = null;
                    db.SaveChanges();

                    return true;
                }
            }

            return false;
        }
        
        public bool RemoveUser(Guid userId)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblUser user = db.tblUsers.FirstOrDefault(x => x.pk_id == userId);
                if(user != null)
                {
                    db.tblUsers.Remove(user);
                    db.SaveChanges();

                    return true;
                }
            }

            return false;
        }
    }
}