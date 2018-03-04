using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor.TreeViewer.Popups
{
	public class SaveAsTreePopup : Popup
	{
		public SaveAsTreePopup(TreeViewerWindow parentWindow) : base(parentWindow)
		{
			title = "Save Tree";
		}

		public override void DoWindow(int unusedWindowID)
		{
			GUILayout.Button("Hi");
			GUI.DragWindow();
		}
	}
}
