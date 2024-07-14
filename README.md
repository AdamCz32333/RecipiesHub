# Recipes Hub
Aplikacja do dodawania nowych receptur i obrazków za pośrednictwem kont użytkownika.


# Stos technologiczny
- .NET 6
- ASP.NET Core Identity
- Entity Framework
- Microsoft SQL Server
# Instalacja
Wypakować repozytorium do folderu i uruchomić plik .sln za pomocą Visual Studio 2022
Po otwarciu projektu uruchomić konsolę menedżerów pakietów i uruchomić komendę
```
update-database
```

# Struktura
Kontroler RecipesController został wygenerowany za pomocą scaffolding EF, zostały zmodyfikowane metody create i edit aby wspierać wysyłanie bitmap do katalogu wwwroot/images
```
if (Image != null && Image.Length > 0)
{
    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
    if (!Directory.Exists(uploads))
    {
        Directory.CreateDirectory(uploads);
    }

    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
    var filePath = Path.Combine(uploads, uniqueFileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await Image.CopyToAsync(stream);
    }

    recipe.ImageUrl = "/images/" + uniqueFileName;
}
```
# Administracja
Kod roli administratora i dane logowania do konta admina znajdują się w pliku program.cs
```
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string email = "admin@recipieshub.com";
            string password = "Aadmin5555!";

            if(await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser();
                user.Email = email;
                user.UserName = email;
                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user,"Admin");
            }


        }
```
Admin jest w stanie edytować i usuwać rekordy z listy receptur

