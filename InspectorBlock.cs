
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public abstract class InspectorBlock
	{
		private bool expanded;

		public InspectorBlock()
		{
		}

		public abstract string GetLabel();

		public virtual void OnGUI()
		{
			expanded = EditorGUILayout.Foldout(expanded, GetLabel());
			if (expanded)
			{
				EditorGUILayout.TextField("dfgasd");
			}
		}
	}
}
