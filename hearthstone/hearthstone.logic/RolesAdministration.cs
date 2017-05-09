using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hearthstone.data;
using System.Diagnostics;
using log4net;

namespace hearthstone.logic
{
    public class RolesAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Returns a user role for a given username
        /// </summary>
        /// <param name="username">a non-empty username</param>
        /// <returns>user role of given username</returns>
        /// <exception cref="Exception">in case of a database error</exception>
        /// <exception cref="ArgumentNullException">if username is null or empty</exception>
        /// <exception cref="ArgumentException">if username is unknown</exception>
        public static UserRole GetUserRole(string username)
        {
            log.Info("RolesAdministration - GetUserRoles(username)");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            UserRole userRole = null;

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User currentUser = context.AllUsers.FirstOrDefault(x => x.Username.Equals(username));
                    if (currentUser != null)
                    {
                        userRole = currentUser.UserRole;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid username {username}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RolesAdministration - GetUserRoles(username) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("UserAdministration - GetUserRoles(username) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }
            return userRole;
        }
    }
}
