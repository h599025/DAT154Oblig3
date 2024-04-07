using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for EditorCourse.xaml
    /// </summary>
    public partial class EditorCourse : Window
    {
        private readonly Dat154Context dx = new();

        public EditorCourse()
        {
            InitializeComponent();

            dx.Grades.Load();
            dx.Students.Load();
            dx.Courses.Load();

            dropBoxStud.ItemsSource = dx.Students.OrderBy(s => s.Studentname).ToList();
            dropBoxCourse.ItemsSource = dx.Courses.ToList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = (Student) dropBoxStud.SelectedItem;
            Course selectedCourse = (Course) dropBoxCourse.SelectedItem;

            if (selectedStudent != null && selectedCourse != null) {

                bool studExist = dx.Grades.Any(g => g.Studentid == selectedStudent.Id && g.Coursecode == selectedCourse.Coursecode);

                if (studExist)
                {
                    MessageBox.Show("Studenten eksisterer allerede", "Opprettelses Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {
                    string gradeInput = textBoxGrade.Text.Trim().ToUpper();
                    Regex validGradesRegex = new Regex(@"^[A-F]$");

                    if (!validGradesRegex.IsMatch(gradeInput))
                    {
                        MessageBox.Show("Ugyldig karakter. Vennligst skriv inn en karakter fra A til F.", "Opprettelses Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Grade s = new()
                    {
                        Studentid = selectedStudent.Id,
                        Coursecode = selectedCourse.Coursecode,
                        Grade1 = gradeInput
                    };

                    dx.Grades.Add(s);
                    dx.SaveChanges();
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = (Student) dropBoxStud.SelectedItem;
            Course selectedCourse = (Course) dropBoxCourse.SelectedItem;

            if (selectedStudent != null && selectedCourse != null)
            {
                Grade gradeToRemove = dx.Grades.SingleOrDefault(g => g.Studentid == selectedStudent.Id && g.Coursecode == selectedCourse.Coursecode);

                if (gradeToRemove != null)
                {
                    dx.Grades.Remove(gradeToRemove);
                    dx.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Studenten eksisterer ikke", "Slette Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
