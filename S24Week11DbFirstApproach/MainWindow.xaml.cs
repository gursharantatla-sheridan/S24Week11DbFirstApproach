using System;
using System.Collections.Generic;
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

namespace S24Week11DbFirstApproach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // create an instance of context class
        SchoolDBEntities db = new SchoolDBEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadStudents()
        {
            var students = db.Students.ToList();
            grdStudents.ItemsSource = students;
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var standards = db.Standards.ToList();

            cmbStandard.ItemsSource = standards;
            cmbStandard.DisplayMemberPath = "StandardName";
            cmbStandard.SelectedValuePath = "StandardId";
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var student = db.Students.Find(id);

            if (student != null)
            {
                txtName.Text = student.StudentName;
                cmbStandard.SelectedValue = student.StandardId;
            }
            else
            {
                txtName.Text = "";
                cmbStandard.SelectedIndex = -1;
                MessageBox.Show("Invalid ID. Please try again.");
            }
        }
    }
}
