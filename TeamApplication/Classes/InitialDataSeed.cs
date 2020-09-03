using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamApplication.Data;
using TeamApplication.Models;
using System;
using System.Linq;

namespace TeamApplication.Classes
{
    public static class InitialDataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SementesApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<SementesApplicationContext>>()))
            {
                // Look for any States.
                if (context.State.Any())
                {
                    return;   // DB has been seeded
                }
                else
                {
                    context.State.AddRange(
                        new State
                        {
                            UFAbreviation = "AC",
                            UFName = "Acre"
                        },
                        new State
                        {
                            UFAbreviation = "AL",
                            UFName = "Alagoas"
                        },
                        new State
                        {
                            UFAbreviation = "AP",
                            UFName = "Amapá"
                        },
                        new State
                        {
                            UFAbreviation = "AM",
                            UFName = "Amazonas"
                        },
                        new State
                        {
                            UFAbreviation = "BA",
                            UFName = "Bahia"
                        },
                        new State
                        {
                            UFAbreviation = "CE",
                            UFName = "Ceará"
                        },
                        new State
                        {
                            UFAbreviation = "DF",
                            UFName = "Distrito Federal"
                        },
                        new State
                        {
                            UFAbreviation = "ES",
                            UFName = "Espírito Santo"
                        },
                        new State
                        {
                            UFAbreviation = "GO",
                            UFName = "Goiás"
                        },
                        new State
                        {
                            UFAbreviation = "MA",
                            UFName = "Maranhão"
                        },
                        new State
                        {
                            UFAbreviation = "MT",
                            UFName = "Mato Grosso"
                        },
                        new State
                        {
                            UFAbreviation = "MS",
                            UFName = "Mato Grosso do Sul"
                        },
                        new State
                        {
                            UFAbreviation = "MG",
                            UFName = "Minas Gerais"
                        },
                        new State
                        {
                            UFAbreviation = "PA",
                            UFName = "Pará"
                        },
                        new State
                        {
                            UFAbreviation = "PB",
                            UFName = "Paraíba"
                        },
                        new State
                        {
                            UFAbreviation = "PR",
                            UFName = "Paraná"
                        },
                        new State
                        {
                            UFAbreviation = "PE",
                            UFName = "Pernambuco"
                        },
                        new State
                        {
                            UFAbreviation = "PI",
                            UFName = "Piauí"
                        },
                        new State
                        {
                            UFAbreviation = "RJ",
                            UFName = "Rio de Janeiro"
                        },
                        new State
                        {
                            UFAbreviation = "RN",
                            UFName = "Rio Grande do Norte"
                        },
                        new State
                        {
                            UFAbreviation = "RS",
                            UFName = "Rio Grande do Sul"
                        },
                        new State
                        {
                            UFAbreviation = "RO",
                            UFName = "Rondônia"
                        },
                        new State
                        {
                            UFAbreviation = "RR",
                            UFName = "Roraima"
                        },
                        new State
                        {
                            UFAbreviation = "SC",
                            UFName = "Santa Catarina"
                        },
                        new State
                        {
                            UFAbreviation = "SP",
                            UFName = "São Paulo"
                        },
                        new State
                        {
                            UFAbreviation = "SE",
                            UFName = "Sergipe"
                        },
                        new State
                        {
                            UFAbreviation = "TO",
                            UFName = "Tocantins"
                        });
                        context.SaveChanges();
                        
                }
                // Look for any States.
                if (context.City.Any())
                {
                    return;   // DB has been seeded
                }
                else
                {
                    context.City.AddRange(
                        new City
                        {
                            CityName = "São José do Rio Preto",
                            StateId = 2
                        },
                        new City
                        {
                            CityName = "Mirassol",
                            StateId = 2
                        },
                        new City
                        {
                            CityName = "Bady Bassit",
                            StateId = 2
                        },
                        new City
                        {
                            CityName = "José Bonifácio",
                            StateId = 2
                        },
                        new City
                        {
                            CityName = "Ubarana",
                            StateId = 2
                        }
                        );
                    context.SaveChanges();
                }

            

            }
        }
    }
}
