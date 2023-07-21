using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WomanShop.Areas.Admin.Models;

namespace WomanShop.Interfaces
{
    public interface IRolesStorage
    {
        public List<Role> GetAll();
        public void Add(Role role);
        public void Remove(Guid id);
        public bool IsInStorage(Role role);
        public Role TryGetById(Guid id);


        public Role TryGetByName(string Name);
      
    }
}
