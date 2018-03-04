using Assets.Scripts.Model.Data.EventTreeViewer;
using Assets.Scripts.Model.Data.TreeViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.TreeViewer.Popups
{
	public class NewTreePopup : Popup
	{
		private string fileName = "";
		private string path = "";

		public NewTreePopup(TreeViewerWindow parentWindow) : base(parentWindow)
		{
			title = "New Tree";
		}

		public override void DoWindow(int unusedWindowID)
		{
			EditorGUILayout.BeginVertical();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("File Name:");
			fileName = EditorGUILayout.TextField(fileName);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Path:");
			path = EditorGUILayout.TextField(path);
			EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("Create new file"))
			{
				try
				{
					NodeTree t = new NodeTree();
					t.AddNode(new EventTreeNode());
					t.AddNode(new EventTreeNode());
					t.Save(path + "/" + fileName + ".xml");
					parentWindow.CurrentPopup = null;
				}
				catch (Exception e)
				{
					Debug.Log(e);
				}
			}

			EditorGUILayout.EndVertical();
			GUI.DragWindow();
		}
	}
}
