using Canvas.ViewModel;

namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDialog : ContentPage
{
	public CourseDialog()
	{
		InitializeComponent();
	}

	public void SubmitClicked(Object sender, EventArgs e) {

		(BindingContext as CourseDialogViewModel)?.AddCourse();
		Shell.Current.GoToAsync($"//SelectedInstructor?personId={PersonId}");
	}

	public void BackClicked(Object sender, EventArgs e) { 
		(BindingContext as CourseDialogViewModel)?.AddCourse();
		Shell.Current.GoToAsync($"//SelectedInstructor?personId={PersonId}");
	}


	public int PersonId { get; set; }
	public int CourseId { get; set; }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new CourseDialogViewModel(PersonId, CourseId);
    }
}