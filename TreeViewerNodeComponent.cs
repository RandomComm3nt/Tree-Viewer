
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public abstract class TreeViewerNodeComponent
	{
		private bool expanded;

		public TreeViewerNodeComponent()
		{
		}

		public abstract string GetLabel();

		public virtual void DrawInspectorBlock()
		{
			expanded = EditorGUILayout.Foldout(expanded, GetLabel());
			if (expanded)
			{
				EditorGUILayout.TextField("dfgasd");
			}
		}

		public virtual float DrawNodeContent(Vector2 position)
		{
			return 0f;
		}
	}
}
