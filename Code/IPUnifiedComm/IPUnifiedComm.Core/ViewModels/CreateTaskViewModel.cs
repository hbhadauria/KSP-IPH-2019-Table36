using System;
using IPUnifiedComm.Core.DataEntity;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class CreateTaskViewModel : BaseViewModel
    {
        private MvxObservableCollection<Document> documents;
        public MvxObservableCollection<Document> Documents
        {
            get => documents;
            set => SetProperty(ref documents, value);
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

        private string title;
        public string Title
        {
            get => title;
            set
            {
                SetProperty(ref title, value);
                EnableSubmitButton = ValidateFields();
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                SetProperty(ref description, value);
                EnableSubmitButton = ValidateFields();
            }
        }


        private string importance;
        public string Importance
        {
            get => importance;
            set
            {
                SetProperty(ref importance, value);
                EnableSubmitButton = ValidateFields();
            }
        }

        public CreateTaskViewModel()
        {
            LoadDocuments();
        }

        public void LoadDocuments()
        {
            Documents = new MvxObservableCollection<Document>();
            Documents.Add(new Document { Data = "Default", Extension = "jpeg", Name = "default.jpeg" });
           //TODO: update it later
        }

        public void AddImage(string newImage)
        {
            documents.Add(new Document { Data = newImage, Extension = "jpeg", Name = "default.jpeg" });
            EnableSubmitButton = ValidateFields();
        }

        private bool ValidateFields()
        {
            //validate form details
            bool formValidated = !string.IsNullOrEmpty(Title)
                && !string.IsNullOrEmpty(Description)
                && !string.IsNullOrEmpty(Importance)
                && Documents.Count > 1;
             

            return formValidated;
        }

        public IMvxCommand ShowSelectContactsViewCommand => new MvxCommand(DoShowSelectContactsView);

        private void DoShowSelectContactsView()
        {
            NavigationService.Navigate<SelectContactsViewModel>();
        }
    }
}
