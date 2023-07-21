using WomanShop.Areas.Admin.Models;
using WomanShop.Interfaces;

namespace WomanShop.Storages
{
    public class InMemoryRolesStorage : IRolesStorage
    {
        private List<Role> roles = new List<Role>()
        {
            new Role("Пользователь"),
            new Role("Админ")
        };

        public List<Role> GetAll()
        {
            return roles;
        }
        public void Add(Role role)
        {
            roles.Add(role);
        }
        public bool IsInStorage(Role role)
        {
            return roles.Any(roleInStorage=>roleInStorage.Name==role.Name);
        }
        public void Remove(Guid roleId)
        { 
            roles.RemoveAll(role=>role.Id==roleId);
        }
        
        public Role TryGetById(Guid id)
        {
            return roles.FirstOrDefault(role => role.Id == id);
        }

        public Role TryGetByName(string Name)
        {
            return roles.FirstOrDefault(role => role.Name == Name);
        }

    }
}
