using GridComboBox.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridComboBox.Data
{
    public static class UserReader
    {
        public static List<User> GetUsers(int count = 20)
        {
            string[] firstNames =
            {
                "Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona", "George", "Hannah", "Ivan", "Julia",
                "Kevin", "Laura", "Michael", "Nina", "Oscar", "Paula", "Quentin", "Rachel", "Samuel", "Tina",
                "Ulysses", "Vera", "William", "Xenia", "Yasmine", "Zach", "Francisco", "Pablo", "Sofia", "Diego", 
                "Camila", "Mateo", "Valentina", "Sebastian", "Isabella", "Liam", "Emma"
            };

            string[] lastNames =
            {
                "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Wilson", "Taylor", 
                "Martinez", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Robinson", "Clark", "Meraz"
            };

            string[] domains = { "example.com", "mail.com", "company.org", "test.net",  "gruporoothn.com"};
            Role[] roles = { Role.Admin, Role.User, Role.Guest };

            var users = new List<User>(count);

            for (int i = 0; i < count; i++)
            {
                string first = firstNames[Random.Shared.Next(firstNames.Length)];
                string last = lastNames[Random.Shared.Next(lastNames.Length)];
                string name = $"{first} {last}";
                string email = $"{first.ToLowerInvariant()}.{last.ToLowerInvariant()}{i + 1}@{domains[Random.Shared.Next(domains.Length)]}";

                users.Add(new User
                {
                    Name = name,
                    Email = email,
                    Role = roles[Random.Shared.Next(roles.Length)]
                });
            }

            return users;
        }
    }
}
