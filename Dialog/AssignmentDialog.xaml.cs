using Canvas.ViewModel;

namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
[QueryProperty(nameof(CourseId), "courseId")]
[QueryProperty(nameof(AssignmentId), "assignmentId")]
public partial class AssignmentDialog : ContentPage
{
	public int PersonId
	{
        get;
        set;
    }
	public int CourseId
	{
        get;
        set;
    }
	public int AssignmentId
	{
        get;
        set;
    }

	public AssignmentDialog()
	{
		InitializeComponent();
	}

    public void SaveClicked(object sender, EventArgs e)
    {
        (BindingContext as AssignmentDialogViewModel)?.Save();
        Shell.Current.GoToAsync($"//SelectedCourse?personId={PersonId}&courseId={CourseId}");
    }

    public void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//SelectedCourse?personId={PersonId}&courseId={CourseId}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AssignmentDialogViewModel(PersonId, CourseId, AssignmentId);
    }
}