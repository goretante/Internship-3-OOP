using System;
using System.Collections;
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
        while (true)
        {
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("\n-- GLAVNI IZBORNIK --");
            Console.WriteLine("1. Ispis svih kontakata");
            Console.WriteLine("2. Dodavanje novih kontakata u imenik");
            Console.WriteLine("3. Brisanje kontakata iz imenika");
            Console.WriteLine("4. Uređivanje preference kontakta");
            Console.WriteLine("5. Upravljanje kontaktom");
            Console.WriteLine("6. Ispis svih poziva");
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
                case "4":
                    ChangeContactPreference(phoneBook);
                    break;
                case "5":
                    ManageContact(phoneBook);
                    break;
                case "6":
                    PrintAllCalls(phoneBook);
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Neispravan unos.");
                    break;
            }
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

    static void ChangeContactPreference(Dictionary<Contact, List<Call>> phoneBook)
    {
        Console.Write("Upišite broj mobitela kojeg želite urediti: ");
        string phoneNumber = Console.ReadLine();

        var contact = phoneBook.Keys.FirstOrDefault(k => k.PhoneNumber == phoneNumber);

        if (contact != null) 
        {
            Console.Write("Upišite novu preferencu (favorit, normalan, blokiran): ");
            string newPreference = Console.ReadLine();
            contact.Preference = newPreference;
            Console.WriteLine("Preferenca je ažurirana!");
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem ne postoji.");
        }
    }

    static void PrintCallsWithContact(KeyValuePair<Contact, List<Call>> contact)
    {
        var calls = contact.Value;
        calls.Sort((x, y) => DateTime.Compare(y.CallEstablishmentTime, x.CallEstablishmentTime));

        foreach (var call in calls)
        {
            Console.WriteLine($"{call.CallEstablishmentTime} - {call.Status}");
        }
    }

    static void CreateNewCall(KeyValuePair<Contact, List<Call>> contact)
    {
        if (contact.Key.Preference == "blokiran")
        {
            Console.WriteLine("Nemoguće je uspostaviti poziv s blokiranim kontaktom!");
            return;
        }

        if (contact.Value.Any(call => call.Status == "u tijeku"))
        {
            Console.WriteLine("Već ste u pozivu s ovim kontaktom!");
            return;
        }

        DateTime timeOfEstablishent = DateTime.Now;
        string status = "u tijeku";

        Call call = new Call
        { 
            CallEstablishmentTime = timeOfEstablishent,
            Status = status
        };

        contact.Value.Add(call);

        Console.WriteLine($"Poziv je uspostavljen - {status}");

        // simuation of call duration
        System.Threading.Thread.Sleep(500);

        // simulation of end of call
        status = new Random().Next(2) == 0 ? "propušten" : "završen";
        call.Status = status;

        Console.WriteLine($"Poziv prekinut - {status}");
    }

    static void ManageContact(Dictionary<Contact, List<Call>> phoneBook)
    {
        Console.Write("Unesite broj mobitela kontakta: ");
        string phoneNumber = Console.ReadLine();

        var contact = phoneBook.Keys.FirstOrDefault(k => k.PhoneNumber == phoneNumber);

        if (contact != null)
        {
            while(true)
            {
                Console.WriteLine("-- PODIZBORNIK --");
                Console.WriteLine("1. Ispis svih poziva s tim kontaktom");
                Console.WriteLine("2. Novi poziv");
                Console.WriteLine("3. Izlaz iz podizbornika");

                string submenuChoice = Console.ReadLine();

                switch (submenuChoice)
                {
                    case "1":
                        PrintCallsWithContact(new KeyValuePair<Contact, List<Call>> (contact, phoneBook[contact]));
                        break;
                    case "2":
                        CreateNewCall(new KeyValuePair<Contact, List<Call>>(contact, phoneBook[contact]));
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Kontakt s tim brojem ne postoji.");
        }
    }  

    static void PrintAllCalls(Dictionary<Contact, List<Call>> phoneBook)
    {
        foreach (var contact in phoneBook)
        {
            Console.WriteLine($"{contact.Key.NameSurname} - {contact.Key.PhoneNumber}");
            PrintCallsWithContact(contact);
            Console.WriteLine();
        }

        Console.WriteLine("Pritisni ENTER za nastavak...");
        Console.ReadLine();
    }
}

