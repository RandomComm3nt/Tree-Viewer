
#region Using Statements
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public abstract class TreeViewerWindow : EditorWindow
	{
		#region Fields

		private bool init = false;
		private List<TreeViewerNode> nodes;
		private Rect windowRect;
		private TreeViewerPanel treeViewerPanel;
		private NodeInspector nodeInspector;

		#endregion

		#region Properties

		public List<TreeViewerNode> Nodes
		{
			get
			{
				return nodes;
			}

			set
			{
				nodes = value;
			}
		}

		#endregion

		#region Methods

		protected virtual void Initiate()
		{
			init = true;
			nodes = new List<TreeViewerNode>();
			treeViewerPanel = new TreeViewerPanel(this);
			nodeInspector = new NodeInspector(this);
			ResizePanels();
		}

		private void ResizePanels()
		{
			windowRect = position;
			treeViewerPanel.PanelRect = new Rect(0, 0, windowRect.width * 0.75f, windowRect.height);
			nodeInspector.PanelRect = new Rect(treeViewerPanel.PanelRect.xMax, 0, windowRect.width - treeViewerPanel.PanelRect.width, windowRect.height);
		}
		
		private void OnGUI()
		{
			if (!init)
				Initiate();

			HandleEvents();

			treeViewerPanel.OnGUI();
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
