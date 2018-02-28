
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class TreeViewerWindow : EditorWindow
	{
		#region Fields

		private bool init = false;
		private List<TreeViewerNode> nodes;
		private Rect windowRect;
		private Rect treeRect;
		private NodeInspector nodeInspector;

		#endregion

		#region Methods

		[MenuItem("Window/Tree Viewer")]
		internal static void Init()
		{
			// Get existing open window or if none, make a new one
			TreeViewerWindow window = GetWindow<TreeViewerWindow>();
			window.Show();
		}

		private void Initiate()
		{
			nodes = new List<TreeViewerNode>();
			nodes.Add(new TreeViewerNode());
			nodeInspector = new NodeInspector();
			ResizePanels();
		}

		private void ResizePanels()
		{
			windowRect = position;
			treeRect = new Rect(0, 0, windowRect.width * 0.75f, windowRect.height);
			nodeInspector.PanelRect = new Rect(treeRect.xMax, 0, windowRect.width - treeRect.width, windowRect.height);
		}
		
		private void OnGUI()
		{
			if (!init)
				Initiate();

			HandleEvents();

			GUILayout.BeginArea(treeRect);

			foreach (TreeViewerNode node in nodes)
			{
				node.Draw();
			}

			GUILayout.EndArea();

			nodeInspector.OnGUI();
		}

		private void HandleEvents()
		{
			// if active event, find node under mouse and handle event
			if (Event.current != null)
			{
				bool eventHandled = false;
				foreach (TreeViewerNode node in nodes)
				{
					// only find first node to handle event
					if (node.HandleMouseEvent())
					{
						eventHandled = true;
						break;
					}
					// if not handled by node, handle as background click
					if (!eventHandled)
						HandleEventOnBackground();
				}
			}
		}

		private void HandleEventOnBackground()
		{
			if (Event.current.type == EventType.MouseUp)
			{
				foreach (TreeViewerNode node in nodes)
				{
					node.MouseUpOutside();
				}
			}
		}

		#endregion
	}
}
