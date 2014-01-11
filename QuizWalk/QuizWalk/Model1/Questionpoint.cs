using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWalk.Model1
{
    public class Questionpoint
    {
        public string name { get; set; }
        public double longitude { get; set;}
        public double latitude { get; set; }
        public bool isAnswered {get;set;}

        public Questionpoint(string name, double latitude, double longitude)
        {
            this.name = name;
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }
}
