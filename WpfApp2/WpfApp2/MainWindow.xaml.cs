using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Windows;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString =
            "Database=C:\\nenasheva_zachet\\analizy2.FDB;" +
            "DataSource=localhost;" +
            "Port=3050;" +
            "User=SYSDBA;" +
            "Password=student;" +
            "Charset=UTF8;";
        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFirebird();
        }
        private void LoadDataFromFirebird()
        {
            try
            {
                using (FbConnection connection = new FbConnection(ConnectionString))
                {
                    connection.Open();
                    string selectSql = "SELECT * FROM Pacient";
                    using (FbCommand command = new FbCommand(selectSql, connection))
                    {
                        DataTable dataTable = new DataTable();
                        using (FbDataAdapter dataAdapter = new FbDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                        PacientDataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (FbException ex)
            {
                MessageBox.Show($"Ошибка Firebird: {ex.Message}", "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Общая ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VIDANALIZA vIDANALIZA = new VIDANALIZA();
            vIDANALIZA.Show();
        }
    }
}