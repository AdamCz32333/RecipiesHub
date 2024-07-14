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
## Model
Za pośrednictwem formularza do bazy danych są wysyłane tytuł, opis, ścieżka do obrazka i nazwa autora
![image](https://github.com/user-attachments/assets/4651c9ae-b8e4-48f9-b0b7-50b2c6a0301a)


## Kontroler
RecipesController.cs został wygenerowany przez funkcję scaffolding (Kontroler MVC z widokami korzystającymi z programu Entity Framework)
Zostały zmodyfikowane wygenerowane metody public async Task<IActionResult> Edit oraz public async Task<IActionResult> Create aby pozwolić na przesyłanie bitmap do katalogu wwwroot/images

![image](https://github.com/user-attachments/assets/5c6b7241-213c-49fd-bdaf-f38018c622a6)

Dodany został też kod który usuwa z nazwy autora domenę mailową

![image](https://github.com/user-attachments/assets/dd7fa165-d449-4930-855a-a0a1e1dba352)

## Widoki
Widoki zostały wygenerowane przez funkcję scaffolding (Kontroler MVC z widokami korzystającymi z programu Entity Framework). Jest podział na strefę dla niezalogowanych użytkowników (Home/index.cshtml)

![image](https://github.com/user-attachments/assets/4bcd8701-0e6c-432d-97f6-f3aa28cd62d4)

I zalogowanych (Recipes/index.cshtml)
![image](https://github.com/user-attachments/assets/d411cd2e-e773-499b-8d4f-7caf24c2a8bc)

Po kliknięciu w opcję stwórz nowy przechodzimy do widoku create (Recipes/create.cshtml)
![image](https://github.com/user-attachments/assets/145b39b9-6eb8-477f-8517-148146c9b6a4)

Widok edit jest zarezerwowany dla administratora
![image](https://github.com/user-attachments/assets/b01f15da-55c1-455e-8ad5-ce2b846614bb)



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

![image](https://github.com/user-attachments/assets/0d8c1739-707b-4972-affd-d991b0dc4c6b)


