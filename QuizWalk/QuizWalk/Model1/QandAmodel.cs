using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWalk.Model1
{
    public class QandAmodel
    {
        public string question { get; set; }
        public string answerA { get; set; }
        public string answerB { get; set; }
        public string answerC { get; set; }
        public string answerD { get; set; }
        public string letterA { get; set; }
        public string letterB { get; set; }
        public string letterC { get; set; }
        public string letterD { get; set; }

        public QandAmodel(string question, string answerA, string answerB, string answerC, string answerD, string letterA, string letterB, string letterC, string letterD)
        {
            this.question = question;
            this.answerA = answerA;
            this.answerB = answerB;
            this.answerC = answerC;
            this.answerD = answerD;
            this.letterA = letterA;
            this.letterB = letterB;
            this.letterC = letterC;
            this.letterD = letterD;
        }

        public QandAmodel()
        {

        }
    }
}
