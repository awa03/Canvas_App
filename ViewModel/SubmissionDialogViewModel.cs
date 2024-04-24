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
    public class SubmissionDialogViewModel : INotifyPropertyChanged
    {

        public StudentService studentSrv { get; set; }
        public CourseService courseSrv { get; set; }
        public Submission currSubmission { get; set; }
        public bool isSubmitted { get; set; }
        public bool isSubmitRun { get; set; }


        public SubmissionDialogViewModel(int PersonId, int CourseId, int AssignmentId) { 
            studentSrv = StudentService.Current;
            courseSrv = CourseService.Current;

            _student = studentSrv.GetStudent(PersonId);
            _course = _student.GetStudentsCourse(CourseId);

            SelectedAssignment = _course.GetAssignment(AssignmentId);
            currSubmission = new Submission();
        }

        public void Submit() {
            Refresh();
            if (SelectedAssignment.SubmitAssignment(currSubmission))
            {
                SubmissionMessage = "Assignment Submitted";
                course.AddAssignmentToSelectStudent(student.Id, SelectedAssignment);
            }
            else
            {
                SubmissionMessage = "Error Submitting Assignment";
            }

            isSubmitRun = true;

            NotifyPropertyChanged(nameof(isSubmitRun));
            NotifyPropertyChanged(nameof(SubmissionMessage));
        }

        // Only abstracted for futrue proofing
        public void Refresh() {
            NotifyPropertyChanged(nameof(SubmissionText));
        }

        private Person _student { get; set; }
        private Course _course { get; set; }

        public Assignment SelectedAssignment { get; set; }

        // Public accessors for the private fields

        public Person student {
            get {
                return _student;
            }
            set {
                _student = value; 
            }
        }

        public Course course
        {
            get { 
                return _course; 
            }
            set
            {
                _course = value; 
            }
        }

        // Data Binding Text ---- Only used for displaying

        public string AssignmentName
        {
            get
            {
                return SelectedAssignment.Name;
            }
        }

        // Data Binding Text ---- For the submission

        public string SubmissionText
        {
            get
            {
                return currSubmission.submissionText;
            }
            set
            {
                currSubmission.submissionText = value;
            }
        }

        public string SubmissionMessage
        {
            get; private set;
        }

        // For Text Updating --------- Boilerplate Code
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
