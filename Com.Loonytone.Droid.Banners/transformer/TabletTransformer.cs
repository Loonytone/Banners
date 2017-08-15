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
using Android.Graphics;

namespace Com.Loonytone.Droid.Banners.transformer
{

	public class TabletTransformer : ABaseTransformer
	{

		private static readonly Matrix OFFSET_MATRIX = new Matrix();
		private static readonly Camera OFFSET_CAMERA = new Camera();
		private static readonly float[] OFFSET_TEMP_FLOAT = new float[2];

		protected override void onTransform(View view, float position)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final float rotation = (position < 0 ? 30f : -30f) * Math.abs(position);
			float rotation = (position < 0 ? 30f : -30f) * Math.Abs(position);

			view.TranslationX = getOffsetXForRotation(rotation, view.Width, view.Height);
			view.PivotX = view.Width * 0.5f;
			view.PivotY = 0;
			view.RotationY = rotation;
		}

		protected static float getOffsetXForRotation(float degrees, int width, int height)
		{
			OFFSET_MATRIX.Reset();
			OFFSET_CAMERA.Save();
			OFFSET_CAMERA.RotateY(Math.Abs(degrees));
			OFFSET_CAMERA.GetMatrix(OFFSET_MATRIX);
			OFFSET_CAMERA.Restore();

			OFFSET_MATRIX.PreTranslate(-width * 0.5f, -height * 0.5f);
			OFFSET_MATRIX.PostTranslate(width * 0.5f, height * 0.5f);
			OFFSET_TEMP_FLOAT[0] = width;
			OFFSET_TEMP_FLOAT[1] = height;
			OFFSET_MATRIX.MapPoints(OFFSET_TEMP_FLOAT);
			return (width - OFFSET_TEMP_FLOAT[0]) * (degrees > 0.0f ? 1.0f : -1.0f);
		}

	}

}