
using Android.Content;
using Android.Net;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Loonytone.Droid.Banners.loader;
using Object = Java.Lang.Object;


namespace BannerTest
{    
	public class GlideImageLoader : ImageLoader
	{
		public override void displayImage(Context context, string path, ImageView imageView)
		{
			//具体方法内容自己去选择，次方法是为了减少banner过多的依赖第三方包，所以将这个权限开放给使用者去选择
			Glide.With(context.ApplicationContext).Load(path).CrossFade().Into(imageView);
		}

	}

}