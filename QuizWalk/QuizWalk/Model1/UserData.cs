using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWalk.Model1
{
    public class UserData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasValue { get; set; }

        public UserData()
        {
            Id = new Random().Next();
        }

        public UserData(string Name)
        {
            Id = new Random().Next();
            this.Name = Name;
            HasValue = true;
        }
    }
}
