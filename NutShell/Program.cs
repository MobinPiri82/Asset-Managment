using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NutShell;
using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Transactions;
namespace nutshell
{
    class Pragram
    {

        static void Main(string[] args)
        {
            Console.WriteLine("1 for add person,2 for add asset, 3 for get owners and their assets");
            int select = int.Parse(Console.ReadLine());
            switch (select)
            {
                case 1:
                    AddPerson();
                    break;
                case 2:
                    AddAsset();
                    break;
                case 3:
                    GetAll();
                    break;
                default:

                case 4:
                    //DeleteAsset();
                    break;
                    Console.WriteLine("Unvalid");
                    break;
            }
            void GetAll()
            {
                using (var context = new Context())
                {
                    var persons = context.Persons.Include(p => p.Assets).ToList();
                    foreach (var person in persons)
                    {
                        Console.Write($"{person.Name} has These assets ");
                        foreach (var asset in person.Assets)
                        {
                            Console.WriteLine(asset.Name);
                        }
                    }
                }

            }
            void AddPerson()
            {
                Console.WriteLine("Enter Name ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Father" +
                    "Name ");
                string fatherName = Console.ReadLine();
                Console.WriteLine("nationalId");
                string nationalId = Console.ReadLine();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(fatherName) || string.IsNullOrEmpty(nationalId))
                {
                    Console.WriteLine("Values can not be null");
                    return;

                }


                int intnationalId = int.Parse(nationalId);
                using var Per = new Context();
                var person = new Person
                {
                    Name = name,
                    FatherName = fatherName,
                    NationalID = intnationalId

                };
                Per.Add(person);
                Per.SaveChanges();
                Console.WriteLine("Person Added");


            }
            void AddAsset()
            {
                using var context = new Context();

                var persons = context.Persons.ToList();

                foreach (var p in persons)
                {
                    Console.WriteLine(
                        $"ID: {p.Id} | Name: {p.Name} | FatherName: {p.FatherName} | NationalID: {p.NationalID}"
                    );
                }

                Console.WriteLine("Enter Name Asset");
                string name = Console.ReadLine();

                Console.WriteLine("Value");
                string value = Console.ReadLine();
                Console.WriteLine("Enter Asset Name Owner");
                string ownerId = Console.ReadLine();
                if (string.IsNullOrEmpty(name)|| string.IsNullOrEmpty(value) || string.IsNullOrEmpty(ownerId))
                {
                    Console.WriteLine("Values can not be null");
                    return;
                }


                using var As = new Context();
                var asset = new Asset
                {
                    Name = name,
                    Value = long.Parse(value),
                    OwnerId = int.Parse(ownerId)


                };
                As.Add(asset);
                As.SaveChanges();
                Console.WriteLine("Asset Added");
                
                

            }
            

        }
    }
}