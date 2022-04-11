using System;
using UnityEngine;

namespace EngiShotgun.Assets
{
	// Token: 0x02000006 RID: 6
	public static class Assets
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000027A6 File Offset: 0x000009A6
		public static Texture2D LoadTexture2D(byte[] resourceBytes)
		{
			if (resourceBytes == null)
			{
				throw new ArgumentNullException("resourceBytes");
			}
			Texture2D texture2D = new Texture2D(128, 128, UnityEngine.Experimental.Rendering.DefaultFormat.HDR, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			ImageConversion.LoadImage(texture2D, resourceBytes, false);
			return texture2D;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027D3 File Offset: 0x000009D3
		public static Sprite TexToSprite(Texture2D tex)
		{
			return Sprite.Create(tex, new Rect(0f, 0f, (float)tex.width, (float)tex.height), new Vector2(0.5f, 0.5f));
		}
	}
}
