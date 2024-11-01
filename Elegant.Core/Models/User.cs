using Microsoft.AspNetCore.Identity;

namespace Elegant.Core.Models;

public class User : IdentityUser, IEntity<string>;