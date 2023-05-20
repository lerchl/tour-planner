using System;
using System.Windows;
using TourPlanner.Logic.Validation;

namespace TourPlanner {

    public class ShowErrorService : IShowErrorService {

        public void ShowError(Exception e) {
            ShowErrorMessageBox(e.Message);
        }

        public void ShowErrors(ValidationException e) {
            var message = "Following error(s) occured:\n\n";
            e.Result.Messages.ForEach(m => message += "- " + m.Message + "\n");
            ShowErrorMessageBox(message);
        }

        private void ShowErrorMessageBox(string message) {
            var caption = "Error";
            var button = MessageBoxButton.OK;
            var image = MessageBoxImage.Error;

            MessageBox.Show(message, caption, button, image);
        }
    }
}
