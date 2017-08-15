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
	public class FlipHorizontalTransformer : ABaseTransformer
	{

		protected override void onTransform(View view, float position)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float rotation = 180f * position;
			float rotation = 180f * position;

			view.Alpha = rotation > 90f || rotation < -90f ? 0 : 1;
			view.PivotX = view.Width * 0.5f;
			view.PivotY = view.Height * 0.5f;
			view.RotationY = rotation;
		}

		protected override void onPostTransform(View page, float position)
		{
			base.onPostTransform(page, position);

			//resolve problem: new page can't handle click event!
			if (position > -0.5f && position < 0.5f)
			{
				page.Visibility = ViewStates.Visible;
			}
			else
			{
				page.Visibility = ViewStates.Invisible;
			}
		}
	}

}