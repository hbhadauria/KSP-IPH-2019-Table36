using System;
using IPUnifiedComm.Core.DataEntity;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace IPUnifiedComm.Core.ViewModels
{
    public class ChatHistoryViewModel : BaseViewModel
    {
        private MvxObservableCollection<ChatMessage> chatMessages;
        public MvxObservableCollection<ChatMessage> ChatMessages
        {
            get => chatMessages;
            set => SetProperty(ref chatMessages, value);
        }

        public IMvxCommand ShowCreateMessageViewCommand => new MvxCommand(DoShowCreateMessageView);

        public ChatHistoryViewModel()
        {
            chatMessages = new MvxObservableCollection<ChatMessage>();
            chatMessages.Add(new ChatMessage { SenderName = "Sudhir Mishra", Message = "Understood sir, will make sure th…", TimeAgo = "19:58" });
            chatMessages.Add(new ChatMessage { SenderName = "Insp. Kumar Gaurav", Message = "Thank you for the tip", TimeAgo = "19:58" });
            chatMessages.Add(new ChatMessage { SenderName = "Insp. Shekar Verma", Message = "Not a problem.", TimeAgo = "19:28" });
            chatMessages.Add(new ChatMessage { SenderName = "DSP Arvind Singh", Message = "Wife and kids safe Sir. Tremors did...", TimeAgo = "10 Nov" });
            chatMessages.Add(new ChatMessage { SenderName = "Sudhir Mishra", Message = "It's the same problem over and ov...", TimeAgo = "09 Nov" });
            chatMessages.Add(new ChatMessage { SenderName = "Sudhir Mishra", Message = "Wife and kids safe Sir. Tremors did...", TimeAgo = "27 Oct" });
        }

        private void DoShowCreateMessageView()
        {
            NavigationService.Navigate<CreateMessageViewModel>();
        }
    }
}
