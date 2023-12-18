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

        public async Task<bool> Add(User user)
        {
            try
            {
                await _dbContext.AddAsync(user);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
