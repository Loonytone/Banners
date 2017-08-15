using System;

namespace Com.Loonytone.Droid.Banners.listener
{


	/// <summary>
	/// 旧版接口，由于返回的下标是从1开始，下标越界而废弃（因为有人使用所以不能直接删除）
	/// </summary>
	[Obsolete]
	public interface OnBannerClickListener
	{
		void OnBannerClick(int position);
	}

}