using Assignment3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
using System.Windows.Shapes;

namespace Assignment3
{
    public partial class GradesSearch : Window
    {
        private readonly Dat154Context dbGrades = new();

        private readonly LocalView<Grade> Grades;
        private readonly LocalView<Student> Students;
        private readonly LocalView<Course> Courses;

        public GradesSearch()
        {
            InitializeComponent();

            Grades = dbGrades.Grades.Local;
            Students = dbGrades.Students.Local;
            Courses = dbGrades.Courses.Local;

            dbGrades.Grades.Load();
            dbGrades.Students.Load();
            dbGrades.Courses.Load();

            List<string> grades = new List<string> { "A", "B", "C", "D", "E", "F" };
            
            dropBoxGrades.ItemsSource = grades;
        }

        private void gButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGrade = dropBoxGrades.SelectedItem as String;

            if (selectedGrade != null)
            {
                var grades = new List<string> { "A", "B", "C", "D", "E", "F" };
                var selectedGradeIndex = grades.IndexOf(selectedGrade);

                var students = dbGrades.Grades
                    .ToList()
                    .Where(g => grades.IndexOf(g.Grade1) <= selectedGradeIndex)
                    .Select(g => new
                    {
                        Name = g.Student.Studentname,
                        Grade = g.Grade1,
                        Course = g.Coursecode
                    })
                    .ToList();

                gradeList.ItemsSource = students;
            }
        }
    }
}
