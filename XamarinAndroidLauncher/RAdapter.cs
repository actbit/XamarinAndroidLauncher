using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace App6
{
    class RAdapter : RecyclerView.Adapter
    {
        string[] items;
        private List<AppInfo> appsList;
        public RAdapter(Context c)
        {

            //This is where we build our list of app details, using the app 
            //object we created to store the label, package name and icon

            PackageManager pm = c.PackageManager;
            appsList = new List<AppInfo>();

            Intent i = new Intent(Intent.ActionMain, null);
            i.AddCategory(Intent.CategoryLauncher);

            IList<ResolveInfo> allApps = pm.QueryIntentActivities(i, 0);
            foreach (ResolveInfo ri in allApps)
            {
                AppInfo app = new AppInfo();
                app.label = ri.LoadLabel(pm);
                app.packageName = ri.ActivityInfo.PackageName;
                app.icon = ri.ActivityInfo.LoadIcon(pm);
                appsList.Add(app);
            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int i)
        {
            var viewHolder = holder as RAdapter.ViewHolder;
            //Here we use the information in the list we created to define the views 

            String appLabel = appsList[i].label.ToString();
            String appPackage = appsList[i].packageName.ToString();
            Drawable appIcon = appsList[i].icon;

            TextView textView = viewHolder.textView;
            textView.Text = appLabel;
            ImageView imageView = viewHolder.img;
            imageView.SetImageDrawable(appIcon);
        }


        public override int ItemCount
        {
            get { return appsList.Count; }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //This is what adds the code we've written in here to our target view
            LayoutInflater inflater = LayoutInflater.From(parent.Context);

            View view = inflater.Inflate(Resource.Layout.li_application, parent, false);

            ViewHolder viewHolder = new RAdapter.ViewHolder(view,appsList);
            return viewHolder;
        }
        // Create new views (invoked by the layout manager)
        

        public class ViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {
            public TextView textView;
            public ImageView img;


            //This is the subclass ViewHolder which simply 
            //'holds the views' for us to show on each row
            //public ViewHolder(View itemView) : base(itemView)
            //{


            //    //Finds the views from our row.xml
            //    textView = (TextView)itemView.FindViewById(Resource.Id.label);
            //    img = (ImageView)itemView.FindViewById(Resource.Id.icon);
            //    itemView.SetOnClickListener(this);
            //}
            List<AppInfo> appsList;
            public ViewHolder(View itemView, List<AppInfo> appl) : base(itemView)
            {
                appsList = appl;

                //Finds the views from our row.xml
                textView = (TextView)itemView.FindViewById(Resource.Id.label);
                img = (ImageView)itemView.FindViewById(Resource.Id.icon);
                itemView.SetOnClickListener(this);
            }
            public void OnClick(View v)
            {
                int pos = AdapterPosition;
                Context context = v.Context;

                Intent launchIntent = context.PackageManager.GetLaunchIntentForPackage(appsList[pos].packageName.ToString());
                context.StartActivity(launchIntent);
                Toast.MakeText(v.Context, appsList[pos].label.ToString(), ToastLength.Long).Show();

            }

        }

    }

}