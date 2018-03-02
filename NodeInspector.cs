
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
		private List<InspectorBlock> blocks;
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
			blocks = new List<InspectorBlock>();
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
				blocks = Window.SelectedNode.Node.Components.Select(c =>
				{
					Type vc;
					if (Window.ComponentViewerMap.TryGetValue(c.GetType(), out vc))
						return (InspectorBlock)Activator.CreateInstance(vc);
					else return null;
				}).ToList();
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
				string s = EditorGUILayout.TextField("Node Name");
				EditorGUILayout.EndHorizontal();

				scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
				for (int i = 0; i < blocks.Count; i++)
				{
					if (blocks[i] == null)
					{

					}
					else
					{
						blocks[i].OnGUI();
					}
				}
				EditorGUILayout.EndScrollView();

				EditorGUILayout.BeginHorizontal();
				selectedComponentToAdd = EditorGUILayout.Popup(selectedComponentToAdd, componentOptionLabelList);
				if (GUILayout.Button("Add"))
				{
					Window.SelectedNode.Node.AddComponent(componentOptionList[selectedComponentToAdd]);
					NodeSelected();
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}
