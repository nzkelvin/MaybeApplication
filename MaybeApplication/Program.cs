using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaybeApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DemoTheOldWayOfNullChecking());
            Console.WriteLine(DemoTheNewWayOfNullChecking());
            // Null exception
            //Console.WriteLine(contact.PersonalPet.PetName);
            Console.ReadKey();
        }

        static string DemoTheOldWayOfNullChecking()
        {
            Contact contact = new Contact();
            contact.Name = "Sean";
            contact.Phone = "12345678";
            contact.Age = 24;
            //contact.PersonalPet = new Pet()
            //{
            //    PetName = new PetName()
            //        {
            //            Name = "My Pet"
            //        }
            //};

            if (contact == null)
                return null;

            if (contact.PersonalPet == null)
                return null;

            if (contact.PersonalPet.PetName == null)
                return null;

            var petName = contact.PersonalPet.PetName.Name;
            if (petName == null)
                return null;

            return petName;
        }

        static string DemoTheNewWayOfNullChecking()
        {
            Contact contact = new Contact();
            contact.Name = "Sean";
            contact.Phone = "12345678";
            contact.Age = 24;
            //contact.PersonalPet = new Pet()
            //{
            //    PetName = new PetName()
            //        {
            //            Name = "My Pet"
            //        }
            //};

            // Maybe class allow you to do something similar to int? for the reference types.
            Maybe<Contact> maybeContact = new Maybe<Contact>(contact);

            var maybePetName = from c in maybeContact
                          from cp in c.PersonalPet
                          from pn in cp.PetName
                          select pn.Name;

            if (maybePetName.HasValue)
                return maybePetName.Value;

            return null;
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public Pet PersonalPet { get; set; }
    }

    public class Pet
    {
        public PetName PetName { get; set; }
    }

    public class PetName
    {
        public string Name { get; set; }
    }
}
