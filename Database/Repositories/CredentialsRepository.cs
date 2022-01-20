using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Database.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class CredentialsRepository : BaseRepository<CredentialModel>, ICredentialsRepository
    {
        public CredentialsRepository(DatabaseContext context) : base(context)
        {
        }

        public CredentialModel GetUserCredentialsIfValid(string userName, string password)
        {
            return DbSet.Where(c => c.Username == userName && c.Password == password).SingleOrDefault();
        }

        public bool UsernameExists(string userName)
        {
            return DbSet.Any(c => c.Username == userName);
        }

        public CredentialModel GetUserCredentialsByUsername(string userName)
        {
            return DbSet.Where(c => c.Username == userName).SingleOrDefault();
        }
    }
}
