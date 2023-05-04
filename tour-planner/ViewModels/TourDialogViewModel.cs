using System.Linq;
using System.Collections.Generic;
using TourPlanner.Logic.Service;
using TourPlanner.Model;
using System.Diagnostics.CodeAnalysis;

namespace TourPlanner.ViewModels {

    public class TourDialogViewModel : EntityDialogViewModel<Tour> {

        [SuppressMessage("Usage", "CA1822", Justification = "Intended for use in XAML.")]
        public List<TransportType> TransportTypes => TransportType.ALL.ToList();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourDialogViewModel(TourService tourService) : base(tourService) {
            // noop
        }
    }
}
