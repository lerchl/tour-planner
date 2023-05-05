using TourPlanner.Model;

namespace TourPlanner {

    /// <summary>
    ///     Interface for the DialogService.
    /// </summary>
    public interface IDialogService {

        bool OpenAddTourDialog();
        bool OpenEditTourDialog(Tour tour);

        bool OpenAddTourLogDialog(Tour tour);
        bool OpenEditTourLogDialog(TourLog tourLog);
    }
}
