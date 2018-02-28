
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
		List<InspectorBlock> blocks;
		
		public NodeInspector(TreeViewerWindow window) : base(window)
		{
			style = new GUIStyle()
			{
				normal = new GUIStyleState()
				{
					background = MakeTex(1, 1, new Color(0.3f, 0.3f, 0.3f))
				}
			};
			blocks = new List<InspectorBlock>();
		}

		public override void OnGUI()
		{
			GUILayout.BeginArea(PanelRect, style);
			EditorGUILayout.BeginVertical();

			EditorGUILayout.BeginHorizontal();
			string s = EditorGUILayout.TextField("Node Name");
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginScrollView(new Vector2());
			for (int i = 0; i < blocks.Count; i++)
			{
				blocks[i].OnGUI();
			}
			EditorGUILayout.EndScrollView();

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Add"))
			{
				blocks.Add(new InspectorBlock());
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}
