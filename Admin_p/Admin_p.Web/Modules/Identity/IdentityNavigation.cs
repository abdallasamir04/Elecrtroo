using Serenity.Navigation;
using MyPages = Admin_p.Identity.Pages;

[assembly: NavigationLink(int.MaxValue, "Identity/Asp Net User", typeof(MyPages.AspNetUserPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Identity/Asp Net User Roles", typeof(MyPages.AspNetUserRolesPage), icon: null)]
[assembly: NavigationLink(int.MaxValue, "Identity/Asp Net Users", typeof(MyPages.AspNetUsersPage), icon: null)]