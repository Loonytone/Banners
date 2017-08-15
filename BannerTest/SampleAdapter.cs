
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace BannerTest
{
	public class SampleAdapter : BaseAdapter
	{

		private string[] mDataSet;
		private Context context;

		public SampleAdapter(Context context, string[] dataSet)
		{
			this.mDataSet = dataSet;
			this.context = context;
		}

		public override int Count
		{
			get
			{
				return mDataSet.Length;
			}
		}

		public override Object GetItem(int position)
		{
			return position;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			convertView = View.Inflate(context, Resource.Layout.text_item, null);
			TextView textView = (TextView) convertView.FindViewById(Resource.Id.text);
			textView.Text = mDataSet[position];
			if (position % 2 == 0)
			{
				textView.SetBackgroundColor(Color.ParseColor("#f5f5f5"));
			}
			else
			{
				textView.SetBackgroundColor(Color.White);
			}
			return convertView;
		}

	}

}