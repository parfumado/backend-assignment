using System;
using System.Net.Mail;
using System.Threading.Tasks;
using DataAdapters.KeyValueDb;
using Microsoft.Extensions.Configuration;
using Utility;
using docDb = DataAdapters.DocumentDb;
using kvDb = DataAdapters.KeyValueDb;

namespace AuthService {
    public class UserService {
        private docDb.IDocumentAdapter _documentAdapter;
        private kvDb.IKeyValueAdapter _keyValueAdapter;
        private IConfiguration _config;

        public UserService(IConfiguration configuration, docDb.IDocumentAdapter documentAdapter, kvDb.IKeyValueAdapter keyValueAdapter) {
            this._config = configuration;
            this._documentAdapter = documentAdapter;
            this._keyValueAdapter = keyValueAdapter;
        }

        //TODO: Reset user password. You can change this code to return the reset URL instead, to simplify testing. Or would you do it some other way?
        public async Task<bool> ResetUserPassword(string email, string domain, string authUrl, string? fallbackCulture) {
            throw new NotImplementedException();
        }

        public async Task<UserSession?> AuthenticateUser(UserRequest credentials, string pepper) {
            User? user = await this.GetUserByEmail(credentials.username);

            if (user is null) return null;

            if (StringHasher.VerifyPassword(user.PasswordHash ?? String.Empty, credentials.password!, pepper, user.Salt ?? string.Empty)) {
                return GenerateUserSession(user);
            } else {
                return null;
            }
        }

        public async Task<UserSession?> AuthenticateUser(string token) {
            string? email = await this._keyValueAdapter.GetReader(DictionaryIndex.ResetTokens).GetKey<string>(token);
            _ = this._keyValueAdapter.GetWriter(DictionaryIndex.ResetTokens).DeleteKey(token);

            User? user = await this.GetUserByEmail(email);

            if (user is null) return null;

            return GenerateUserSession(user);
        }

        //TODO: Update user service
        public async Task<bool> UpdateUser(User user, string? sessionId, string pepper) {
            throw new NotImplementedException();
        }

        //TODO: Get user by email
        public async Task<User?> GetUserByEmail(string? email) {
            throw new NotImplementedException();
        }

        private UserSession GenerateUserSession(User user) {
            kvDb.IWriter kvDbWriter = this._keyValueAdapter.GetWriter(DictionaryIndex.Sessions);

            string token = StringHasher.GenerateToken(user.Salt + Guid.NewGuid());
            UserSession session = new UserSession {
                SessionToken = token,
                ExpiresIn = TimeSpan.FromDays(3),
                User = user,
                CsrFToken = StringHasher.GenerateToken(token, 4096)
            };

            _ = kvDbWriter.SetKey<UserSession>(token, session);

            return session;
        }

        public async Task<(double ReturnCode, UserSession? UserSession, string Message)>
                            RegisterUser(User user, string domain, string? fallBackLanguageCode) {

            docDb.IWriter docDbWriter = this._documentAdapter.GetWriter();
            docDb.IReader docDbReader = this._documentAdapter.GetReader();

            try {
                _ = new MailAddress(user.Email!).Address;
            } catch (FormatException) {
                return (422.1, null, "Invalid e-mail address");
            }

            if (await this.GetUserByEmail(user.Email) is not null) {
                return (422.2, null, "Email address already registered.");
            }

            if (!await docDbWriter.InsertDocument<User>(docDb.Collections.User, user)) {
                return (500, null, "Error saving user");
            }

            UserSession session = GenerateUserSession(user);

            return (200, session, "User Registered");
        }

        public async Task<(int, UserSession?)> FetchUserSession(string token) {
            UserSession? userSession = await this._keyValueAdapter.GetReader(DictionaryIndex.Sessions).GetKey<UserSession>(token);
            if (userSession is null) {
                return (401, null);
            }

            return (200, userSession);
        }
    }
}