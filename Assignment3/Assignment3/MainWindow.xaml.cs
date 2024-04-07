using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment3
{

    public partial class MainWindow : Window
    {
        private readonly Dat154Context dx = new();

        private readonly LocalView<Student> Students;

        public MainWindow()
        {
            InitializeComponent();

            Students = dx.Students.Local;

            dx.Students.Load();

            studList.DataContext = Students.OrderBy(x => x.Studentname);
        }

        private void sButton_Click(object sender, RoutedEventArgs e)
        {
            studList.DataContext = Students.Where(x => x.Studentname.Contains(sBox.Text)).OrderBy(x => x.Studentname);
        }

        private void course_Click(object sender, RoutedEventArgs e)
        {
            CourseSearch nW = new()
            {
            };
            nW.Show();
        }

        private void grades_Click(object sender, RoutedEventArgs e)
        {
            GradesSearch nW = new()
            {
            };
            nW.Show();
        }

        private void failStud_Click(object sender, RoutedEventArgs e)
        {
            FailedStudSearch nW = new()
            {
            };
            nW.Show();
        }

        private void editCourse_Click(object sender, RoutedEventArgs e)
        {
            EditorCourse nW = new()
            {
            };
            nW.Show();
        }
    }
}