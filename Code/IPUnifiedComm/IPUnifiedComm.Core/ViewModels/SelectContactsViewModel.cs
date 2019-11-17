using System;
using System.Linq;
using IPUnifiedComm.Core.DataEntity;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class SelectContactsViewModel : BaseViewModel
    {
        private MvxObservableCollection<TaskAssignmentContact> level1;
        public MvxObservableCollection<TaskAssignmentContact> Level1
        {
            get => level1;
            set => SetProperty(ref level1, value);
        }

        private MvxObservableCollection<TaskAssignmentContact> level2;
        public MvxObservableCollection<TaskAssignmentContact> Level2
        {
            get => level2;
            set => SetProperty(ref level2, value);
        }

        private MvxObservableCollection<TaskAssignmentContact> level3;
        public MvxObservableCollection<TaskAssignmentContact> Level3
        {
            get => level3;
            set => SetProperty(ref level3, value);
        }

        public SelectContactsViewModel()
        {
            Level1 = new MvxObservableCollection<TaskAssignmentContact>();
            Level1.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Ashwin" });
            Level1.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Bhadur" });
            Level1.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Chetan" });
            Level1.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Disha" });

            Level2 = new MvxObservableCollection<TaskAssignmentContact>();
            Level2.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Guarav" });
            Level2.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Preet" });

            Level3 = new MvxObservableCollection<TaskAssignmentContact>();
            Level3.Add(new TaskAssignmentContact() { ContactUserId = "919999068531", Name = "Vijay" });
        }

        public IMvxCommand ShowSuccessViewCommand => new MvxCommand(DoShowSuccessView);

        private void DoShowSuccessView()
        {
            NavigationService.Navigate<TaskSuccessViewModel>();
        }

        private bool enableSubmitButton;
        public bool EnableSubmitButton
        {
            get => enableSubmitButton;
            set
            {
                SetProperty(ref enableSubmitButton, value);
            }
        }

        public void ValidateButton(bool isAdded)
        {
            //validate form details
            EnableSubmitButton = isAdded;
        }
    }
}
