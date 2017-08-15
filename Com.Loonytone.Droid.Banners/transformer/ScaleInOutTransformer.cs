
using Android.Views;

namespace Com.Loonytone.Droid.Banners.transformer
{
	public class ScaleInOutTransformer : ABaseTransformer
	{

		protected override void onTransform(View view, float position)
		{
			view.PivotX = position < 0 ? 0 : view.Width;
			view.PivotY = view.Height / 2f;
			float scale = position < 0 ? 1f + position : 1f - position;
			view.ScaleX = scale;
			view.ScaleY = scale;
		}

	}

}