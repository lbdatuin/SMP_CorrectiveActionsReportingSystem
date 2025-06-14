using MudBlazor;
namespace CARWeb.Client.Utilities
{
    public class SubmitModal
    {
        private readonly IDialogService _dialog;

        public SubmitModal(IDialogService dialog)
        {
            _dialog = dialog;
        }

        public async Task ShowSuccess(string? message)
        {
            var parameters = new DialogParameters();
            parameters.Add("message", message);
            var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, CloseButton = true };

            var dialogRef = _dialog.Show<SuccessPopUpDialog>("", parameters, options);

            // Wait for 2 seconds before closing
            await Task.Delay(3000);

            // Close the dialog
            dialogRef.Close();
        }


    }
}
