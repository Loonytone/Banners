
using Android.Content;
using Android.Widget;
using Object = Java.Lang.Object;

namespace Com.Loonytone.Droid.Banners.loader
{
	public abstract class ImageLoader :Object, ImageLoaderInterface<ImageView>
	{
		public abstract void displayImage(Context context, string path, ImageView imageView);

		public virtual ImageView createImageView(Context context)
		{
            ImageView imageView = new ImageView(context);
			return imageView;
		}

	}

}