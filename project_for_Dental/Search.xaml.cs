using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
namespace project_for_Dental
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        public ObservableCollection<Client> Clients { get; set; }
        public Search()
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                // Agar qidiruv matni bo'sh bo'lsa, barcha ma'lumotlarni ko'rsatamiz
                DataGrid.ItemsSource = Clients;
                return;
            }

            var filtered = Clients.Where(p => p.PatientName.ToLower().Contains(query) ||
                                                p.DoctorConsultant.ToLower().Contains(query) ||
                                                p.PhoneNumber.Contains(query)).ToList();

            DataGrid.ItemsSource = filtered;
        }
    }
}
