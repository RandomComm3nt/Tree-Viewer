
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

			foreach (TreeViewerNode node in Window.Nodes)
			{
				node.Draw();
			}

			GUILayout.EndArea();
		}
	}
}
