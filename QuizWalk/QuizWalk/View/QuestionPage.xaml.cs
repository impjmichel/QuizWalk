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
    public sealed partial class QuestionPage : Page
    {
        private int questionNumber;
        private string answerLetterA;
        private string answerLetterB;
        private string answerLetterC;
        private string answerLetterD;

        public QuestionPage()
        {
            this.InitializeComponent();
        }

        public void loadText(int questionNumber)
        {
            this.questionNumber = questionNumber;
            // load the right text to the question block and answer buttons.
        }

        private void AnsweredA(object sender, TappedRoutedEventArgs e)
        {
            //set image of column X to answerLetterA
            this.Frame.Navigate(typeof(MapPage));
        }

        private void AnsweredB(object sender, TappedRoutedEventArgs e)
        {
            //set image of column X to answerLetterB
            this.Frame.Navigate(typeof(MapPage));
        }

        private void AnsweredC(object sender, TappedRoutedEventArgs e)
        {
            //set image of column X to answerLetterC
            this.Frame.Navigate(typeof(MapPage));
        }

        private void AnsweredD(object sender, TappedRoutedEventArgs e)
        {
            //set image of column X to answerLetterD
            this.Frame.Navigate(typeof(MapPage));
        }
    }
}
