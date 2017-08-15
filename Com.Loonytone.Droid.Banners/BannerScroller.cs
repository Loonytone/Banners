
using Android.Content;
using Android.Views.Animations;
using Android.Widget;

namespace Com.Loonytone.Droid.Banners
{

	public class BannerScroller : Scroller
	{
		private int mDuration = BannerConfig.DURATION;

		public BannerScroller(Context context) : base(context)
		{
		}

		public BannerScroller(Context context, IInterpolator interpolator) : base(context, interpolator)
		{
		}

		public BannerScroller(Context context, IInterpolator interpolator, bool flywheel) : base(context, interpolator, flywheel)
		{
		}

		public override void StartScroll(int startX, int startY, int dx, int dy, int duration)
		{
			base.StartScroll(startX, startY, dx, dy, mDuration);
		}

		public override void StartScroll(int startX, int startY, int dx, int dy)
		{
			base.StartScroll(startX, startY, dx, dy, mDuration);
		}

		public void SetDuration(int value)
        {
            mDuration = value;
        }

	}

}