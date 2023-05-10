using System.Drawing.Imaging;
using System.IO;

namespace TourPlanner.ViewModels {

    public class LoadingIndicatorViewModel : BaseViewModel {

        private string _visiblity = "Hidden";
        public string Visibility {
            get => _visiblity;
            private set {
                _visiblity = value;
                RaisePropertyChanged();
            }
        }

        public MemoryStream Gif { get; private set; } = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public LoadingIndicatorViewModel() {
            Properties.Resources.loading_with_background.Save(Gif, ImageFormat.Gif);
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public void Show() {
            Visibility = "Visible";
        }

        public void Hide() {
            Visibility = "Hidden";
        }
    }
}
