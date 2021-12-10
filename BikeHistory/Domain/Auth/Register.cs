using BikeHistory.Data;

namespace BikeHistory.Domain.Auth
{
    public static class Register
    {
        public async static Task RegisterUser(BikeContext db, User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

    }
}