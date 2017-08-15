
using System.Collections;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Com.Loonytone.Droid.Banners.listener;
using Com.Loonytone.Droid.Banners;
using Android.Content;
using Android.Views;


namespace BannerTest
{
    [Activity(Label = "BannerTest", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, SwipeRefreshLayout.IOnRefreshListener, IOnBannerListener
    {
        internal const int REFRESH_COMPLETE = 0X1112;
        internal SwipeRefreshLayout mSwipeLayout;
        internal ListView listView;
        internal Banner banner;

        private Handler mHandler;

        private class HandlerAnonymousInnerClass : Handler
        {
            MainActivity outerInstance;
            public HandlerAnonymousInnerClass(MainActivity outer)
            {
                outerInstance = outer;
            }

            public virtual void handleMessage(Message msg)
            {
                switch (msg.What)
                {
                    case REFRESH_COMPLETE:
                        string[] urls = outerInstance.Resources.GetStringArray(Resource.Array.url4);
                        outerInstance.banner.Update(urls.ToList());
                        outerInstance.mSwipeLayout.Refreshing = false;
                        break;
                }
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            mSwipeLayout = (SwipeRefreshLayout)FindViewById(Resource.Id.swipe);
            mSwipeLayout.SetOnRefreshListener(this);
            listView = (ListView)FindViewById(Resource.Id.list);
            View header = LayoutInflater.From(this).Inflate(Resource.Layout.header, null);
            banner = (Banner)header.FindViewById(Resource.Id.banner);
            banner.LayoutParameters = new AbsListView.LayoutParams(ViewGroup.LayoutParams.MatchParent, 360);
            listView.AddHeaderView(banner);
            mHandler = new HandlerAnonymousInnerClass(this);

            string[] data = Resources.GetStringArray(Resource.Array.demo_list);
            listView.Adapter = new SampleAdapter(this, data);
            //简单使用
            string[] urls = Resources.GetStringArray(Resource.Array.url);
            string[] tips = Resources.GetStringArray(Resource.Array.title);
            banner.SetBannerTitles(tips.ToList()).SetImages(urls.ToList()).SetImageLoader(new GlideImageLoader()).SetOnBannerListener(this).Start();

        }

        public void OnBannerClick(int position)
        {
            Toast.MakeText(ApplicationContext, "你点击了：" + position, ToastLength.Short).Show();
        }


        //如果你需要考虑更好的体验，可以这么操作
        protected override void OnStart()
        {
            base.OnStart();
            //开始轮播
            banner.StartAutoPlay();
        }

        protected override void OnStop()
        {
            base.OnStop();
            //结束轮播
            banner.StopAutoPlay();
        }


        public void OnRefresh()
        {
            mHandler.SendEmptyMessageDelayed(REFRESH_COMPLETE, 2000);
        }
    }
}

