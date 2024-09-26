
namespace admin_panel2.Models
{
    internal class Admin
    {
        public int Id { get; set; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string Password { get; }

        public Admin()
        {
            Name = "Charles";
            Surname = "Harrison";
            Email = "charles.harri@gmail.com";
            Password = "Harri_123";
        }
    }
}
