using System;
using TourPlanner.Logic.Validation;

namespace TourPlanner {

    public interface IShowErrorService {

        void ShowError(Exception e);

        void ShowErrors(ValidationException e);
    }
}
