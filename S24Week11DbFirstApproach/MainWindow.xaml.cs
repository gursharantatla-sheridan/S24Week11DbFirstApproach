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
            //var students = db.Students.ToList();

            var students = (from std in db.Students
                           select new { std.StudentID, std.StudentName, std.Standard.StandardName }).ToList();


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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Student std = new Student();
            std.StudentName = txtName.Text;
            std.StandardId = (int)cmbStandard.SelectedValue;

            db.Students.Add(std);
            db.SaveChanges();

            LoadStudents();
            MessageBox.Show("New student added");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var std = db.Students.Find(id);

            std.StudentName = txtName.Text;
            std.StandardId = (int)cmbStandard.SelectedValue;

            db.SaveChanges();
            LoadStudents();
            MessageBox.Show("Student updated");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var std = db.Students.Find(id);

            db.Students.Remove(std);
            db.SaveChanges();

            LoadStudents();
            MessageBox.Show("Student deleted");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // LINQ - Language INtegrated Query

            // query syntax
            //var students = (from std in db.Students
            //               where std.StudentName.Contains(txtName.Text)
            //               select std).ToList();

            // method syntax
            var students = db.Students
                                .Where(s => s.StudentName.Contains(txtName.Text))
                                .ToList();


            grdStudents.ItemsSource = students;
        }
    }
}
