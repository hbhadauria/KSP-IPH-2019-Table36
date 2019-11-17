using IPUnifiedComm.Core.DataEntity;
using IPUnifiedComm.Core.NavParams;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class NewChatViewModel : BaseViewModel<ContactNavParam>
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        private MvxObservableCollection<ChatMessage> messages;
        public MvxObservableCollection<ChatMessage> Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public NewChatViewModel()
        {
            messages = new MvxObservableCollection<ChatMessage>
            {
                new ChatMessage{ SenderId="+919999068539",Message="Hello Sir, We wanted to set up expectations for the next drill. Kindly let me know how to go about it.Thanks"},
                new ChatMessage{ SenderId="+918839601297",Message="Carry on as planned."},
                new ChatMessage{ SenderId="+919999068539",Message="Sure sir sounds good."}
            };
        }

        protected override void DoPrepare(ContactNavParam parameter)
        {
            base.DoPrepare(parameter);
            Name = parameter.Contact.Name;
            Description = parameter.Contact.Description;
            PhoneNumber = string.IsNullOrEmpty(parameter.Contact.PhoneNumber) ? "+918839601297" : parameter.Contact.PhoneNumber;
        }
    }
}
