using System;
using Com.Loonytone.Droid.Banners.transformer;

namespace Com.Loonytone.Droid.Banners
{


    public class Transformer
	{
		public static Type Default = typeof(DefaultTransformer);
		public static Type Accordion = typeof(AccordionTransformer);
		public static Type BackgroundToForeground = typeof(BackgroundToForegroundTransformer);
		public static Type ForegroundToBackground = typeof(ForegroundToBackgroundTransformer);
		public static Type CubeIn = typeof(CubeInTransformer);
		public static Type CubeOut = typeof(CubeOutTransformer);
		public static Type DepthPage = typeof(DepthPageTransformer);
		public static Type FlipHorizontal = typeof(FlipHorizontalTransformer);
		public static Type FlipVertical = typeof(FlipVerticalTransformer);
		public static Type RotateDown = typeof(RotateDownTransformer);
		public static Type RotateUp = typeof(RotateUpTransformer);
		public static Type ScaleInOut = typeof(ScaleInOutTransformer);
		public static Type Stack = typeof(StackTransformer);
		public static Type Tablet = typeof(TabletTransformer);
		public static Type ZoomIn = typeof(ZoomInTransformer);
		public static Type ZoomOut = typeof(ZoomOutTranformer);
		public static Type ZoomOutSlide = typeof(ZoomOutSlideTransformer);
	}

}