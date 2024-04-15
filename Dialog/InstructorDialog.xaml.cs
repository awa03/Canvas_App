using Canvas.ViewModel;

namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
public partial class InstructorDialog : ContentPage
{
	public InstructorDialog()
	{
		InitializeComponent();
	}
	public void SubmitClicked(Object sender, EventArgs e)
	{
		(BindingContext as InstructorDialogViewModel)?.AddInstructor();
        Shell.Current.GoToAsync("//Instructor");
    }

	public void BackClicked(Object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Instructor");
    }

	public int PersonId
	{
        set;
        get;
    }


	private void OnArriving(Object sender, NavigatedToEventArgs e) {
		BindingContext = new InstructorDialogViewModel(PersonId);
	}


}