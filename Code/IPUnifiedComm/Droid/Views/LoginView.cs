using System;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using IPUnifiedComm.Core.ViewModels;
using Java.Util.Concurrent;

namespace IPUnifiedComm.Droid.Views
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : BaseActivity<LoginViewModel>
    {
        private Button btnProceed, btnLogin;
        private string verificationId;

        public LoginView() : base(Resource.Layout.LoginView)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            if (IsPlayServicesAvailable())
            {
                FirebaseApp.InitializeApp(this);
            }

            btnProceed = FindViewById<Button>(Resource.Id.btnProceed);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnProceed.Click += OnProceedButtonClick;
            btnLogin.Click += OnLoginButtonClick;
        }

        private void OnProceedButtonClick(object sender, EventArgs e)
        {
            var instance = FirebaseAuth.Instance;

            var callbacks = new MyVerificationStateChangedCallbacks(OnVerificationCompleted, OnVerificationFailed, OnCodeSent);

            PhoneAuthProvider.Instance.VerifyPhoneNumber(
                $"+91{ViewModel.PhoneNumber}",
                60,
                TimeUnit.Seconds,
                this,
                callbacks);
        }

        private void OnLoginButtonClick(object sender, EventArgs e)
        {
            // [START verify_with_code]
            var credential = PhoneAuthProvider.GetCredential(verificationId, ViewModel.OTP);
            // [END verify_with_code]
            SignInWithPhoneAuthCredential(credential);
        }

        private void OnVerificationCompleted(PhoneAuthCredential authCredential)
        {
            SignInWithPhoneAuthCredential(authCredential);
        }

        private void OnVerificationFailed()
        {

        }

        private void OnCodeSent(string storedVerificationId, PhoneAuthProvider.ForceResendingToken resendToken)
        {
            ViewModel.IsOTPSent = true;
            verificationId = storedVerificationId;
        }

        private void SignInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            FirebaseAuth.Instance.SignInWithCredential(credential);
            ViewModel.IsOTPSent = false;
            ViewModel.ShowMainViewCommand.Execute();
        }

        public class MyVerificationStateChangedCallbacks : PhoneAuthProvider.OnVerificationStateChangedCallbacks
        {
            private readonly string TAG = "MyVerificationStateChangedCallbacks";
            private bool verificationInProgress = false;
            private string storedVerificationId = "";
            private PhoneAuthProvider.ForceResendingToken resendToken;
            private Action<PhoneAuthCredential> onVerificationCompleted;
            private Action onVerificationFailed;
            private Action<string, PhoneAuthProvider.ForceResendingToken> onCodeSent;

            public MyVerificationStateChangedCallbacks(
                Action<PhoneAuthCredential> onVerificationCompleted = null,
                Action onVerificationFailed = null,
                Action<string, PhoneAuthProvider.ForceResendingToken> onCodeSent = null)
            {
                this.onVerificationCompleted = onVerificationCompleted;
                this.onVerificationFailed = onVerificationFailed;
                this.onCodeSent = onCodeSent;
            }

            public override void OnVerificationCompleted(PhoneAuthCredential p0)
            {
                // This callback will be invoked in two situations:
                // 1 - Instant verification. In some cases the phone number can be instantly
                //     verified without needing to send or enter a verification code.
                // 2 - Auto-retrieval. On some devices Google Play services can automatically
                //     detect the incoming verification SMS and perform verification without
                //     user action.
                Log.Debug(TAG, "onVerificationCompleted:$credential");
                // [START_EXCLUDE silent]
                verificationInProgress = false;
                // [END_EXCLUDE]

                // [START_EXCLUDE silent]
                // Update the UI and attempt sign in with the phone credential
                onVerificationCompleted?.Invoke(p0);
                // [END_EXCLUDE]
            }

            public override void OnVerificationFailed(FirebaseException p0)
            {
                // This callback is invoked in an invalid request for verification is made,
                // for instance if the the phone number format is not valid.
                Log.Debug(TAG, "onVerificationFailed", p0);
                // [START_EXCLUDE silent]
                verificationInProgress = false;
                // [END_EXCLUDE]

                if (p0 is FirebaseAuthInvalidCredentialsException)
                {
                    // Invalid request
                    // [START_EXCLUDE]
                    //fieldPhoneNumber.error = "Invalid phone number.";
                    // [END_EXCLUDE]
                }
                else if (p0 is FirebaseTooManyRequestsException)
                {
                    // The SMS quota for the project has been exceeded
                    // [START_EXCLUDE]
                    Toast.MakeText(Application.Context, "Quota exceeded.", ToastLength.Long);
                    // [END_EXCLUDE]
                }

                // Show a message and update the UI
                // [START_EXCLUDE]
                onVerificationFailed?.Invoke();
                // [END_EXCLUDE]
            }

            public override void OnCodeSent(string p0, PhoneAuthProvider.ForceResendingToken p1)
            {
                // The SMS verification code has been sent to the provided phone number, we
                // now need to ask the user to enter the code and then construct a credential
                // by combining the code with a verification ID.
                Log.Debug(TAG, "onCodeSent:$verificationId");

                // Save verification ID and resending token so we can use them later
                storedVerificationId = p0;
                resendToken = p1;

                onCodeSent?.Invoke(storedVerificationId, resendToken);
                // [START_EXCLUDE]
                // Update UI
                //updateUI(STATE_CODE_SENT);
                // [END_EXCLUDE]
            }
        }

        public bool IsPlayServicesAvailable()
        {
            string message = "";
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    message = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    message = "This device is not supported";
                }
                return false;
            }
            else
            {
                message = "Google Play Services is available.";
                return true;
            }
        }
    }
}
