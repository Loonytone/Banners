using System;

/*
 * Copyright 2014 Toxic Bakery
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Android.Views;

namespace Com.Loonytone.Droid.Banners.transformer
{
	public class ZoomOutSlideTransformer : ABaseTransformer
	{

		private const float MIN_SCALE = 0.85f;
		private const float MIN_ALPHA = 0.5f;

		protected override void onTransform(View view, float position)
		{
			if (position >= -1 || position <= 1)
			{
				// Modify the default slide transition to shrink the page as well
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float height = view.getHeight();
				float height = view.Height;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float width = view.getWidth();
				float width = view.Width;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float scaleFactor = Math.max(MIN_SCALE, 1 - Math.abs(position));
				float scaleFactor = Math.Max(MIN_SCALE, 1 - Math.Abs(position));
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float vertMargin = height * (1 - scaleFactor) / 2;
				float vertMargin = height * (1 - scaleFactor) / 2;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float horzMargin = width * (1 - scaleFactor) / 2;
				float horzMargin = width * (1 - scaleFactor) / 2;

				// Center vertically
				view.PivotY = 0.5f * height;
				view.PivotX = 0.5f * width;

				if (position < 0)
				{
					view.TranslationX = horzMargin - vertMargin / 2;
				}
				else
				{
					view.TranslationX = -horzMargin + vertMargin / 2;
				}

				// Scale the page down (between MIN_SCALE and 1)
				view.ScaleX = scaleFactor;
				view.ScaleY = scaleFactor;

				// Fade the page relative to its size.
				view.Alpha = MIN_ALPHA + (scaleFactor - MIN_SCALE) / (1 - MIN_SCALE) * (1 - MIN_ALPHA);
			}
		}

	}

}