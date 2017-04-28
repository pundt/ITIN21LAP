using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hearthstone.data;
using System.Diagnostics;
using log4net;

namespace hearthstone.logic
{
    public enum RegisterResult
    {
        NotSet,
        Successful,
        UsernameExists
    }
    public class UserAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Registers a new user in data storage
        /// returns true if successful, otherelse false
        /// throws exception on invalid data
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">invalid data within parameters</exception>
        public static RegisterResult Register(string firstName, string lastName, string email, string username, string password, byte[] avatar)
        {
            log.Info("UserAdministration - Register");

            RegisterResult result = RegisterResult.NotSet;

            if (!string.IsNullOrEmpty(firstName))
                throw new ArgumentException($"{nameof(firstName)} is null or empty");
            if (!string.IsNullOrEmpty(lastName))
                throw new ArgumentException($"{nameof(lastName)} is null or empty");
            if (!string.IsNullOrEmpty(email))
                throw new ArgumentException($"{nameof(email)} is null or empty");
            if (!string.IsNullOrEmpty(username))
                throw new ArgumentException($"{nameof(username)} is null or empty");
            if (!string.IsNullOrEmpty(password))
                throw new ArgumentException($"{nameof(password)} is null or empty");

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers.FirstOrDefault(x => x.Username.Equals(username));

                    if (user != null)
                    {
                        result = RegisterResult.UsernameExists;
                        log.Debug($"Username {username} exists");
                    }
                    else
                    {
                        user = new User()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Mail = email,
                            Username = username,
                            Password = "WTF",
                            Avatar = avatar
                        };

                        context.AllUsers.Add(user);
                        context.SaveChanges();
                        result = RegisterResult.Successful;
                        log.Debug($"Username {username} registered!");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("UserAdministration - Register - Exception", ex);
                if (ex.InnerException!=null)
                    log.Error("UserAdministration - Register - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex; 
            }

            return result;
        }
    }
}
