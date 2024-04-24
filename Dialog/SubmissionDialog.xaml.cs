using Canvas.ViewModel;

namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
[QueryProperty(nameof(CourseId), "courseId")]
[QueryProperty(nameof(AssignmentId), "assignmentId")]
public partial class SubmissionDialog : ContentPage
{
	public int PersonId {
		get; set;	
	}

	public int CourseId
	{
        get; set;
    }

	public int AssignmentId
	{
        get; set;
    }

	public SubmissionDialog()
	{
		InitializeComponent();
	}

	public void SubmitClicked(Object sender, EventArgs e) {
		(BindingContext as SubmissionDialogViewModel)?.Submit();
	}

	public void BackClicked(Object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//SelectedStudentCourse?personId={PersonId}&courseId={CourseId}");
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new SubmissionDialogViewModel(PersonId, CourseId, AssignmentId);
    }
}