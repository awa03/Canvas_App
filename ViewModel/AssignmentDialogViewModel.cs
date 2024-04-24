using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.ViewModel
{
    public class AssignmentDialogViewModel
    {

        public Course course { get; set; }

        public Person student { get; set; }

        public string Name {
            get { 
                return course.Name; }            
                
        }

        public StudentService studentSrv { get; set; } 
        public CourseService courseSrv { get; set; }
        public Assignment LoadAssignment { get; set; }
        public Person Instructor { get; set; }

        public bool AddToAll { get; set; }

        public AssignmentDialogViewModel(int InstructorId, int PersonId, int CourseId, int AssignmentId)
        {
            Instructor = InstructorService.Current.GetInstructor(InstructorId);
            if (PersonId == SelectedCourseViewModel.DefaultStudentId)
            {
                AddToAll = true;
            }
            else {
                AddToAll = false; 
            }
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current;
            course = courseSrv.GetCourseId(CourseId);
            student = studentSrv.GetStudent(PersonId);
            if (AssignmentId == SelectedCourseViewModel.DefaultAssignmentId) {
                LoadAssignment = new Assignment(); 
            }
            else
            {
                LoadAssignment = course.GetAssignment(AssignmentId);
            }
        }

        public string AssignmentName {
            get {
                return LoadAssignment.Name; 
            }
            set {
                LoadAssignment.Name = value; 
            }
        }

        public string AssignmentDescription
        {
            get
            {
                return LoadAssignment.Desc; 
            }
            set
            {
                LoadAssignment.Desc = value; 
            }
        }

        public int AssignmentPoints
        {
            get
            {
                return LoadAssignment.Points; 
            }
            set
            {
                LoadAssignment.Points = value; 
            }
        }

        public int AssignmentPossiblePoints
        {
            get
            {
                return LoadAssignment.PossiblePoints; 
            }
            set
            {
                LoadAssignment.PossiblePoints = value; 
            }
        }

        public DateTime AssignmentDueDate
        {
            get
            {
                return LoadAssignment.DueDate; 
            }
            set
            {
                LoadAssignment.DueDate = value; 
            }
        }

        public void DeleteAssignment()
        {
            if (AddToAll)
            {
                course.DeleteAssignmentFromAllStudents(LoadAssignment);
            }
            else
            {
                course.DeleteAssignmentFromSelectStudent(student.Id, LoadAssignment);
            }
        }
        

        public string SubmissionText {
            get {
                // May change to show all submissions
                return LoadAssignment.GetLastSubmission();
            } 
        }

        public void Save()
        {
            if (AddToAll || student == null)
            {
                course.AddAssignmentToAllStudents(LoadAssignment);
            }
            else
            {
               course.AddAssignmentToSelectStudent(student.Id, LoadAssignment);
            }
        }

    }
}
