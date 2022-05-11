using Ecommerce01.Clases;
using Ecommerce01.Data.Entities;
using Ecommerce01.Helpers;

namespace Ecommerce01.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            this._userHelper = userHelper;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsyng();
            await CheckUserAsync("1010", "Jorge", "Ibrahim", "jorgeibrahim88@yopmail.com", "3815861510", "bulnes 1020", UserType.Admin);
            await CheckUserAsync("2020", "Juan", "Perez", "juanchi8@yopmail.com", "3814985632", "Av. Roca 658", UserType.User);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;

        }

        private async Task CheckRolesAsyng()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                           Name = "Tucuman",
                           Cities = new List<City>()
                           {
                               new City{Name = "La Banda"},
                               new City{Name = "San Miguel de tucuman"},
                               new City{Name = "Concepcion"},
                           }
                        },

                        new State
                        {
                           Name = "Santiago del Estero",
                           Cities = new List<City>()
                           {
                               new City{Name = "La Banda"},
                           }
                        },

                        new State
                        {
                           Name = "Salta",
                           Cities = new List<City>()
                           {
                               new City{Name = "Alemania"},
                               new City{Name = "Salta Capital"},
                           }
                        }
                    }

                });

                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>() {
                                new City() { Name = "Medellín" },
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>() {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" },
                            }
                        },



                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>() {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                    }
                });


            }//fin

            await _context.SaveChangesAsync();

        }
        private async Task CheckCategoriesAsync()
        {
            if(!_context.Categories.Any())
            {
                _context.Categories.Add(new Category
                {
                    Name = "Electrodomesticos"
                });
                _context.Categories.Add(new Category
                {
                    Name = "Tecnologia"
                });
                _context.Categories.Add(new Category
                {
                    Name = "Gaming"
                });
                _context.Categories.Add(new Category
                {
                    Name = "Bebidas"
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
