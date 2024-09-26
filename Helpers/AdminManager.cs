
using admin_panel2.Models;

namespace admin_panel2.Helpers
{
    public class AdminManager
    {
        private static Admin admin = new Admin();
        public bool Login(string email, string password)
        {
            return admin.Email == email.ToLower().Trim() && admin.Password == password;
        }
    }
}
