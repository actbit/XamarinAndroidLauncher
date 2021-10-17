using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App6
{
    class AppInfoList
    {

        internal List<AppInfo> create(Context context)
        {
            List<AppInfo> appInfos = new List<AppInfo>();
            var pm = context.PackageManager;
            var intent = new Intent(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryLauncher);

            IList<ResolveInfo> allApps = pm.QueryIntentActivities(intent, 0);
            foreach (ResolveInfo ri in allApps)
            {
                AppInfo app = new AppInfo();
                app.label = ri.LoadLabel(pm);
                //app.componentName = new ComponentName(ri.ActivityInfo.PackageName, ri.ActivityInfo.Name);
                app.icon = ri.ActivityInfo.LoadIcon(pm);
                appInfos.Add(app);
            }
            return appInfos;
        }
    }
}