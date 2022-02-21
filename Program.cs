using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacjaBankowa
{
    public class Program
    {
        private static List<Client> clients;
        private static bool working = true;
        static void Main(string[] args)
        {
            // Dodawanie klientów na start
            clients = new List<Client>
            {
                new Client(1, "Jan", "Nowak", "001", 1457.53m),
                new Client(2, "Angieszka", "Kowalska", "002", 3600.18m),
                new Client(3, "Robert", "Lewandowski", "003", 2745.03m),
                new Client(4, "Zofia", "Płucińska", "004", 7344.00m),
                new Client(5, "Grzegorz", "Braun", "005", 455.38m)
            };

            Menu();
        }

        static void Menu()
        {
            while (working)
            {
                Console.WriteLine("WYBIERZ OPCJE:");
                Console.WriteLine("1 => LISTA WSZYSTKICH KLIENTÓW BANKU");
                Console.WriteLine("2 => LOGOWANIE");
                Console.WriteLine("3 => ZAKOŃCZ PROGRAM");
                Console.WriteLine("WYBIERZ 1, 2 LUB 3:");

                Console.CursorLeft = 0;
                var choice = Console.ReadKey();
                Console.Clear();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ListAllClients();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Login();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        working = false;
                        break;
                }
            }
        }
        static void ListAllClients()
        {
            Console.WriteLine("ID | IMIĘ I NAZWISKO | NR KONTA | SALDO");
            foreach(var client in clients)
            {
                Console.WriteLine($"{client.id} | {client.firstname} {client.lastname} | {client.accountNumber} | {client.balance:0.00} zł");
            }
        }
        static void Login()
        {
            // Przyjmujemy wartość od użytkownika
            Console.WriteLine("ZALOGUJ SIĘ WYBIERAJĄC ID KLIENTA:");
            var clientIdFromUser = Console.ReadLine();

            // Szukamy czy klient istnieje
            Client loggedClient = null;
            foreach(var client in clients)
            {
                if(client.id.ToString() == clientIdFromUser)
                {
                    loggedClient = client;
                    break;
                }
            }

            // Jeśli klient nie istnieje to kończymy działanie programu
            if(loggedClient == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("LOGOWANIE NIEUDANE");
                Console.ReadKey();
                working = false;
                return;
            }

            Transfer(loggedClient);
        }
        static void Transfer(Client loggedClient)
        {
            Console.Clear();
            Console.WriteLine("ZALOGOWANY KLIENT");
            Console.WriteLine($"ID: {loggedClient.id}");
            Console.WriteLine($"IMIĘ I NAZWISKO: {loggedClient.firstname} {loggedClient.lastname}");
            Console.WriteLine($"NR KONTA: {loggedClient.accountNumber}");
            Console.WriteLine($"SALDO: {loggedClient.balance} zł");
            Console.WriteLine("WPISZ NUMER KONTA NA KTÓRY CHCESZ WYKONAĆ PRZELEW:");
            var clientIdFromUser = Console.ReadLine();

            // Szukamy klienta według podanego ID
            Client recipientClient = null;
            foreach (var client in clients)
            {
                if (client.accountNumber == clientIdFromUser)
                {
                    recipientClient = client;
                    break;
                }
            }

            // Jeśli klient nie istnieje to kończymy działanie programu
            if (recipientClient == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("NIEPRAWIDŁOWY NUMER KONTA");
                Console.ReadKey();
                working = false;
                return;
            }

            if(recipientClient.id == loggedClient.id)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("NIE MOŻESZ ZROBIĆ PRZELEWU NA WŁASNE KONTO.");
                Console.ReadKey();
                working = false;
                return;
            }

            // Przyjmuejy kwotę przelewu
            Console.Clear();
            Console.WriteLine("PODAJ KWOTĘ PRZELEWU:");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("NIEPRAWIDŁOWA KWOTA");
                Console.ReadKey();
                working = false;
                return;
            }

            if(amount > loggedClient.balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("NIEWYSTARCZAJĄCE ŚRODKI NA RACHUNKU");
                Console.ReadKey();
                working = false;
                return;
            }

            if(amount < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine("UJEMNA KWOTA");
                Console.ReadKey();
                working = false;
                return;
            }

            // Ustawiamy balansy
            loggedClient.balance -= amount;
            recipientClient.balance += amount;

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.WriteLine("PRZELEW ZOSTAŁ WYKONANY");
            Console.ForegroundColor = originalColor;
            ListAllClients();
            Console.ReadKey();
            working = false;
        }
    }
}
