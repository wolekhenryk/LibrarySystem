using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}
        public DateTime HireDate { get; set; }
        public int Books { get; set; }
        public List<Person> Db { get; set; }

        protected Person()
        {
            Db = new List<Person>();
        }
        protected Person(int id)
        {
            Id = id;
        }
        protected Person(string personName, string personSurname, string email, string password)
        {
            Name = personName;
            Surname = personSurname;
            CreatedAt = DateTime.Now;
            LastUpdatedAt = CreatedAt;
            Email = email;
            HashedPassword = HashPassword(password);
        }

        public abstract void DisplayBasicInfo();
        public abstract void Login();
        public void SetId(int index) => Id = index;

        public string HashPassword(string password)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA512.Create().ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }

    public class Customer : Person
    {
        public Customer(int cId) : base(cId) { }
        public Customer(string name, string surname, string email, string password) : base(name, surname, email,
            password) => Db.Add(this);

        public override void DisplayBasicInfo()
        {
            Console.WriteLine("This is a CUSTOMER");
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Surname: {0}", Surname);
            Console.WriteLine("Created/Updated at: {0} {1}", CreatedAt, LastUpdatedAt);
        }

        public override void Login() => throw new NotImplementedException();
    }

    public class Staff : Person
    {
        public Staff(string name, string surname, string email, string password) :
            base(name, surname, email, password) => Db.Add(this);

        public override void DisplayBasicInfo()
        {
            Console.WriteLine("This is a STAFF MEMBER");
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Surname: {0}", Surname);
            Console.WriteLine("Created/Updated at: {0} {1}", CreatedAt, LastUpdatedAt);
        }

        public virtual void GetBasicCustomerInfo(int customerId)
        {
            if (!Db.Contains(new Customer(customerId)))
            {
                throw new KeyNotFoundException();
            }
            var mCustomer = Db.Find(mPerson => mPerson.Id == customerId);
            mCustomer.DisplayBasicInfo();
        }

        public override void Login() => throw new NotImplementedException();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
