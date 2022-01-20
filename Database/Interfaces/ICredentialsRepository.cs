using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface ICredentialsRepository : IRepository<CredentialModel>
    {
        CredentialModel GetUserCredentialsIfValid(string userName, string password);

        CredentialModel GetUserCredentialsByUsername(string userName);

        bool UsernameExists(string userName);
    }
}
