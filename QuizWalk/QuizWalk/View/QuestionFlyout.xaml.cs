using QuizWalk.Model1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace QuizWalk.View
{
    public sealed partial class QuestionFlyout : SettingsFlyout
    {
        private int questionNumber;
        private QandAmodel model { get; set; }

        public QuestionFlyout()
        {
            this.InitializeComponent();
        }

        public void loadText2(int questionNumber)
        {
            this.questionNumber = questionNumber;
            XDocument doc = XDocument.Load("Assets/QandA.xml");
            var questions = from elm in doc.Descendants("Question")
                         where (int)elm.Attribute("Id") == questionNumber
                         select new QandAmodel
                         {
                            question = (string)elm.Element("Ask"),
                            answerA = (string)elm.Element("AnswerA"),
                            answerB = (string)elm.Element("AnswerB"),
                            answerC = (string)elm.Element("AnswerC"),
                            answerD = (string)elm.Element("AnswerD"),
                            letterA = (string)elm.Element("AnswerA").Attribute("letter"),
                            letterB = (string)elm.Element("AnswerB").Attribute("letter"),
                            letterC = (string)elm.Element("AnswerC").Attribute("letter"),
                            letterD = (string)elm.Element("AnswerD").Attribute("letter")
                         };

            List<QandAmodel> list = questions.ToList<QandAmodel>();
            model = list[0];

            System.Diagnostics.Debug.WriteLine(list.Count);

            QuestionBlock.Text = model.question;
            AnswerOne.Content = model.answerA;
            AnswerTwo.Content = model.answerB;
            AnswerThree.Content = model.answerC;
            AnswerFour.Content = model.answerD;
        }

        private void AnsweredA(object sender, TappedRoutedEventArgs e)
        {
            MapPage.getInstance().changeLetter(getMapPageColumn(questionNumber), model.letterA);
            MapPage.getInstance().setValues(questionNumber.ToString(), true);
        }

        private void AnsweredB(object sender, TappedRoutedEventArgs e)
        {
            MapPage.getInstance().changeLetter(getMapPageColumn(questionNumber), model.letterB);
            MapPage.getInstance().setValues(questionNumber.ToString(), true);
        }

        private void AnsweredC(object sender, TappedRoutedEventArgs e)
        {
            MapPage.getInstance().changeLetter(getMapPageColumn(questionNumber), model.letterC);
            MapPage.getInstance().setValues(questionNumber.ToString(), true);
        }

        private void AnsweredD(object sender, TappedRoutedEventArgs e)
        {
            MapPage.getInstance().changeLetter(getMapPageColumn(questionNumber), model.letterD);
            MapPage.getInstance().setValues(questionNumber.ToString(), true);
        }

        private int getMapPageColumn(int questionNr)
        {          
            switch(questionNr)
            {
                case 1:
                    return 3;
                case 2:
                    return 5;
                case 3:
                    return 14;
                case 4:
                    return 1;
                case 5:
                    return 9;
                case 6:
                    return 8;
                case 7:
                    return 0;
                case 8:
                    return 15;
                case 9:
                    return 11;
                case 10:
                    return 2;
                case 11:
                    return 7;
                case 12:
                    return 12;
                case 13:
                    return 13;
                case 14:
                    return 6;
                case 15:
                    return 4;
                default:
                    return 10;
            }
        }
    }
}
