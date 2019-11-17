using System;
using System.Linq;
using IPUnifiedComm.Core.DataEntity;
using IPUnifiedComm.Core.NavParams;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class CreateMessageViewModel : BaseViewModel
    {
        public Action<string> SearchEvent;

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);

                if (!string.IsNullOrEmpty(value))
                {
                    Contacts = new MvxObservableCollection<Contact>(TempContacts.Where(x => x.Name.StartsWith(value, StringComparison.InvariantCultureIgnoreCase)));
                }
                else
                {
                    RefreshContacts();
                }
            }
        }

        private MvxObservableCollection<Contact> contacts;
        public MvxObservableCollection<Contact> Contacts
        {
            get => contacts;
            set => SetProperty(ref contacts, value);
        }

        public MvxObservableCollection<Contact> TempContacts
        {
            get
            {
                return new MvxObservableCollection<Contact>()
                {
                    new Contact { Name = "Aaditya Thakur", Designation = "Sub-Inspector", Location = "Bengaluru" },
                    new Contact { Name = "Aakarshan Sinha", Designation = "SP", Location = "Dakshina Kannada" },
                    new Contact { Name = "Abhinav Thomas", Designation = "CPI", Location = "Chikkaballapura" },
                    new Contact { Name = "Ameya Shrivastava", Designation = "CPI", Location = "Chikkaballapura" },
                    new Contact { Name = "Arjun Mahajan", Designation = "SP", Location = "Dakshina Kannada" },
                    new Contact { Name = "Ashok Mishra", Designation = "Sub-Inspector", Location = "Bengaluru" }
                };
            }
        }

        public IMvxCommand<Contact> ShowNewChatViewCommand => new MvxCommand<Contact>(ShowNewChatView);

        public CreateMessageViewModel()
        {
            Contacts = new MvxObservableCollection<Contact>();
            Contacts.Add(new Contact { Name = "Aaditya Thakur", Designation = "Sub-Inspector", Location = "Bengaluru" });
            Contacts.Add(new Contact { Name = "Aakarshan Sinha", Designation = "SP", Location = "Dakshina Kannada" });
            Contacts.Add(new Contact { Name = "Abhinav Thomas", Designation = "CPI", Location = "Chikkaballapura" });
            Contacts.Add(new Contact { Name = "Ameya Shrivastava", Designation = "CPI", Location = "Chikkaballapura" });
            Contacts.Add(new Contact { Name = "Arjun Mahajan", Designation = "SP", Location = "Dakshina Kannada" });
            Contacts.Add(new Contact { Name = "Ashok Mishra", Designation = "Sub-Inspector", Location = "Bengaluru" });
        }

        public void RefreshContacts()
        {
            Contacts = new MvxObservableCollection<Contact>();
            Contacts.Add(new Contact { Name = "Aaditya Thakur", Designation = "Sub-Inspector", Location = "Bengaluru" });
            Contacts.Add(new Contact { Name = "Aakarshan Sinha", Designation = "SP", Location = "Dakshina Kannada" });
            Contacts.Add(new Contact { Name = "Abhinav Thomas", Designation = "CPI", Location = "Chikkaballapura" });
            Contacts.Add(new Contact { Name = "Ameya Shrivastava", Designation = "CPI", Location = "Chikkaballapura" });
            Contacts.Add(new Contact { Name = "Arjun Mahajan", Designation = "SP", Location = "Dakshina Kannada" });
            Contacts.Add(new Contact { Name = "Ashok Mishra", Designation = "Sub-Inspector", Location = "Bengaluru" });
        }

        private void ShowNewChatView(Contact contact)
        {
            NavigationService.Navigate<NewChatViewModel, ContactNavParam>(new ContactNavParam { Contact = contact });
        }
    }
}
