using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace project_for_Dental
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public ObservableCollection<Client> Clients { get; set; }
        public Add()
        {
            InitializeComponent();

            Clients = new ObservableCollection<Client>();
            DataGrid.ItemsSource = Clients;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // App.config faylidan connection string olish
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Dental_info;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ma'lumotlarni olish uchun SQL so'rovi
                    string selectQuery = "SELECT * FROM Client";

                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Ma'lumotlarni Client obyektiga yuklash
                        var client = new Client
                        {
                            AdmissionDate = reader.GetDateTime(reader.GetOrdinal("AdmissionDate")),
                            PatientName = reader.GetString(reader.GetOrdinal("PatientName")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            DoctorConsultant = reader.GetString(reader.GetOrdinal("DoctorConsultant")),
                            ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                            DiscountPercentage = reader.GetDecimal(reader.GetOrdinal("DiscountPercentage")),
                            VisitType = reader.GetString(reader.GetOrdinal("VisitType")),
                            AdmissionDetails = reader.GetString(reader.GetOrdinal("AdmissionDetails")),
                            DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount"))
                        };

                        // Ma'lumotlarni ObservableCollectionga qo'shish
                        Clients.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }
        }

        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // App.config faylidan connection string olish
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Dental_info;Integrated Security=True";
                string name = Name.Text;
                string date = Dateofbirth.Text;
                string tel = Telnomer.Text;
                string dc = DoctorName.Text;
                string sn = ServiceName.Text;
                string dp = DiscountPercentage.Text;
                string vt = VisitType.Text;
                string ad = AdmissionDetails.Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert SQL so'rovi
                    string insertQuery = "INSERT INTO Client (AdmissionDate, PatientName, DateOfBirth, PhoneNumber, DoctorConsultant, ServiceName, DiscountPercentage, VisitType, AdmissionDetails, DiscountAmount) " +
                                         "VALUES (@AdmissionDate, @PatientName, @DateOfBirth, @PhoneNumber, @DoctorConsultant, @ServiceName, @DiscountPercentage, @VisitType, @AdmissionDetails, @DiscountAmount)";

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    
                    // Parametrlarni qo'shish
                    command.Parameters.AddWithValue("@AdmissionDate", DateTime.Now); // Qabul kuni hozirgi sana
                    command.Parameters.AddWithValue("@PatientName", name);  // Bemor ismi
                    command.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(date));  // Tug'ilgan yil
                    command.Parameters.AddWithValue("@PhoneNumber", tel);  // Telefon raqami
                    command.Parameters.AddWithValue("@DoctorConsultant", dc);  // Shifokor maslahatchisi
                    command.Parameters.AddWithValue("@ServiceName", sn);  // Xizmat nomi
                    command.Parameters.AddWithValue("@DiscountPercentage", dp);  // Chegirma miqdori
                    command.Parameters.AddWithValue("@VisitType", vt);  // Tashrif turi
                    command.Parameters.AddWithValue("@AdmissionDetails", ad);  // Qabul haqida ma'lumot
                    command.Parameters.AddWithValue("@DiscountAmount", 100);  // Chegirma miqdori

                    // Ma'lumotlarni bazaga yozish
                    command.ExecuteNonQuery();
                }

                // Ma'lumotlar bazasiga saqlangandan so'ng DataGridni yangilash
                Clients.Clear();
                LoadData();  // Yangi ma'lumotlar bilan DataGridni to'ldirish
                MessageBox.Show("Ma'lumotlar saqlandi!");
                Name.Clear();
                Dateofbirth.Clear();
                Telnomer.Clear();                
                ServiceName.Clear();
                DiscountPercentage.Clear();
                VisitType.Clear();
                AdmissionDetails.Clear();
                Summa.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message);
            }
        }
    }
}
