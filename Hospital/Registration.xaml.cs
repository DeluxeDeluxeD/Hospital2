using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Windows;
using Hospital.Model;
using System.IO;
using System;

namespace Hospital
{
    public partial class Registration : Page
    {
        private Frame myFrame;

        public Registration()
        {
            InitializeComponent();
            AppConnect.HospitalModel = new HospitalEntities9();
            AppFrame.frameMain = myFrame;
        }

        // Отображает изображение в новом окне
        private void QR(object sender, RoutedEventArgs e)
        {
            Window imageMessageBox = new Window();
            imageMessageBox.Title = "QR Code";
            imageMessageBox.Width = 500;
            imageMessageBox.Height = 500;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hospital;component/Resources/qr.jpg"));
            imageMessageBox.Content = image;
            imageMessageBox.ShowDialog();
        }

        private void DownloadContract(object sender, RoutedEventArgs e)
        {
            string fileName = "Форма_договора_предоставления_платных_медицинских_услуг.docx";

            byte[] docBytes = Properties.Resources.Форма_договора_предоставления_платных_медицинских_услуг; // Получаем байты документа из ресурсов

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "Документы Word (*.docx)|*.docx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllBytes(filePath, docBytes); // Записываем байты в выбранный файл
                MessageBox.Show("Документ загружен. Путь: " + filePath);
            }
        }

        private void DownloadApproval(object sender, RoutedEventArgs e)
        {
            string fileName = "Согласие_на_обработку_ПД.docx";

            byte[] docBytes = Properties.Resources.Согласие_на_обработку_ПД;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "Документы Word (*.docx)|*.docx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllBytes(filePath, docBytes);
                MessageBox.Show("Документ загружен. Путь: " + filePath);
            }
        }

        private void Done(object sender, RoutedEventArgs e)
        {
            if (Surname.Text == "" || Name.Text == "" || Patronymic.Text == "" || Nomer.Text == "" ||
                Seria.Text == "" || Gender.Text == "" || Address.Text == "" || Phone.Text == "" ||
                Mail.Text == "" || Work.Text == "" || Polis.Text == "" || Number.Text == "")
            {
                MessageBox.Show("Заполните все поля.");
            }
            else
            {
                Patients people = new Patients();
                people.Surname                 = Surname.Text;
                people.Name                    = Name.Text;
                people.Patronymic              = Patronymic.Text;
                people.PassportSeries          = int.Parse(Seria.Text);
                people.PassportNumber          = int.Parse(Nomer.Text);
                people.Gender                  = Gender.Text;
                people.Address                 = Address.Text;
                people.Telephone               = Phone.Text;
                people.Email                   = Mail.Text;
                people.PlaceWorks              = Work.Text;
                people.InsurancePolicy         = Polis.Text;
                people.MedicalCardNumber       = int.Parse(Number.Text);
                people.Birthdate               = DataPic.SelectedDate.Value;
                people.InsuranceExpirationDate = DataPolis.SelectedDate.Value;

                VariableClass.Surname    = Surname.Text;
                VariableClass.Name       = Name.Text;
                VariableClass.Patronymic = Patronymic.Text;
                VariableClass.Seria      = Seria.Text;
                VariableClass.Nomer      = Nomer.Text;
                VariableClass.Gender     = Gender.Text;
                VariableClass.Address    = Address.Text;
                VariableClass.Phone      = Phone.Text;
                VariableClass.Mail       = Mail.Text;
                VariableClass.Work       = Work.Text;
                VariableClass.Polis      = Polis.Text;

                if (AppConnect.HospitalModel != null)
                {
                    AppConnect.HospitalModel.Patients.Add(people);
                    AppConnect.HospitalModel.SaveChanges();
                    MessageBox.Show("Вы зарегистрированы!");

                    // Backup
                    string connectionString = "Server=SPC\\STP;Database=Hospital;Integrated Security=True;";
                    string databaseName = "Hospital";
                    string backupPath = "D:\\";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string backupQuery = $"BACKUP DATABASE {databaseName} TO DISK = '{backupPath + databaseName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".bak"}'";
                        using (SqlCommand command = new SqlCommand(backupQuery, connection))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Резервная копия успешно создана.");
                        }
                    }
                    NavigationService.Navigate(new PatientPersonalAccount());
                }
                else
                {
                    MessageBox.Show("Ошибка в подключении к базе данных.");
                }
            }
        }
    }
}
