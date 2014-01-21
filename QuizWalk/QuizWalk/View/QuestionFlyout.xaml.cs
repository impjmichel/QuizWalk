using QuizWalk.Model1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public void loadText(int questionNumber)
        {
            this.questionNumber = questionNumber;
            model = 
                (
                from c in System.Xml.Linq.XDocument.Load("QandA.xml").Root.Descendants("Question")
                where c.Attribute("number").Equals(questionNumber.ToString())
                select
                new QandAmodel(c.Element("Ask").ToString(), 
                    c.Element("AnswerA").ToString(), c.Element("AnswerB").ToString(), c.Element("AnswerC").ToString(), c.Element("AnswerD").ToString(),
                    c.Element("AnswerA").Attribute("letter").ToString(), c.Element("AnswerB").Attribute("letter").ToString(), c.Element("AnswerC").Attribute("letter").ToString(), c.Element("AnswerD").Attribute("letter").ToString())
            
                ).First();

            QuestionBlock.Text = model.question;
            AnswerOne.Content = model.answerA;
            AnswerTwo.Content = model.answerB;
            AnswerThree.Content = model.answerC;
            AnswerFour.Content = model.answerD;
        }

        private void AnsweredA(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void AnsweredB(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AnsweredC(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AnsweredD(object sender, TappedRoutedEventArgs e)
        {

        }

        private int getMapPageColumn(int questionNr)
        {
            return 0;
        }
    }
}
