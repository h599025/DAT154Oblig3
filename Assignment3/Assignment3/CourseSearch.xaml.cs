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
    public partial class CourseSearch : Window
    {
        private readonly Dat154Context dbCourse = new();

        private readonly LocalView<Course> Courses;

        public CourseSearch()
        {
            InitializeComponent();

            Courses = dbCourse.Courses.Local;

            dbCourse.Courses.Load();

            dropBoxCourse.ItemsSource = Courses.ToList();
        }

        private void sButton_Click_1(object sender, RoutedEventArgs e)
        {
            Course selectedCourse = (Course)dropBoxCourse.SelectedItem;

            if(selectedCourse != null) {
                var students = dbCourse.Grades
                    .Include(c => c.Student)
                    .Where(c => c.Coursecode == selectedCourse.Coursecode)
                    .Select(c => new
                    {
                        Name = c.Student.Studentname,
                        Grade = c.Grade1
                    })
                    .ToList();

                courseList.ItemsSource = students;
            }
        }
    }
}
