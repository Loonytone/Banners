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

using IPageTransformer = Android.Support.V4.View.ViewPager.IPageTransformer;
using Android.Views;

namespace Com.Loonytone.Droid.Banners.transformer
{

	public abstract class ABaseTransformer : Java.Lang.Object, IPageTransformer
	{

		/// <summary>
		/// Called each <seealso cref="#transformPage(View, float)"/>.
		/// </summary>
		/// <param name="page">
		///            Apply the transformation to this page </param>
		/// <param name="position">
		///            Position of page relative to the current front-and-center position of the pager. 0 is front and
		///            center. 1 is one full page position to the right, and -1 is one page position to the left. </param>
		protected abstract void onTransform(View page, float position);

		/// <summary>
		/// Apply a property transformation to the given page. For most use cases, this method should not be overridden.
		/// Instead use <seealso cref="#transformPage(View, float)"/> to perform typical transformations.
		/// </summary>
		/// <param name="page">
		///            Apply the transformation to this page </param>
		/// <param name="position">
		///            Position of page relative to the current front-and-center position of the pager. 0 is front and
		///            center. 1 is one full page position to the right, and -1 is one page position to the left. </param>
		public void TransformPage(View page, float position)
		{
			onPreTransform(page, position);
			onTransform(page, position);
			onPostTransform(page, position);
		}

		/// <summary>
		/// If the position offset of a fragment is less than negative one or greater than one, returning true will set the
		/// fragment alpha to 0f. Otherwise fragment alpha is always defaulted to 1f.
		/// 
		/// @return
		/// </summary>
		protected virtual bool hideOffscreenPages()
		{
			return true;
		}

		/// <summary>
		/// Indicates if the default animations of the view pager should be used.
		/// 
		/// @return
		/// </summary>
		protected virtual bool PagingEnabled
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Called each <seealso cref="#transformPage(View, float)"/> before {<seealso cref="#onTransform(View, float)"/>.
		/// <para>
		/// The default implementation attempts to reset all view properties. This is useful when toggling transforms that do
		/// not modify the same page properties. For instance changing from a transformation that applies rotation to a
		/// transformation that fades can inadvertently leave a fragment stuck with a rotation or with some degree of applied
		/// alpha.
		/// 
		/// </para>
		/// </summary>
		/// <param name="page">
		///            Apply the transformation to this page </param>
		/// <param name="position">
		///            Position of page relative to the current front-and-center position of the pager. 0 is front and
		///            center. 1 is one full page position to the right, and -1 is one page position to the left. </param>
		protected virtual void onPreTransform(View page, float position)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float width = page.getWidth();
			float width = page.Width;

			page.RotationX = 0;
			page.RotationY = 0;
			page.Rotation = 0;
			page.ScaleX = 1;
			page.ScaleY = 1;
			page.PivotX = 0;
			page.PivotY = 0;
			page.TranslationY = 0;
			page.TranslationX = PagingEnabled ? 0f : -width * position;

			if (hideOffscreenPages())
			{
				page.Alpha = position <= -1f || position >= 1f ? 0f : 1f;
	//			page.setEnabled(false);
			}
			else
			{
	//			page.setEnabled(true);
				page.Alpha = 1f;
			}
		}

		/// <summary>
		/// Called each <seealso cref="#transformPage(View, float)"/> after <seealso cref="#onTransform(View, float)"/>.
		/// </summary>
		/// <param name="page">
		///            Apply the transformation to this page </param>
		/// <param name="position">
		///            Position of page relative to the current front-and-center position of the pager. 0 is front and
		///            center. 1 is one full page position to the right, and -1 is one page position to the left. </param>
		protected virtual void onPostTransform(View page, float position)
		{
		}

		/// <summary>
		/// Same as <seealso cref="Math#min(double, double)"/> without double casting, zero closest to infinity handling, or NaN support.
		/// </summary>
		/// <param name="val"> </param>
		/// <param name="min">
		/// @return </param>
		protected static float min(float val, float min)
		{
			return val < min ? min : val;
		}

	}

}