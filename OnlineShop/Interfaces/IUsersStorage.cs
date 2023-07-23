using WomanShop.Areas.Admin.Models;
using WomanShop.Models;

namespace WomanShop.Interfaces
{
    public interface IUsersStorage
    {
        public void Add(UserViewModel user);
        public UserViewModel TryGetUserById(Guid id);

        public UserViewModel TryGetUserByEmail(string email);

        public bool IsCorrectPassword(Login login);
        public void Remove(Guid userId);
        public void Update(UserViewModel userId);
        public void UpdatePassword(UserViewModel user, string password);
        public void UpdateRole(UserViewModel user, Role role);

        public List<UserViewModel> GetAll();
    }
}
