using System.Windows.Controls;
using System.Data.SqlClient;
using ClosedXML.Excel;
using Hospital.Model;
using System.Windows;
using System.Data;
using System.Linq;
using System.Xml;
using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace Hospital
{
    public partial class MedicalDiagnosticProcedure : Page
    {
        public MedicalDiagnosticProcedure()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new HospitalEntities9())
            {
                var treatments = context.MedicalDiagnosticProcedure.ToList();
                var patients = context.Patients.ToList();
                DGridPatients.ItemsSource = treatments.Join(patients, t => t.Id, p => p.Id, (t, p) => new {MedicalDiagnosticProcedure = t, Patients = p }).ToList();
            }
        }

        private void ExportXlsxButton(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=SPC\\STP;Database=Hospital;Integrated Security=True;";
            string query = "SELECT Patients.Surname, Patients.MedicalCardNumber, MedicalDiagnosticProcedure.EventType, MedicalDiagnosticProcedure.Results, MedicalDiagnosticProcedure.Recommendations FROM Patients JOIN MedicalDiagnosticProcedure ON Patients.Id = MedicalDiagnosticProcedure.IdPatient;";
            string filePath = "D:\\MedicalDiagnosticProcedure.xlsx";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataTable);
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    for (int i = 0; i < dataTable.Columns.Count; i++)
                        worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                            worksheet.Cell(i + 2, j + 1).Value = dataTable.Rows[i][j].ToString();

                    workbook.SaveAs(filePath);
                    MessageBox.Show("Данные успешно сохранены в файл: " + filePath);
                }
            }
        }

        private void ExportCsvButton(object sender, System.Windows.RoutedEventArgs e)
        {
            string connectionString = "Server=SPC\\STP;Database=Hospital;Integrated Security=True;";
            string query = "SELECT Patients.Surname, Patients.MedicalCardNumber, MedicalDiagnosticProcedure.EventType, MedicalDiagnosticProcedure.Results, MedicalDiagnosticProcedure.Recommendations FROM Patients JOIN MedicalDiagnosticProcedure ON Patients.Id = MedicalDiagnosticProcedure.IdPatient;";
            string filePath = "D:\\MedicalDiagnosticProcedure.csv";

            try
            {
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Data");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    writer.WriteStartElement("Row");
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        writer.WriteElementString(reader.GetName(i), reader[i].ToString());
                                    }
                                    writer.WriteEndElement();
                                }
                            }
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                MessageBox.Show("Данные успешно сохранены в файл: " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void ExportDocButton(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=SPC\\STP;Database=Hospital;Integrated Security=True;";
            string query = "SELECT Patients.Surname, Patients.MedicalCardNumber, MedicalDiagnosticProcedure.EventType, MedicalDiagnosticProcedure.Results, MedicalDiagnosticProcedure.Recommendations FROM Patients JOIN MedicalDiagnosticProcedure ON Patients.Id = MedicalDiagnosticProcedure.IdPatient;";
            string filePath = "D:\\MedicalDiagnosticProcedure.doc";

            try
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();
                    mainPart.Document.Append(body);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Paragraph para = new Paragraph();
                            para.Append(new Run(new Text(
                                $"Surname: {row["Surname"]}," +
                                $"MedicalCardNumber: {row["MedicalCardNumber"]}," +
                                $"EventType: {row["EventType"]}," +
                                $"Results: {row["Results"]}," +
                                $"Recommendations: {row["Recommendations"]},\n")));
                            body.Append(para);
                        }
                    }
                    MessageBox.Show("Данные успешно сохранены в файл: " + filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных в документ Word: {ex.Message}");
            }
        }
    }
}
