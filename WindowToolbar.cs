
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class WindowToolbar : EditorPanel
	{
		private GUIStyle style; // refactor this into super class

		public WindowToolbar(TreeViewerWindow window) : base(window)
		{
			style = new GUIStyle()
			{
				normal = new GUIStyleState()
				{
					background = MakeTex(1, 1, new Color(0.4f, 0.4f, 0.4f))
				}
			};
		}

		public override void OnGUI()
		{
			GUILayout.BeginArea(PanelRect, style);

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("New", GUILayout.Width(100)))
			{

			}

			if (GUILayout.Button("Open", GUILayout.Width(100)))
			{

			}

			if (GUILayout.Button("Save", GUILayout.Width(100)))
			{

			}

			GUILayout.EndHorizontal();

			GUILayout.EndArea();
		}
	}
}
