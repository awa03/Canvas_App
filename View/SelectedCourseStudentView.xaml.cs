using Canvas.ViewModel;
namespace Canvas.View;


[QueryProperty(nameof(PersonId), "personId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class SelectedCourseStudentView : ContentPage
{
	// Passed in properties ----- data for loading the view and student ... 
	public int PersonId { get; set; }
	public int CourseId { get; set; }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new SelectedCourseStudentViewModel(PersonId, CourseId);
	}
	// ---------------------------------------- End of Passed in properties

	public void AssignmentSelectedClicked(object sender, EventArgs e)
	{
		(BindingContext as SelectedCourseStudentViewModel)?.GoToSubmissionPage(Shell.Current);
    }

	public void BackClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Student");
    }

	public SelectedCourseStudentView()
	{
		InitializeComponent();
	}

}