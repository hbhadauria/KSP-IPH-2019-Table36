using System;
using Android.App;
using Firebase.Messaging;

namespace IPUnifiedComm.Droid.Services
{
    [Service(Name = "com.deloitte.ipunifiedcomm.MyFirebaseMessagingService")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public MyFirebaseMessagingService()
        {
        }

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
        }
    }
}
