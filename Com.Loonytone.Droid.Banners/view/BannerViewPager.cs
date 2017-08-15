

using Android.Content;
using ViewPager = Android.Support.V4.View.ViewPager;
using Android.Util;
using Android.Views;

namespace Com.Loonytone.Droid.Banners.view
{

	public class BannerViewPager : ViewPager
	{
		private bool scrollable = true;

		public BannerViewPager(Context context) : base(context)
		{
		}

		public BannerViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}


		public override bool OnTouchEvent(MotionEvent ev)
		{
			return this.scrollable && base.OnTouchEvent(ev);
		}

		public override bool OnInterceptTouchEvent(MotionEvent ev)
		{
			return this.scrollable && base.OnInterceptTouchEvent(ev);
		}

		public virtual bool Scrollable
		{
			set
			{
				this.scrollable = value;
			}
		}
	}

}