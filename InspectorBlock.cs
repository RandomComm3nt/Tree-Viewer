
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class InspectorBlock
	{
		private bool expanded;
		private string title;

		public InspectorBlock()
		{
			title = "meh";
		}

		public virtual void OnGUI()
		{
			expanded = EditorGUILayout.Foldout(expanded, title);
			if (expanded)
			{
				EditorGUILayout.TextField("dfgasd");
			}
		}
	}
}
