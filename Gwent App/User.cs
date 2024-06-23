using Gwent_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_App
{
    public class User
    {
        public User(string name, string password, int winnings, int losing)
        {
            Name = name;
            Winnings = winnings;
            Password = password;
            Losing = losing;
        }
        public User()
        {
            Name = "";
            Password = "";
            Winnings = 0;
            Losing = 0;
        }
        public User(string name,string password)
        {
            Name = name;
            Password = password;
            Winnings = 0;
            Losing = 0;
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Winnings {  get; set; }
        public int Losing { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is User player)
            {
                return this.Name.Equals(player.Name) && this.Password.Equals(player.Password);
            }
            return false;
        }

    }
}
