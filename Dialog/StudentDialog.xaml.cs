using Canvas.ViewModel;
using Library.Models;
using Library.Services;
namespace Canvas.Dialog;


[QueryProperty(nameof(PersonId), "personId")]
public partial class StudentDialog : ContentPage
{
	public StudentDialog()
	{
		InitializeComponent();
	}

	public int PersonId
	{
		set;
		get;
    }

	void SubmitClicked(Object sender, EventArgs e)
	{
		(BindingContext as StudentDialogViewModel)?.AddStudent();
		Shell.Current.GoToAsync("//Student");
    }

	void BackClicked(Object sender, EventArgs e) { 
        Shell.Current.GoToAsync("//Student");
	}

	private void OnArriving(Object sender, NavigatedToEventArgs e) {
		BindingContext = new StudentDialogViewModel(PersonId);
	}


}