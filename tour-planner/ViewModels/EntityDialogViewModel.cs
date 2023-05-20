using System;
using TourPlanner.Logic.Service;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public abstract class EntityDialogViewModel<E> : BaseViewModel where E : Entity, new() {

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private E _entity = new();
        public E Entity {
            get => _entity;
            private set {
                _entity = value;
                RaisePropertyChanged();
            }
        }

        private string _dialogTitle = "";
        public string DialogTitle {
            get => _dialogTitle;
            private set {
                _dialogTitle = value;
                RaisePropertyChanged();
            }
        }

        public Action Close { get; private set; } = () => { };
        public Action<bool> SetDialogResult { get; private set; } = (result) => { };

        private readonly IShowErrorService _showErrorService;

        private readonly ICrudService<E> _crudService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public EntityDialogViewModel(ICrudService<E> crudService, IShowErrorService showErrorService) {
            _crudService = crudService;
            _showErrorService = showErrorService;
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public virtual void Init(E entity, Action close, Action<bool> setDialogResult) {
            Entity = entity;
            string entityName = typeof(E).Name;
            DialogTitle = (entity.GetGuid() == Guid.Empty ? "Create " : "Edit ") + entityName;
            Close = close;
            SetDialogResult = setDialogResult;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        protected virtual void Save() {
            try {
                if (Entity.GetGuid() == Guid.Empty) {
                    _crudService.Add(Entity);
                } else {
                    _crudService.Update(Entity);
                }
                SetDialogResult(true);
                Close();
            } catch (ValidationException e) {
                _showErrorService.ShowErrors(e);
            }
        }

        private void Cancel() {
            SetDialogResult(false);
            Close();
        }
    }
}
