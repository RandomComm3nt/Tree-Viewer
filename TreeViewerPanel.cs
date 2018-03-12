
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class TreeViewerPanel : EditorPanel
	{
		private GUIStyle style; // refactor this into super class
		Vector2 scroll = new Vector2();
		public TreeViewerPanel(TreeViewerWindow window) : base(window)
		{
			style = new GUIStyle()
			{
				normal = new GUIStyleState()
				{
					background = MakeTex(1, 1, new Color(0.2f, 0.2f, 0.2f))
				}
			};
		}

		public override void OnGUI()
		{
			GUILayout.BeginArea(PanelRect, style);
			scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Width(9000), GUILayout.Height(9000));

			foreach (TreeViewerNode node in Window.Nodes)
			{
				node.Draw();
			}

			EditorGUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}
}
