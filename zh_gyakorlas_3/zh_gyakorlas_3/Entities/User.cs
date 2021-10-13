using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//a .Entites csak akkor jelenik meg, ha a mappában van létrehozva a User.cs
namespace zh_gyakorlas_3.Entities
{
    class User
    {
        //Kiírni --> Így lehet azonosítót generálni
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Kiírni --> Csak visszaadja a 2 nevet összefűzve
        public string FullName
        { 
            get 
            {
                return string.Format(
                    "{0} {1}",
                    LastName,
                    FirstName);
            }
        }

    }
}
