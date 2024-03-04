using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hospital.Model;

namespace Hospital
{
    public partial class AuthorizationDoctor : Page
    {
        private Frame myFrame;

        public AuthorizationDoctor()
        {
            InitializeComponent();
            AppConnect.HospitalModel = new HospitalEntities9();
            AppFrame.frameMain = myFrame;
        }

        private void DoctorAuthorizationButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var user0bj = AppConnect.HospitalModel.Doctors.FirstOrDefault(x => x.Login == Txt_Login.Text && x.Password == Txt_Password.Password); // Создание динамической переменной user0bj, внутри которой будут храниться данные, получаемые из заполняемых полей
                if (user0bj == null)
                {
                    MessageBox.Show("Такого пользователя нет.");
                }
                else
                {
                    VariableClass.Surname = user0bj.Surname;
                    VariableClass.Name = user0bj.Name;
                    VariableClass.Patronymic = user0bj.Patronymic;
                    Manager.myFrame.Navigate(new DoctorPersonalAccount());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ОШИБКА! " + Ex.Message.ToString() + " Сделайте всё заново и проверьте поля!");
            }

        }
    }
}

