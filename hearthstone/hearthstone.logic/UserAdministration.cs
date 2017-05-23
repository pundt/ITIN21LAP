using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// we need this for include with lambda expressions!!
using System.Data.Entity;

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

    public enum LoginResult
    {
        NotSet,
        Successful,
        InvalidPassword,
        InvalidUsername
    }
    public class UserAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Registers a new user in data storage
        /// adds three static user decks with randomized deck names
        /// returns true if successful, otherelse false
        /// throws exception on invalid data
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="gamerTag"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">invalid data within parameters</exception>
        /// <exception cref="Exception">in case of a database error</exception>
        public static RegisterResult Register(string firstName, string lastName, string gamerTag, string username, string password)
        {
            log.Info("UserAdministration - Register");

            RegisterResult result = RegisterResult.NotSet;

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException($"{nameof(firstName)} is null or empty");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException($"{nameof(lastName)} is null or empty");
            if (string.IsNullOrEmpty(gamerTag))
                throw new ArgumentException($"{nameof(gamerTag)} is null or empty");
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException($"{nameof(username)} is null or empty");
            if (string.IsNullOrEmpty(password))
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
                        string userSalt = Tools.GenerateSalt(128);
                        user = new User()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            GamerTag = gamerTag,
                            Username = username, // = email
                            Password = Tools.GetHash(password + userSalt),
                            UserSalt = userSalt,
                            ID_UserRole = UserRole.PLAYER
                        };

                        for (int i = 0; i < User.MAX_DECK; i++)
                        {
                            Deck userDeck = new Deck()
                            {
                                Name = $"{gamerTag}'s {Deck.GetRandomizedDeckName}"
                            };
                            user.AllDecks.Add(userDeck);
                        }

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
                if (ex.InnerException != null)
                    log.Error("UserAdministration - Register - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Returns login status for given credentials
        /// </summary>
        /// <param name="username">username to login</param>
        /// <param name="password">password to login</param>
        /// <exception cref="ArgumentException">in case any parameter is null or empty</exception>
        /// <exception cref="Exception">in case of a database error</exception>
        /// <returns>login status for given credentials</returns>
        public static LoginResult Login(string username, string password)
        {
            log.Info("UserAdministration - Login");

            LoginResult result = LoginResult.NotSet;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException($"{nameof(username)} is null or empty");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException($"{nameof(password)} is null or empty");

            try
            {
                using (var context = new clonestoneEntities())
                {
                    User user = context.AllUsers.FirstOrDefault(x => x.Username.Equals(username));

                    if (user != null)
                    {
                        byte[] hashedPassword = Tools.GetHash(password + user.UserSalt);
                        if (user.Password.SequenceEqual(hashedPassword))
                        {
                            result = LoginResult.Successful;
                            log.Debug($"User {username} logged in successfully");
                        }
                        else
                        {
                            result = LoginResult.InvalidPassword;
                            log.Warn($"User {username} entered invalid password");
                        }
                    }
                    else
                    {
                        result = LoginResult.InvalidUsername;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("UserAdministration - Login - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("UserAdministration - Login - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return result;
        }


        /// <summary>
        /// returns a user object for a given username
        /// </summary>
        /// <param name="username">a non-empty, existing username</param>
        /// <returns>user object for given username</returns>
        /// <exception cref="Exception">in case of a common db error</exception>
        /// <exception cref="ArgumentNullException">username is null or empty</exception>
        /// <exception cref="ArgumentException">invalid username</exception>
        public static User GetUser(string username)
        {
            log.Info("UserAdministration - GetUser(username)");
            User currentUser = null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            
            try
            {
                /// open connection and keep it open for as long as needed
                using (var context = new clonestoneEntities())
                {
                    currentUser = context.AllUsers
                        
                        /// therefore we should/could/may (as userstory indicates!)
                        /// eager load any navigation properties
                        .Include(x => x.UserRole)


                        .FirstOrDefault(x => x.Username.Equals(username));
                    if (currentUser == null)
                        throw new ArgumentException($"Invalid username {username}");

                } /// connection will be close

                /// so at this point LAZY LOADING is NOT available anymore!!
            }
            catch (Exception ex)
            {
                log.Error("UserAdministration - GetUser(username) - Exception", ex);
                if (ex.InnerException != null)
                    log.Error("UserAdministration - GetUser(username) - Exception (inner)", ex.InnerException);

                Debugger.Break();
                throw ex;
            }

            return currentUser;
        }
    }
}
