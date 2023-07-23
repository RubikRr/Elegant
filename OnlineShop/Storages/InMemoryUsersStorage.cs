using WomanShop.Areas.Admin.Models;
using WomanShop.Interfaces;
using WomanShop.Models;

namespace WomanShop.Storages
{
    public class InMemoryUsersStorage:IUsersStorage
    {
        //public List<UserViewModel> users=new List<UserViewModel>() 
        //{ 
        //    //new UserViewModel("vova.chakalov.2003@yandex.ru","1"),
        //    //new UserViewModel("alina.mamsurova.2004.ru","2")
        //};

        //public void Add(UserViewModel user)
        //{
        //    users.Add(user);
        //}
        //public List<UserViewModel> GetAll()
        //{
        //    return users;
        //}
        //public UserViewModel TryGetUserById(Guid id)
        //{
        //    return users.FirstOrDefault(user => user.Id == id);
        //}
        //public UserViewModel TryGetUserByEmail(string email)
        //{
        //    return users?.FirstOrDefault(user => user.Email == email)??null;
        //}

        //public bool IsCorrectPassword(Login login)
        //{
        //    var user=TryGetUserByEmail(login.Email);
        //    return user != null && user.Password == login.Password;
        //}

        //public void UpdateRole(UserViewModel user, Role newrole)
        //{
        //    user.RoleName= newrole.Name;
        //}
        //public void Update(UserViewModel user)
        //{
        //    var existingUser=TryGetUserById(user.Id);
        //    existingUser.Password = user.Password;
        //    existingUser.Email = user.Email;
        //    existingUser.FirstName = user.FirstName;
        //    existingUser.LastName = user.LastName;
        //    existingUser.Phone= user.Phone;
        //    existingUser.RoleName= user.RoleName;
        //}
        //public void UpdatePassword(UserViewModel user, string password)
        //{
        //    user.Password = password;
        //}
        //public void Remove(Guid id) 
        //{
        //    users.RemoveAll(user => user.Id == id);
        //}
    }
}
