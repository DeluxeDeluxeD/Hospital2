using System.Windows;
using System;

namespace Hospital
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myFrame.Navigate(new Main());
            Manager.myFrame = myFrame;
        }

        public void back(object sender, RoutedEventArgs e)
        {
            Manager.myFrame.GoBack();
        }

        // Отображение/отключение кнопки возвращения
        private void myFrame_ContentRendered(object sender, EventArgs e)
        {
            if (myFrame.CanGoBack)
                TXTbutton.Visibility = Visibility.Visible;
            else
                TXTbutton.Visibility = Visibility.Hidden;
        }
    }
}
