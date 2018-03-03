using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.TreeViewer.Popups
{
	public abstract class Popup
	{
		private Rect windowRect;
		protected string title;
		protected TreeViewerWindow parentWindow;

		public Popup(TreeViewerWindow parentWindow)
		{
			this.parentWindow = parentWindow;
			windowRect = new Rect(0, 50, 150, 150);
		}

		public void OnGUI(int windowId)
		{
			windowRect = GUILayout.Window(windowId, windowRect, DoWindow, title);
		}

		public abstract void DoWindow(int unusedWindowID);
	}
}
