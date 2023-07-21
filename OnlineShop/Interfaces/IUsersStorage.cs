using WomanShop.Areas.Admin.Models;
using WomanShop.Models;

namespace WomanShop.Interfaces
{
    public interface IUsersStorage
    {
        public void Add(User user);
        public User TryGetUserById(Guid id);

        public User TryGetUserByEmail(string email);

        public bool IsCorrectPassword(Login login);
        public void Remove(Guid userId);
        public void Update(User userId);
        public void UpdatePassword(User user, string password);
        public void UpdateRole(User user, Role role);

        public List<User> GetAll();
    }
}
