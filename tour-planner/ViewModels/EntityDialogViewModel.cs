using System;
using System.ComponentModel;
using TourPlanner.Logic.Service;
using TourPlanner.Model;

namespace TourPlanner.ViewModels {

    public abstract class EntityDialogViewModel<E> : INotifyPropertyChanged where E : Entity, new() {

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private E _entity = new();
        public E Entity {
            get => _entity;
            private set {
                _entity = value;
                PropertyChanged?.Invoke(this, new(nameof(Entity)));
            }
        }

        private string _dialogTitle = "";
        public string DialogTitle {
            get => _dialogTitle;
            private set {
                _dialogTitle = value;
                PropertyChanged?.Invoke(this, new(nameof(DialogTitle)));
            }
        }

        public Action Close { get; private set; } = () => { };
        public Action<bool> SetDialogResult { get; private set; } = (result) => { };

        private readonly ICrudService<E> _crudService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public EntityDialogViewModel(ICrudService<E> crudService) {
            _crudService = crudService;
            SaveCommand = new RelayCommand(x => Save());
            CancelCommand = new RelayCommand(x => Cancel());
        }

        public void Init(E entity, Action close, Action<bool> setDialogResult) {
            Entity = entity;
            string entityName = typeof(E).Name;
            DialogTitle = (entity.GetGuid() == Guid.Empty ? "Create " : "Edit ") + entityName;
            Close = close;
            SetDialogResult = setDialogResult;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        private void Save() {
            if (Entity.GetGuid() == Guid.Empty) {
                _crudService.Add(Entity);
            } else {
                _crudService.Update(Entity);
            }
            SetDialogResult(true);
            Close();
        }

        private void Cancel() {
            SetDialogResult(false);
            Close();
        }
    }
}
