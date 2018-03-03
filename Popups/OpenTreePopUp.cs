using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor.TreeViewer.Popups
{
	public class OpenTreePopup : Popup
	{
		public OpenTreePopup(TreeViewerWindow parentWindow) : base(parentWindow)
		{
			title = "Open Tree";
		}

		public override void DoWindow(int unusedWindowID)
		{
			GUILayout.Button("Hi");
			GUI.DragWindow();
		}
	}
}
