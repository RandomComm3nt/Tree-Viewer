
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion


namespace Assets.Scripts.Editor.TreeViewer
{
	public class EditorPanel
	{
		private Rect panelRect;

		public Rect PanelRect
		{
			get
			{
				return panelRect;
			}

			set
			{
				panelRect = value;
			}
		}

		// https://forum.unity.com/threads/changing-the-background-color-for-beginhorizontal.66015/
		protected Texture2D MakeTex(int width, int height, Color col)
		{
			Color[] pix = new Color[width * height];

			for (int i = 0; i < pix.Length; i++)
				pix[i] = col;

			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();

			return result;
		}

		public virtual void OnGUI()
		{

		}
	}
}
