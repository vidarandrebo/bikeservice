using System;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class User : IdentityUser<Guid>
{
}