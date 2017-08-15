using System;
using System.Collections.Generic;

using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using Java.Lang.Reflect;
using PagerAdapter = Android.Support.V4.View.PagerAdapter;
using ViewPager = Android.Support.V4.View.ViewPager;
using IOnPageChangeListener = Android.Support.V4.View.ViewPager.IOnPageChangeListener;
using IPageTransformer = Android.Support.V4.View.ViewPager.IPageTransformer;
using Android.Widget;
using Com.Loonytone.Droid.Banners.listener;
using Com.Loonytone.Droid.Banners.loader;
using Com.Loonytone.Droid.Banners.view;
using Android.Graphics;
using Object = Java.Lang.Object;
using Java.Lang;

namespace Com.Loonytone.Droid.Banners
{

    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //	import static Android.Support.V4.View.ViewPager.IOnPageChangeListener;
    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //	import static Android.Support.V4.View.ViewPager.IPageTransformer;

    public class Banner : FrameLayout, ViewPager.IOnPageChangeListener
    {
		public string tag = "banner";
		private int mIndicatorMargin = BannerConfig.PADDING_SIZE;
		private int mIndicatorWidth;
		private int mIndicatorHeight;
		private int indicatorSize;
		private int bannerStyle = BannerConfig.CIRCLE_INDICATOR;
		private int delayTime = BannerConfig.TIME;
		private int scrollTime = BannerConfig.DURATION;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		private bool isAutoPlay = BannerConfig.IS_AUTO_PLAY;
		private bool isScroll = BannerConfig.IS_SCROLL;
		private int mIndicatorSelectedResId = Resource.Drawable.gray_radius;
		private int mIndicatorUnselectedResId = Resource.Drawable.white_radius;
		private int mLayoutResId = Resource.Layout.banner;
		private int titleHeight;
		private int titleBackground;
		private int titleTextColor;
		private int titleTextSize;
		private int count = 0;
		private int currentItem;
		private GravityFlags gravity = GravityFlags.NoGravity;
		private int lastPosition = 1;
		private int scaleType = 1;
		private List<string> titles;
		private List<string> imageUrls;
		private List<View> imageViews;
		private List<ImageView> indicatorImages;
		private Context context;
		private BannerViewPager viewPager;
		private TextView bannerTitle, numIndicatorInside, numIndicator;
		private LinearLayout indicator, indicatorInside, titleView;
		private ImageLoader imageLoader;
		private BannerPagerAdapter adapter;
		private IOnPageChangeListener mOnPageChangeListener;
		private BannerScroller mScroller;
		private OnBannerClickListener bannerListener;
		private IOnBannerListener listener;
		private DisplayMetrics dm;

        private readonly Runnable task;


        private WeakHandler handler = new WeakHandler();

		public Banner(Context context) : this(context, null)
		{
		}

		public Banner(Context context, IAttributeSet attrs) : this(context, attrs, 0)
		{
		}

		public Banner(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			this.context = context;
			titles = new List<string>();
			imageUrls = new List<string>();
			imageViews = new List<View>();
			indicatorImages = new List<ImageView>();
			dm = context.Resources.DisplayMetrics;
			indicatorSize = dm.WidthPixels / 80;
			InitView(context, attrs);
            task =new Runnable( delegate {
                if (count > 1 && isAutoPlay)
                {
                    currentItem = currentItem % (count + 1) + 1;
                    //                Log.i(tag, "curr:" + currentItem + " count:" + count);
                    if (currentItem == 1)
                    {
                        viewPager.SetCurrentItem(currentItem, false);
                        handler.post(task);
                    }
                    else
                    {
                        viewPager.CurrentItem =currentItem;
                        handler.PostDelayed(task, delayTime);
                    }
                }
            });

        }

		private void InitView(Context context, IAttributeSet attrs)
		{
			imageViews.Clear();
			HandleTypedArray(context, attrs);
			View view = LayoutInflater.From(context).Inflate(mLayoutResId, this, true);
			viewPager = (BannerViewPager) view.FindViewById(Resource.Id.bannerViewPager);
			titleView = (LinearLayout) view.FindViewById(Resource.Id.titleView);
			indicator = (LinearLayout) view.FindViewById(Resource.Id.circleIndicator);
			indicatorInside = (LinearLayout) view.FindViewById(Resource.Id.indicatorInside);
			bannerTitle = (TextView) view.FindViewById(Resource.Id.bannerTitle);
			numIndicator = (TextView) view.FindViewById(Resource.Id.numIndicator);
			numIndicatorInside = (TextView) view.FindViewById(Resource.Id.numIndicatorInside);
			InitViewPagerScroll();
		}

		private void HandleTypedArray(Context context, IAttributeSet attrs)
		{
			if (attrs == null)
			{
				return;
			}
			TypedArray typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.Banner);
			mIndicatorWidth = typedArray.GetDimensionPixelSize(Resource.Styleable.Banner_indicator_width, indicatorSize);
			mIndicatorHeight = typedArray.GetDimensionPixelSize(Resource.Styleable.Banner_indicator_height, indicatorSize);
			mIndicatorMargin = typedArray.GetDimensionPixelSize(Resource.Styleable.Banner_indicator_margin, BannerConfig.PADDING_SIZE);
			mIndicatorSelectedResId = typedArray.GetResourceId(Resource.Styleable.Banner_indicator_drawable_selected, Resource.Drawable.gray_radius);
			mIndicatorUnselectedResId = typedArray.GetResourceId(Resource.Styleable.Banner_indicator_drawable_unselected, Resource.Drawable.white_radius);
			scaleType = typedArray.GetInt(Resource.Styleable.Banner_image_scale_type, scaleType);
			delayTime = typedArray.GetInt(Resource.Styleable.Banner_delay_time, BannerConfig.TIME);
			scrollTime = typedArray.GetInt(Resource.Styleable.Banner_scroll_time, BannerConfig.DURATION);
			isAutoPlay = typedArray.GetBoolean(Resource.Styleable.Banner_is_auto_play, BannerConfig.IS_AUTO_PLAY);
			titleBackground = typedArray.GetColor(Resource.Styleable.Banner_title_background, BannerConfig.TITLE_BACKGROUND);
			titleHeight = typedArray.GetDimensionPixelSize(Resource.Styleable.Banner_title_height, BannerConfig.TITLE_HEIGHT);
			titleTextColor = typedArray.GetColor(Resource.Styleable.Banner_title_textcolor, BannerConfig.TITLE_TEXT_COLOR);
			titleTextSize = typedArray.GetDimensionPixelSize(Resource.Styleable.Banner_title_textsize, BannerConfig.TITLE_TEXT_SIZE);
			mLayoutResId = typedArray.GetResourceId(Resource.Styleable.Banner_layout_id, mLayoutResId);
			typedArray.Recycle();
		}

		private void InitViewPagerScroll()
		{
			try
			{
				Field mField = new ViewPager(Context).Class.GetDeclaredField("mScroller");
				mField.Accessible = true;
				mScroller = new BannerScroller(viewPager.Context);
				mScroller.SetDuration(scrollTime);
				mField.Set(viewPager, mScroller);
			}
			catch (System.Exception e)
			{
				Log.Error(tag, e.Message);
			}
		}


		public virtual Banner SetAutoPlay(bool isAutoPlay)
		{
			this.isAutoPlay= isAutoPlay;
			return this;
		}

		public virtual Banner SetImageLoader(ImageLoader imageLoader)
		{
			this.imageLoader = imageLoader;
			return this;
		}

		public virtual Banner SetDelayTime(int delayTime)
		{
			this.delayTime = delayTime;
			return this;
		}

		public virtual Banner SetIndicatorGravity(int type)
		{
			switch (type)
			{
				case BannerConfig.LEFT:
					this.gravity = GravityFlags.Left | GravityFlags.CenterVertical;
					break;
				case BannerConfig.CENTER:
					this.gravity = GravityFlags.Center;
					break;
				case BannerConfig.RIGHT:
					this.gravity = GravityFlags.Right | GravityFlags.CenterVertical;
					break;
			}
			return this;
		}

		public virtual Banner SetBannerAnimation(Java.Lang.Class transformer)
		{
			try
			{
				SetPageTransformer(true, (IPageTransformer)transformer.NewInstance());
			}
			catch (System.Exception e)
			{
				Log.Error(tag, "Please set the IPageTransformer class");
			}
			return this;
		}

		/// <summary>
		/// Set the number of pages that should be retained to either side of the
		/// current page in the view hierarchy in an idle state. Pages beyond this
		/// limit will be recreated From the adapter when needed.
		/// </summary>
		/// <param name="limit"> How many pages will be kept offscreen in an idle state. </param>
		/// <returns> Banner </returns>
		public virtual Banner SetOffscreenPageLimit(int limit)
		{
			if (viewPager != null)
			{
				viewPager.OffscreenPageLimit = limit;
			}
			return this;
		}

		/// <summary>
		/// Set a <seealso cref="IPageTransformer"/> that will be called for each attached page whenever
		/// the scroll position is changed. This allows the application to apply custom property
		/// transformations to each page, overriding the default sliding look and feel.
		/// </summary>
		/// <param name="reverseDrawingOrder"> true if the supplied IPageTransformer requires page views
		///                            to be drawn From last to first instead of first to last. </param>
		/// <param name="transformer">         IPageTransformer that will modify each page's animation properties </param>
		/// <returns> Banner </returns>
		public virtual Banner SetPageTransformer(bool reverseDrawingOrder, IPageTransformer transformer)
		{
			viewPager.SetPageTransformer(reverseDrawingOrder, transformer);
			return this;
		}

		public virtual Banner SetBannerTitles(List<string> titles)
		{
			this.titles = titles;
			return this;
		}

		public virtual Banner SetBannerStyle(int bannerStyle)
		{
			this.bannerStyle = bannerStyle;
			return this;
		}

		public virtual Banner SetViewPagerIsScroll(bool isScroll)
		{
			this.isScroll = isScroll;
			return this;
		}

		public virtual Banner SetImages(List<string> imageUrls)
		{
			this.imageUrls = imageUrls;
			this.count = imageUrls.Count;
			return this;
		}

		public virtual void Update(List<string> imageUrls, List<string> titles)
		{
			this.imageUrls.Clear();
			this.titles.Clear();
			this.imageUrls.AddRange(imageUrls);
			((List<string>)this.titles).AddRange(titles);
			this.count = this.imageUrls.Count;
			Start();
		}

		public virtual void Update(List<string> imageUrls)
		{
			this.imageUrls.Clear();
			this.imageUrls.AddRange(imageUrls);
			this.count = this.imageUrls.Count;
			Start();
		}

		public virtual void UpdateBannerStyle(int bannerStyle)
		{
			indicator.Visibility = ViewStates.Gone;
			numIndicator.Visibility = ViewStates.Gone;
			numIndicatorInside.Visibility = ViewStates.Gone;
			indicatorInside.Visibility = ViewStates.Gone;
			bannerTitle.Visibility = ViewStates.Gone;
			titleView.Visibility = ViewStates.Gone;
			this.bannerStyle = bannerStyle;
			Start();
		}

		public virtual Banner Start()
		{
			SetBannerStyleUI();
			SetImageList(imageUrls);
			SetData();
			return this;
		}

		private void SetTitleStyleUI()
		{
			if (titles.Count != imageUrls.Count)
			{
				throw new System.Exception("[Banner] --> The number of titles and images is different");
			}
			if (titleBackground != -1)
			{
				titleView.SetBackgroundColor(new Color(titleBackground));
			}
			if (titleHeight != -1)
			{
				titleView.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, titleHeight);
			}
			if (titleTextColor != -1)
			{
				bannerTitle.SetTextColor(new Color(titleTextColor));
			}
			if (titleTextSize != -1)
			{
				bannerTitle.SetTextSize(ComplexUnitType.Px, titleTextSize);
			}
			if (titles != null && titles.Count > 0)
			{
				bannerTitle.Text = titles[0];
				bannerTitle.Visibility = ViewStates.Visible;
				titleView.Visibility = ViewStates.Visible;
			}
		}

		private void SetBannerStyleUI()
		{
            ViewStates visibility;
			if (count > 1)
			{
				visibility = ViewStates.Visible;
			}
			else
			{
				visibility = ViewStates.Gone;
			}
			switch (bannerStyle)
			{
				case BannerConfig.CIRCLE_INDICATOR:
					indicator.Visibility = visibility;
					break;
				case BannerConfig.NUM_INDICATOR:
					numIndicator.Visibility = visibility;
					break;
				case BannerConfig.NUM_INDICATOR_TITLE:
					numIndicatorInside.Visibility = visibility;
					SetTitleStyleUI();
					break;
				case BannerConfig.CIRCLE_INDICATOR_TITLE:
					indicator.Visibility = visibility;
					SetTitleStyleUI();
					break;
				case BannerConfig.CIRCLE_INDICATOR_TITLE_INSIDE:
					indicatorInside.Visibility = visibility;
					SetTitleStyleUI();
					break;
			}
		}

		private void InitImages()
		{
			imageViews.Clear();
			if (bannerStyle == BannerConfig.CIRCLE_INDICATOR || bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE || bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE_INSIDE)
			{
				CreateIndicator();
			}
			else if (bannerStyle == BannerConfig.NUM_INDICATOR_TITLE)
			{
				numIndicatorInside.Text = "1/" + count;
			}
			else if (bannerStyle == BannerConfig.NUM_INDICATOR)
			{
				numIndicator.Text = "1/" + count;
			}
		}

		private void SetImageList(List<string> value)
		{
            if (value == null || value.Count <= 0)
            {
                Log.Error(tag, "Please set the images data.");
                return;
            }
            InitImages();
            for (int i = 0; i <= count + 1; i++)
            {
                ImageView imageView = null;
                if (imageLoader != null)
                {
                    imageView = imageLoader.createImageView(context);
                }
                if (imageView == null)
                {
                    imageView = new ImageView(context);
                }
                ScaleType = imageView;
                string url = null;
                if (i == 0)
                {
                    url = value[count - 1];
                }
                else if (i == count + 1)
                {
                    url = value[0];
                }
                else
                {
                    url = value[i - 1];
                }
                imageViews.Add(imageView);
                if (imageLoader != null)
                {
                    imageLoader.displayImage(context, url, imageView);
                }
                else
                {
                    Log.Error(tag, "Please set images loader.");
                }
            }
        }

		private ImageView ScaleType
		{
			set
			{
				if (value is ImageView)
				{
					ImageView view = value;
					switch (scaleType)
					{
						case 0:
							view.SetScaleType(ImageView.ScaleType.Center);
							break;
						case 1:
							view.SetScaleType(ImageView.ScaleType.CenterCrop);
							break;
						case 2:
							view.SetScaleType(ImageView.ScaleType.CenterInside);
							break;
						case 3:
							view.SetScaleType(ImageView.ScaleType.FitCenter);
							break;
						case 4:
							view.SetScaleType(ImageView.ScaleType.FitEnd);
							break;
						case 5:
							view.SetScaleType(ImageView.ScaleType.FitStart);
							break;
						case 6:
							view.SetScaleType(ImageView.ScaleType.FitXy);
							break;
						case 7:
							view.SetScaleType(ImageView.ScaleType.Matrix);
							break;
					}
    
				}
			}
		}

		private void CreateIndicator()
		{
			indicatorImages.Clear();
			indicator.RemoveAllViews();
			indicatorInside.RemoveAllViews();
			for (int i = 0; i < count; i++)
			{
				ImageView imageView = new ImageView(context);
				imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                LinearLayout.LayoutParams @params = new LinearLayout.LayoutParams(mIndicatorWidth, mIndicatorHeight)
                {
                    LeftMargin = mIndicatorMargin,
                    RightMargin = mIndicatorMargin
                };
                if (i == 0)
				{
					imageView.SetImageResource(mIndicatorSelectedResId);
				}
				else
				{
					imageView.SetImageResource(mIndicatorUnselectedResId);
				}
				indicatorImages.Add(imageView);
				if (bannerStyle == BannerConfig.CIRCLE_INDICATOR || bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE)
				{
					indicator.AddView(imageView, @params);
				}
				else if (bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE_INSIDE)
				{
					indicatorInside.AddView(imageView, @params);
				}
			}
		}


		private void SetData()
		{
			currentItem = 1;
			if (adapter == null)
			{
				adapter = new BannerPagerAdapter(this);
				viewPager.AddOnPageChangeListener(this);
			}
			viewPager.Adapter = adapter;
			viewPager.Focusable = true;
			viewPager.CurrentItem = 1;
			if (gravity != GravityFlags.NoGravity)
			{
				indicator.SetGravity(gravity);
			}
			if (isScroll && count > 1)
			{
				viewPager.Scrollable = true;
			}
			else
			{
				viewPager.Scrollable = false;
			}
			if (isAutoPlay)
			{
				StartAutoPlay();
			}
		}


		public virtual void StartAutoPlay()
		{
			handler.RemoveCallbacks(task);
			handler.PostDelayed(task, delayTime);
		}

		public virtual void StopAutoPlay()
		{
			handler.RemoveCallbacks(task);
		}

        //private readonly ThreadStart task = new RunnableAnonymousInnerClass();


        private class RunnableAnonymousInnerClass : Java.Lang.Object, IRunnable
		{
            Banner outerInstance;

            public RunnableAnonymousInnerClass(Banner outer)
			{
                outerInstance = outer;
            }

			public virtual void Run()
			{
				if (outerInstance.count > 1 && outerInstance.isAutoPlay)
				{
					outerInstance.currentItem = outerInstance.currentItem % (outerInstance.count + 1) + 1;
		//                Log.i(tag, "curr:" + currentItem + " count:" + count);
					if (outerInstance.currentItem == 1)
					{
						outerInstance.viewPager.SetCurrentItem(outerInstance.currentItem, false);
                        outerInstance.handler.post(outerInstance.task);
					}
					else
					{
						outerInstance.viewPager.CurrentItem = outerInstance.currentItem;
						outerInstance.handler.PostDelayed(outerInstance.task, outerInstance.delayTime);
					}
				}
			}
		}

		public override bool DispatchTouchEvent(MotionEvent ev)
		{
	//        Log.i(tag, ev.getAction() + "--" + isAutoPlay);
			if (isAutoPlay)
			{
                MotionEventActions action = ev.Action;
				if (action == MotionEventActions.Up || action == MotionEventActions.Cancel || action == MotionEventActions.Outside)
				{
					StartAutoPlay();
				}
				else if (action == MotionEventActions.Down)
				{
					StopAutoPlay();
				}
			}
			return base.DispatchTouchEvent(ev);
		}

		/// <summary>
		/// 返回真实的位置
		/// </summary>
		/// <param name="position"> </param>
		/// <returns> 下标从0开始 </returns>
		public virtual int ToRealPosition(int position)
		{
			int realPosition = (position - 1) % count;
			if (realPosition < 0)
			{
				realPosition += count;
			}
			return realPosition;
		}

		internal class BannerPagerAdapter : PagerAdapter
		{
			private readonly Banner outerInstance;

			public BannerPagerAdapter(Banner outerInstance)
			{
				this.outerInstance = outerInstance;
			}


			public override int Count
			{
				get
				{
					return outerInstance.imageViews.Count;
				}
			}

			public override bool IsViewFromObject(View view, Object @object)
			{
				return view == @object;
			}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: @Override public Object instantiateItem(android.view.ViewGroup container, final int position)
			public override Object InstantiateItem(ViewGroup container, int position)
			{
				container.AddView(outerInstance.imageViews[position]);
				View view = outerInstance.imageViews[position];
				if (outerInstance.bannerListener != null)
				{
					view.SetOnClickListener(new OnClickListenerAnonymousInnerClass(this, position));
				}
				if (outerInstance.listener != null)
				{
					view.SetOnClickListener(new OnClickListenerAnonymousInnerClass2(this, position));
				}
				return view;
			}

			private class OnClickListenerAnonymousInnerClass :Java.Lang.Object, IOnClickListener
			{
				private readonly BannerPagerAdapter outerInstance;

				private int position;

				public OnClickListenerAnonymousInnerClass(BannerPagerAdapter outerInstance, int position)
				{
					this.outerInstance = outerInstance;
					this.position = position;
				}

				public void OnClick(View v)
				{
					Log.Error(outerInstance.outerInstance.tag, "你正在使用旧版点击事件接口，下标是从1开始，" + "为了体验请更换为setOnBannerListener，下标从0开始计算");
					outerInstance.outerInstance.bannerListener.OnBannerClick(position);
				}
			}

			private class OnClickListenerAnonymousInnerClass2 :Java.Lang.Object, IOnClickListener
			{
				private readonly BannerPagerAdapter outerInstance;

				private int position;

				public OnClickListenerAnonymousInnerClass2(BannerPagerAdapter outerInstance, int position)
				{
					this.outerInstance = outerInstance;
					this.position = position;
				}

				public void OnClick(View v)
				{
					outerInstance.outerInstance.listener.OnBannerClick(outerInstance.outerInstance.ToRealPosition(position));
				}
			}

			public override void DestroyItem(ViewGroup container, int position, Object @object)
			{
				container.RemoveView((View) @object);
			}

		}

		public void OnPageScrollStateChanged(int state)
		{
			if (mOnPageChangeListener != null)
			{
				mOnPageChangeListener.OnPageScrollStateChanged(state);
			}
			currentItem = viewPager.CurrentItem;
			switch (state)
			{
				case 0: //No operation
					if (currentItem == 0)
					{
						viewPager.SetCurrentItem(count, false);
					}
					else if (currentItem == count + 1)
					{
						viewPager.SetCurrentItem(1, false);
					}
					break;
				case 1: //start Sliding
					if (currentItem == count + 1)
					{
						viewPager.SetCurrentItem(1, false);
					}
					else if (currentItem == 0)
					{
						viewPager.SetCurrentItem(count, false);
					}
					break;
				case 2: //end Sliding
					break;
			}
		}

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
		{
			if (mOnPageChangeListener != null)
			{
				mOnPageChangeListener.OnPageScrolled(position, positionOffset, positionOffsetPixels);
			}
		}

		public void OnPageSelected(int position)
		{
			if (mOnPageChangeListener != null)
			{
				mOnPageChangeListener.OnPageSelected(position);
			}
			if (bannerStyle == BannerConfig.CIRCLE_INDICATOR || bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE || bannerStyle == BannerConfig.CIRCLE_INDICATOR_TITLE_INSIDE)
			{
				indicatorImages[(lastPosition - 1 + count) % count].SetImageResource(mIndicatorUnselectedResId);
				indicatorImages[(position - 1 + count) % count].SetImageResource(mIndicatorSelectedResId);
				lastPosition = position;
			}
			if (position == 0)
			{
				position = count;
			}
			if (position > count)
			{
				position = 1;
			}
			switch (bannerStyle)
			{
				case BannerConfig.CIRCLE_INDICATOR:
					break;
				case BannerConfig.NUM_INDICATOR:
					numIndicator.Text = position + "/" + count;
					break;
				case BannerConfig.NUM_INDICATOR_TITLE:
					numIndicatorInside.Text = position + "/" + count;
					bannerTitle.Text = titles[position - 1];
					break;
				case BannerConfig.CIRCLE_INDICATOR_TITLE:
					bannerTitle.Text = titles[position - 1];
					break;
				case BannerConfig.CIRCLE_INDICATOR_TITLE_INSIDE:
					bannerTitle.Text = titles[position - 1];
					break;
			}

		}

		[Obsolete]
		public virtual Banner SetOnBannerClickListener(OnBannerClickListener listener)
		{
			this.bannerListener = listener;
			return this;
		}

		/// <summary>
		/// 废弃了旧版接口，新版的接口下标是从1开始，同时解决下标越界问题
		/// </summary>
		/// <param name="listener">
		/// @return </param>
		public virtual Banner SetOnBannerListener(IOnBannerListener listener)
		{
			this.listener = listener;
			return this;
		}

		public virtual ViewPager.IOnPageChangeListener IOnPageChangeListener
		{
			set
			{
				mOnPageChangeListener = value;
			}
		}

		public virtual void ReleaseBanner()
		{
			handler.removeCallbacksAndMessages(null);
		}
	}

}