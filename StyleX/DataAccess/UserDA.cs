using Microsoft.EntityFrameworkCore;
using StyleX.Models;

namespace StyleX.DataAccess
{
    public class UserDA
    {
        private readonly DatabaseContext _dbContext;

        public UserDA(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool SignUp(User user)
        {
            try
            {
                _dbContext.Add(user);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
        }


    }
}
