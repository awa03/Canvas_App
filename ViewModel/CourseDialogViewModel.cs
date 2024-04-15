using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    class CourseDialogViewModel
    {

        InstructorService instructorService { get; set; }
        public Person instructor { get; set; }
        public Course course { get; set; }
        public Course originalCourse { get; set; }

        public CourseService courseService { get; set; }

        public CourseDialogViewModel(int loadInstructorId, int loadCourse)
        {
            instructorService = InstructorService.Current;
            courseService = CourseService.Current;
            instructor = instructorService.GetInstructor(loadInstructorId);
            if (loadCourse == SelectedInstructorViewModel.Default)
            {
                course = new Course();
            }
            else {
                originalCourse = courseService.GetCourseId(loadCourse);
                course = originalCourse;
            }

        }

        // Needs Refactoring  ---  Course Structure is shit
        public void AddCourse() {
            InstructorService i = InstructorService.Current;
            var ins = i.GetInstructor(instructor.Id);
            var c = ins.courseList.IndexOf(originalCourse);
            if (c != -1)
            {
                var index = ins.courseList.IndexOf(originalCourse);
                ins.courseList.RemoveAt(index);
                ins.courseList.Insert(index, course);

                var index2 = courseService.CourseList.IndexOf(originalCourse);
                courseService.CourseList.RemoveAt(index2);
                ins.courseList.Insert(index2, course);
            }
            else
            {
                courseService.CourseList.Add(course);
                ins.courseList.Add(course);
            }
        }


        public string InstructorName {
            get {
                if (instructor == null) { instructor = new Person() { year = Person.Classification.Staff }; }
                return instructor.Name;
            }
        }

        public string InstructorEmail {
            get {
                if (instructor == null) { instructor = new Person() { year = Person.Classification.Staff }; }
                return instructor.Email;
            }
        }

        public string CourseName {
            get {
                if (course == null) { course = new Course(); }
                return course.Name;
            }
            set {
                if (course == null) { course = new Course(); }
                course.Name = value; 
            }
        }

        public string CourseCode { 
            get {
                if (course == null) { course = new Course(); }
                return course.Code;
            }
            set {
                if (course == null) { course = new Course();  }
                course.Code = value; 
            }
        }

        public string CourseDesc {
            get {
                if (course == null) { course = new Course(); }
                return course.Desc; 
            }
            set {
                if (course == null) { course = new Course(); }
                course.Desc = value; 
            }
        }

         public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        


    }
}
