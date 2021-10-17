using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;

namespace App6
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar.FullScreen", MainLauncher = true)]

    [IntentFilter(new string[] { "android.intent.action.MAIN"},Categories = new string[] { "android.intent.category.HOME", "android.intent.category.LAUNCHER" , "android.intent.category.DEFAULT" , "android.intent.category.LAUNCHER_APP", "android.intent.category.MONKEY" })]
    
    public class MainActivity : AppCompatActivity, View.IOnApplyWindowInsetsListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //this.Window.SetFlags(WindowManagerFlags.Fullscreen,WindowManagerFlags.Fullscreen);
            View view = this.Window.DecorView;
            var d = view.SystemGestureExclusionRects ;
            //Intent launchIntent = PackageManager.GetLaunchIntentForPackage("com.android.chrome");
            //StartActivity(launchIntent);
            //ImageView chromeIcon = (ImageView)FindViewById(Resource.Id.chromeButton);
            //chromeIcon.SetImageDrawable(getActivityIcon(this, "com.android.chrome", "com.google.android.apps.chrome.Main"));
            //chromeIcon.Click += ChromeIcon_Click;
            view.SystemUiVisibility =StatusBarVisibility.Hidden;
            
            RAdapter rAdapter = new RAdapter(this);
            var sd = (RecyclerView)FindViewById(Resource.Id.recyclerView1);
            sd.SetAdapter(rAdapter);
            sd.SetLayoutManager(new LinearLayoutManager(this));

        }

        private void ChromeIcon_Click(object sender, EventArgs e)
        {
            Intent launchIntent = PackageManager.GetLaunchIntentForPackage("com.android.chrome");
            StartActivity(launchIntent);
        }

        public void onChromeButtonClick(View v)
        {

        }
        public static Drawable getActivityIcon(Context context, string packageName, string activityName)
        {
            PackageManager pm = context.PackageManager;
            Intent intent = new Intent();
            intent.SetComponent(new ComponentName(packageName, activityName));
            ResolveInfo resolveInfo = pm.ResolveActivity(intent, 0);

            return resolveInfo.LoadIcon(pm);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void Finish()
        {
            
        }
        List<Rect> exclusionRects;

        public void onLayout(
                bool changedCanvas, int left, int top, int right, int bottom)
        {
            // Update rect bounds and the exclusionRects list
            this.Window.DecorView.SystemGestureExclusionRects=(exclusionRects);
        }

        public void onDraw(Canvas canvas)
        {
            // Update rect bounds and the exclusionRects list
            this.Window.DecorView.SystemGestureExclusionRects = exclusionRects;
        }

        public WindowInsets OnApplyWindowInsets(View v, WindowInsets insets)
        {
            return insets.ConsumeSystemWindowInsets();
        }
    }
}