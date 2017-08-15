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

	public class ZoomInTransformer : ABaseTransformer
	{

		protected override void onTransform(View view, float position)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float scale = position < 0 ? position + 1f : Math.abs(1f - position);
			float scale = position < 0 ? position + 1f : Math.Abs(1f - position);
			view.ScaleX = scale;
			view.ScaleY = scale;
			view.PivotX = view.Width * 0.5f;
			view.PivotY = view.Height * 0.5f;
			view.Alpha = position < -1f || position > 1f ? 0f : 1f - (scale - 1f);
		}

	}

}