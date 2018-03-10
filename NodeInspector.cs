
#region Using Statements
using Assets.Scripts.Editor.EventTreeViewer;
using Assets.Scripts.Model.Data.EventTreeViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class NodeInspector : EditorPanel
	{
		private GUIStyle style;
		private int selectedComponentToAdd;
		private List<Type> componentOptionList;
		private string[] componentOptionLabelList;
		private Vector2 scrollPosition = new Vector2();

		public NodeInspector(TreeViewerWindow window) : base(window)
		{
			style = new GUIStyle()
			{
				normal = new GUIStyleState()
				{
					background = MakeTex(1, 1, new Color(0.3f, 0.3f, 0.3f))
				}
			};
			componentOptionList = new List<Type> { typeof(NodeInspector), typeof(GUIStyle) };
			NodeSelected();
		}

		public void NodeSelected()
		{
			if (Window.SelectedNode == null)
			{

			}
			else
			{
				componentOptionList = Window.SelectedNode.GetAvailableNewComponents();
				componentOptionLabelList = componentOptionList.Select(t => t.Name).ToArray();
			}
		}

		public override void OnGUI()
		{
			GUILayout.BeginArea(PanelRect, style);
			EditorGUILayout.BeginVertical();

			EditorGUILayout.BeginHorizontal();
			if (Window.SelectedNode == null)
			{
				EditorGUILayout.LabelField("No node selected.");
			}
			else
			{
				Window.SelectedNode.Node.Name = EditorGUILayout.TextField(Window.SelectedNode.Node.Name);
				EditorGUILayout.EndHorizontal();

				scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
				for (int i = 0; i < Window.SelectedNode.Components.Count; i++)
				{
					if (Window.SelectedNode.Components[i] == null)
					{

					}
					else
					{
						Window.SelectedNode.Components[i].DrawInspectorBlock();
					}
				}
				EditorGUILayout.EndScrollView();

				EditorGUILayout.BeginHorizontal();
				selectedComponentToAdd = EditorGUILayout.Popup(selectedComponentToAdd, componentOptionLabelList);
				if (GUILayout.Button("Add"))
				{
					Window.SelectedNode.AddComponent(componentOptionList[selectedComponentToAdd]);
					NodeSelected();
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}
