using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels {
    
    class LoadingViewModel {

        public MemoryStream Gif { get; private set; } = new();

        public LoadingViewModel() {
            Properties.Resources.loading.Save(Gif, ImageFormat.Gif);
        }
    }
}
