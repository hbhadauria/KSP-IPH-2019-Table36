using System;
using MvvmCross.Commands;

namespace IPUnifiedComm.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool isOTPSent;
        public bool IsOTPSent
        {
            get => isOTPSent;
            set
            {
                SetProperty(ref isOTPSent, value);

                try
                {
                    MaskedPhoneNumberText = $"Enter OTP sent on xxxx-xx-{PhoneNumber.Substring(6, 4)}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        private string maskedPhoneNumberText;
        public string MaskedPhoneNumberText
        {
            get => maskedPhoneNumberText;
            set => SetProperty(ref maskedPhoneNumberText, value);
        }

        private string otp;
        public string OTP
        {
            get => otp;
            set => SetProperty(ref otp, value);
        }

        public IMvxCommand ShowMainViewCommand => new MvxCommand(DoShowMainView);

        public LoginViewModel()
        {
        }

        private void DoShowMainView()
        {
            NavigationService.Navigate<MainViewModel>();
        }
    }
}
