
#region Using Statements
using Assets.Scripts.Model.Data.TreeViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
		protected NodeInspector nodeInspector;
		private Dictionary<Type, Type> componentViewerMap;

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

		public Dictionary<Type, Type> ComponentViewerMap
		{
			get
			{
				return componentViewerMap;
			}

			set
			{
				componentViewerMap = value;
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

			componentViewerMap = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.GetCustomAttributes(typeof(ComponentViewerAttribute), true).Length > 0)
				.ToDictionary(t => (t.GetCustomAttribute(typeof(ComponentViewerAttribute)) as ComponentViewerAttribute).ComponentType, t => t) ;
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
