using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    public class SelectedCourseViewModel : INotifyPropertyChanged
    {
        public CourseService courseSrv;
        public SelectedCourseViewModel(int personId, int courseId)
        {
            instructorSrv = InstructorService.Current; 
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current; 
            instructor = instructorSrv.GetInstructor(personId);
            _course = courseSrv.GetCourseId(courseId);
            Query = string.Empty;
            SelectedStudent = null;
            SelectedAssignment = null;
        }

        public string Query { get; set; }

        public ObservableCollection<Person> StudentsInCourse {
            get {
                return new ObservableCollection<Person>( course.Search(Query)
                    );
            }
        }

        public InstructorService instructorSrv;
        public StudentService studentSrv;

        public event PropertyChangedEventHandler? PropertyChanged;

         private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Person instructor { get; set; }
        private Course _course { get; set; }
        public Course course
        {
            get
            {
                return _course; 
            }
            set
            {
                _course = value; 
            }
        }


        public void Search() {
            NotifyPropertyChanged(nameof(StudentsInCourse));
        }

        private Person? selectedStudent { get; set; }

        public Person? SelectedStudent
        {
            get {
                return selectedStudent; 
            }
            set {
                selectedStudent = value; 
                NotifyPropertyChanged(nameof(SelectedStudentAssignments));
            }
        }

        public ObservableCollection<Assignment> SelectedStudentAssignments {
            get {
                if (SelectedStudent == null) return new ObservableCollection<Assignment>();
                return new ObservableCollection<Assignment>(course.GetStudentsAssignments(SelectedStudent.Id));
            }
        }
        public Assignment SelectedAssignment { get; set; }

        public static int DefaultAssignmentId = 0;
        public static int DefaultStudentId = 0;

        // Checks if the selected student is null, if so, it will not allow the user to add an assignment
        // If the selected assignment is null, it will take the user to the assignment detail page with the default assignment id
        // If the selected assignment is not null, it will take the user to the assignment detail page with the selected assignment id
        public void AddAssignment(Shell s)
        {
            if (selectedStudent == null) { return; } // Will implement later - j quality of life change
            s.GoToAsync($"//AssignmentDetail?instructorId={instructor.Id}&personId={SelectedStudent?.Id}&courseId={course.Id}&assignmentId={DefaultAssignmentId}");
            NotifyPropertyChanged(nameof(SelectedStudentAssignments));
        }

        public void EditAssignment(Shell s)
        {
            if(selectedStudent == null) { return;  }
            if (SelectedAssignment == null) { return; }
            s.GoToAsync($"//AssignmentDetail?instructorId={instructor.Id}&personId={SelectedStudent?.Id}&courseId={course.Id}&assignmentId={SelectedAssignment.Id}");
            NotifyPropertyChanged(nameof(SelectedStudentAssignments));
        }

        public void DeleteAssignment()
        {
            course.DeleteAssignmentFromSelectStudent(SelectedStudent.Id, SelectedAssignment);
            NotifyPropertyChanged(nameof(SelectedStudentAssignments));
        }

        public void ViewAssignments() {
            NotifyPropertyChanged(nameof(SelectedStudentAssignments));
        }

        public string CourseName { get
            {
                return course.Name;

            }
                   set
            {
                course.Name = value; 
            }
        }
        public int InstructorId { get {
                return instructor.Id;

            }
            set {
                instructor.Id = value; 
            }
        } 
        public string CourseCode { 
            get {
                return _course.Code;

            }
            set {
                _course.Code = value; 
            }
        } 
        public string CourseDescription {
            get {
                return course.Desc; 
            }

            set { 
                _course.Desc = value;
            }
        }
    }
}
