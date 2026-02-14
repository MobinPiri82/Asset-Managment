using Microsoft.Identity.Client;
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
            Console.WriteLine("1 for add person,2 for add asset");
            int select = int.Parse(Console.ReadLine());
            if (select == 1)
            {
                AddPerson();
            }
            if (select == 2)
            {
                AddAsset();
            }

            void AddPerson()
            {
                Console.WriteLine("Enter Name ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Father" +
                    "Name ");
                string fatherName = Console.ReadLine();
                Console.WriteLine("nationalId");
                int nationalId = int.Parse(Console.ReadLine());
                if (name == null || fatherName == null || nationalId == null)
                { 
                    Console.WriteLine("Values can not be null");
                    return;
                }
                else
                {
                    using var Per = new Context();
                    var person = new Person
                    {
                        Name = name,
                        FatherName = fatherName,
                        NationalID = nationalId
                    };
                    Per.Add(person);
                    Per.SaveChanges();
                    Console.WriteLine("Person Added");
                }
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
                long value = long.Parse(Console.ReadLine());
                Console.WriteLine("Enter Asset Name Owner");
                int ownerId = int.Parse(Console.ReadLine());
                if (name == null || value == null || ownerId == null)
                {
                    Console.WriteLine("Values can not be null");
                    return;
                }

                using var As = new Context();
                var asset = new Asset
                {
                    Name = name,
                    Value = value,
                    OwnerId = ownerId,


                };
                As.Add(asset);
                As.SaveChanges();
                Console.WriteLine("Asset Added");
            }
            

        }
    }
}