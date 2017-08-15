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

	public class BackgroundToForegroundTransformer : ABaseTransformer
	{

		protected override void onTransform(View view, float position)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float height = view.getHeight();
			float height = view.Height;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float width = view.getWidth();
			float width = view.Width;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float scale = min(position < 0 ? 1f : Math.abs(1f - position), 0.5f);
			float scale = min(position < 0 ? 1f : Math.Abs(1f - position), 0.5f);

			view.ScaleX = scale;
			view.ScaleY = scale;
			view.PivotX = width * 0.5f;
			view.PivotY = height * 0.5f;
			view.TranslationX = position < 0 ? width * position : -width * position * 0.25f;
		}

	}

}