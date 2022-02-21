using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikacjaBankowa
{
    public class Client
    {
        public int id;
        public string firstname;
        public string lastname;
        public string accountNumber;
        public decimal balance;
        public Client(int id, string firstname, string lastname, string accountNumber, decimal balance)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.accountNumber = accountNumber;
            this.balance = balance;
        }
    }
}
