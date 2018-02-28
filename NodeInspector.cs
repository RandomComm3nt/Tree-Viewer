
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class NodeInspector : EditorPanel
	{
		private GUIStyle style;
		
		public NodeInspector()
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

			EditorGUILayout.BeginHorizontal();
			string s = EditorGUILayout.TextField("Node Name");
			EditorGUILayout.EndHorizontal();

			GUILayout.EndArea();
		}
	}
}
