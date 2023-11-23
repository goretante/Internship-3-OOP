using System;
using System.Collections.Generic;

class Contact
{
    public string NameSurname { get; set; }
    public string PhoneNumber {  get; set; }
    public string Preference { get; set; }
};

class Call
{
    public DateTime CallEstablishmentTime { get; set; }
    public string Status { get; set; }
}

class Program
{

    static void Main()
    {
        Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>();

        Console.WriteLine("\n-- GLAVNI IZBORNIK --");
        Console.WriteLine("1. Ispis svih kontakata");
        Console.WriteLine("2. Dodavanje novih kontakata u imenik");
        Console.WriteLine("3. Brisanje kontakata iz imenika");
        /* 
        Console.WriteLine("4. Uređivanje preference kontakta");
        Console.WriteLine("5. Upravljanje kontaktom");
        Console.WriteLine("6. Ispis svih poziva");
        */
        Console.WriteLine("7. Izlaz iz aplikacije");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                PrintContacts(phoneBook);
                break;
            case "2": 
                AddContact(phoneBook); 
                break;
            case "3":
                DeleteContact(phoneBook);
                break;
            case "7":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Neispravan unos.");
                break;
        }
    }

    static void PrintContacts(Dictionary<Contact, List<Call>> phoneBook)
    {
        foreach (var contact in phoneBook.Keys)
        {
            Console.WriteLine($"{contact.NameSurname} - {contact.PhoneNumber} ({contact.Preference})");
        }
    }

    static void AddContact(Dictionary<Contact, List<Call>> phoneBook)
    {
        Console.Write("Unesite ime i prezime: ");
        var nameSurname = Console.ReadLine();
        Console.Write("Unesite broj mobitela: ");
        string phoneNumber = Console.ReadLine();

        if (phoneBook.Keys.Any(contact => contact.PhoneNumber == phoneNumber))
        {
            Console.WriteLine("Kontakt s tim brojem mobitela je već u imeniku!");
            return;
        }

        Console.Write("Unesite preferencu (favorit, normalan, blokiran): ");
        string preference = Console.ReadLine();

        Contact contact = new Contact()
        {
            NameSurname = nameSurname,
            PhoneNumber = phoneNumber,
            Preference = preference
        };

        phoneBook[contact] = new List<Call>();
        Console.WriteLine("Dodali ste novi kontakt!");
    }

    static void DeleteContact(Dictionary<Contact, List<Call>> phoneBook)
    {
        Console.Write("Unesite broj kontakta kojeg želite obrisati: ");
        string phoneNumber = Console.ReadLine();

        var contact = phoneBook.Keys.FirstOrDefault(k => k.PhoneNumber == phoneNumber);

        if (contact != null)
        {
            phoneBook.Remove(contact);
            Console.WriteLine("Uspješno ste obrisali kontakt!");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem mobitela ne postoji!");
        }
    }
}

