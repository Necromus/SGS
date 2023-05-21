using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ApplicationContext db = new ApplicationContext();

        public MainWindow()
        {
            InitializeComponent();

            City_ComboBox.ItemsSource = db.Cities.GroupBy(x=>x.city).Select(group => group.Key).ToList();
            Brigade_ComboBox.ItemsSource = db.Brigades.Select(x => x.brigade).ToList();

        }

        private void City_ComboBox_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            WorkShop_ComboBox.ItemsSource = (from w in db.WorkShops join c in db.Cities on w.cityId equals c.id where c.city == City_ComboBox.SelectedItem.ToString() group w by w.workShop into g select g.Key).ToList();
        }

        private void WorkShop_ComboBox_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (WorkShop_ComboBox.SelectedItem != null)
            {
                var query = from w in db.WorkShops join c in db.Cities on w.cityId equals c.id where c.city == City_ComboBox.SelectedItem.ToString() select new { id = w.id, workShop = w.workShop };
                Employee_ComboBox.ItemsSource = (from w in query join ee in db.Employees on w.id equals ee.workShopId where w.workShop == WorkShop_ComboBox.SelectedItem.ToString() select ee.employee).ToList();
            }
            else
            {
                Employee_ComboBox.ItemsSource=null;
            }
        }

        private void Accept_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Reports.Add(new Report
                {
                    city = City_ComboBox.SelectedItem.ToString(),
                    workShop = WorkShop_ComboBox.SelectedItem.ToString(),
                    employee = Employee_ComboBox.SelectedItem.ToString(),
                    brigade = Brigade_ComboBox.SelectedItem.ToString(),
                    change = Change_TextBox.Text
                });
                db.SaveChanges();


                string filePath = System.IO.Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reports.json");
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(db.Reports.ToList(), options);

                File.WriteAllText(filePath, jsonString);

                MessageBox.Show("Отчёт составлен!");
            }
            catch 
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
            
        }
    }
}
