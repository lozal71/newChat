using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proba
{
    class Proba
    {

        static void Main(string[] args)
        {
            using (UserContext db = new UserContext()) 
            {
                //db.Database.ExecuteSqlCommand("delete from Users");
                User user1 = new User { Name = "user1" };
                User user2 = new User { Name = "user2" };
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                var users = db.Users;
                foreach (User u in users)
                {
                    string s;
                    s = String.Format("id = {0}, name = {1}", u.Id, u.Name);
                    Console.WriteLine(s);
                }
                Application.Run(new Form1());
            }
        }
    }
}
