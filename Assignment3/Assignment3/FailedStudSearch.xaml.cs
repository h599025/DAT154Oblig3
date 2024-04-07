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
    public partial class FailedStudSearch : Window
    {
        private readonly Dat154Context dbCourse = new();

        public FailedStudSearch()
        {
            InitializeComponent();

            var failedStuds = dbCourse.Grades
                .Include(c => c.Student)
                .Where(c => c.Grade1 == "F")
                .Select(c => new
                {
                    Name = c.Student.Studentname,
                    Course = c.Coursecode,
                    Grade = c.Grade1
                })
                .ToList();

            failedStudList.DataContext = failedStuds;
        }
    }
}
