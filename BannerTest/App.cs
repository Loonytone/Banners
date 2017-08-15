using System.Collections;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Util;
using System.Linq;

namespace BannerTest
{
	public class App : Application
	{
		public static List<string> images = new List<string>();
		public static List<string> titles = new List<string>();
		public static int H, W;
		public static App app;
		public override void OnCreate()
		{
			base.OnCreate();
			app = this;
			getScreen(this);
			string[] urls = Resources.GetStringArray(Resource.Array.url);
			string[] tips = Resources.GetStringArray(Resource.Array.title);
			images = urls.ToList();
			titles = tips.ToList();
		}
		public virtual void getScreen(Context aty)
		{
			DisplayMetrics dm = aty.Resources.DisplayMetrics;
			H = dm.HeightPixels;
			W = dm.WidthPixels;
		}
	}

}