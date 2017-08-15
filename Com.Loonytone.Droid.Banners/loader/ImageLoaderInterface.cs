using Android.Content;
using Android.Views;

namespace Com.Loonytone.Droid.Banners.loader
{
    public interface ImageLoaderInterface<T> where T : View
	{

		void displayImage(Context context, string path, T imageView);

		T createImageView(Context context);
	}

}