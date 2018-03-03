using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Editor.TreeViewer.Popups
{
	public class NewTreePopup : Popup
	{
		public NewTreePopup() : base()
		{
			title = "New Tree";
		}

		public override void DoWindow(int unusedWindowID)
		{
			GUILayout.Button("Hi");
			GUI.DragWindow();
		}
	}
}
