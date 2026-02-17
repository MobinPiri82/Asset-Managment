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
            while (true)
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

                Console.WriteLine("Enter Asset Name");
                string name = Console.ReadLine();

                Console.WriteLine("Value");
                string valueInput = Console.ReadLine();

                Console.WriteLine("Enter Asset Owner ID");
                string ownerIdInput = Console.ReadLine();

                if (string.IsNullOrEmpty(name) ||string.IsNullOrEmpty(valueInput) ||string.IsNullOrEmpty(ownerIdInput))
                {
                    Console.WriteLine("Values can not be null");
                    return;
                }

                if (!long.TryParse(valueInput, out long value))
                {
                    Console.WriteLine("Invalid value");
                    return;
                }
                if (int.TryParse(valueInput,out int values))
                    { }
                if (!int.TryParse(ownerIdInput, out int ownerId))
                {
                    Console.WriteLine("Invalid owner ID");
                    return;
                }

                var ownerExist = context.Persons.Any(p => p.Id == ownerId);
                if (!ownerExist)
                {
                    Console.WriteLine("Person does not exist");
                    return;
                }

                var asset = new Asset
                {
                    Name = name,
                    Value = value,
                    OwnerId = ownerId
                };

                context.Assets.Add(asset);
                context.SaveChanges();

                Console.WriteLine("Asset Added");
            }





            //void AddAsset()
            //{
            //    using var context = new Context();

            //    var persons = context.Persons.ToList();

            //    foreach (var p in persons)
            //    {
            //        Console.WriteLine(
            //            $"ID: {p.Id} | Name: {p.Name} | FatherName: {p.FatherName} | NationalID: {p.NationalID}"
            //        );
            //    }

            //    Console.WriteLine("Enter Name Asset");
            //    string name = Console.ReadLine();

            //    Console.WriteLine("Value");
            //    string value = Console.ReadLine();
            //    Console.WriteLine("Enter Asset Owner  ID");
            //    string ownerId = Console.ReadLine();
            //    if (string.IsNullOrEmpty(name)|| string.IsNullOrEmpty(value) || string.IsNullOrEmpty(ownerId))
            //    {
            //        Console.WriteLine("Values can not be null");
            //        return;
            //    }

            //    using var As = new Context();
            //    var ownerExist = context.Persons.Any(p => p.Id == int.Parse(ownerId));
            //    if(!ownerExist)
            //        {
            //            Console.WriteLine("Person does not exist");
            //        }
            //    var asset = new Asset
            //    {
            //        Name = name,
            //        Value = long.Parse(value),
            //        OwnerId = int.Parse(ownerId),


            //    };
            //    As.Add(asset);
            //    As.SaveChanges();
            //    Console.WriteLine("Asset Added");



            //}


        }
        
    }
}