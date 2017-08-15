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

	public class DepthPageTransformer : ABaseTransformer
	{

		private const float MIN_SCALE = 0.75f;

		protected override void onTransform(View view, float position)
		{
			if (position <= 0f)
			{
				view.TranslationX = 0f;
				view.ScaleX = 1f;
				view.ScaleY = 1f;
			}
			else if (position <= 1f)
			{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float scaleFactor = MIN_SCALE + (1 - MIN_SCALE) * (1 - Math.abs(position));
				float scaleFactor = MIN_SCALE + (1 - MIN_SCALE) * (1 - Math.Abs(position));
				view.Alpha = 1 - position;
				view.PivotY = 0.5f * view.Height;
				view.TranslationX = view.Width * -position;
				view.ScaleX = scaleFactor;
				view.ScaleY = scaleFactor;
			}
		}

		protected override bool PagingEnabled
		{
			get
			{
				return true;
			}
		}

	}

}