using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDTest.API.Models;

namespace TDDTest.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    Name  = "Test User 1",
                    Email = "User1@test.com",
                    Address = new Address
                    {
                        Street = "900 Market Street",
                        City = "Southend",
                        ZipCode = "ss122u"
                    }
                },
                new User
                {
                    Name  = "Test User 2",
                    Email = "User2@test.com",
                    Address = new Address
                    {
                        Street = "123 North Street",
                        City = "Manchester",
                        ZipCode = "m4nc3t"
                    }
                },

                new User
                {
                    Name  = "Test User 3",
                    Email = "User3@test.com",
                    Address = new Address
                    {
                        Street = "41 Bakery Lane",
                        City = "Liverpool",
                        ZipCode = "li4p0l"
                    }
                }
            };
    }
}
