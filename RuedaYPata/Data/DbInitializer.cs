using Microsoft.AspNetCore.Identity;
using RuedaYPata.Models;

namespace RuedaYPata.Data
{
    public static class DbInitializer
    {
         public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Crear roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Cliente"))
                await roleManager.CreateAsync(new IdentityRole("Cliente"));

            // Crear usuario admin por defecto
            if (await userManager.FindByEmailAsync("admin@ruedaypata.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@ruedaypata.com",
                    Email = "admin@ruedaypata.com",
                    NombreCompleto = "Administrador General",
                    Direccion = "Oficina Central",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}