using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels {

    class LoadingViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _visiblity = "Hidden";
        public string Visibility {
            get => _visiblity;
            private set {
                _visiblity = value;
                PropertyChanged?.Invoke(this, new(nameof(Visibility)));
            }
        }

        public MemoryStream Gif { get; private set; } = new();

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public LoadingViewModel() {
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
