
#region Using Statements
using Assets.Scripts.Editor.TreeViewer.Popups;
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
		private NodeTree tree;
		private List<TreeViewerNode> nodes;
		private Rect windowRect;
		private WindowToolbar toolbar;
		private TreeViewerPanel treeViewerPanel;
		protected NodeInspector nodeInspector;
		private Dictionary<Type, Type> componentViewerMap;
		private TreeViewerNode selectedNode;
		private Popup currentPopup;
		public string treePath = "C:\\Users\\Rando\\Documents\\GitHub";

		#endregion

		#region Properties

		public List<TreeViewerNode> Nodes
		{
			get
			{
				return nodes;
			}
		}

		public Dictionary<Type, Type> ComponentViewerMap
		{
			get
			{
				return componentViewerMap;
			}
		}


		public TreeViewerNode SelectedNode
		{
			get
			{
				return selectedNode;
			}

			set
			{
				if (selectedNode != null)
					selectedNode.Selected = false;
				if (value != null)
					value.Selected = true;
				selectedNode = value;
				nodeInspector.NodeSelected();
			}
		}

		public Popup CurrentPopup
		{
			get
			{
				return currentPopup;
			}

			set
			{
				currentPopup = value;
			}
		}

		#endregion

		#region Methods

		public void NewTree(string name)
		{
			NodeTree tree = new NodeTree()
			{
				Name = name
			};
			SetTree(tree);
		}

		public void SaveTree()
		{
			tree.Save(treePath + "/" + tree.Name + ".xml");
		}

		protected abstract TreeViewerNode CreateNewTreeViewerNode(TreeNode node);

		protected virtual void Initiate()
		{
			init = true;
			nodes = new List<TreeViewerNode>();
			toolbar = new WindowToolbar(this);
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
			toolbar.PanelRect = new Rect(0, 0, windowRect.xMax, 25f);
			treeViewerPanel.PanelRect = new Rect (
				0,
				toolbar.PanelRect.yMax,
				windowRect.width * 0.75f,
				windowRect.height - toolbar.PanelRect.height); 
			nodeInspector.PanelRect = new Rect (
				treeViewerPanel.PanelRect.xMax,
				toolbar.PanelRect.yMax,
				windowRect.width - treeViewerPanel.PanelRect.width,
				windowRect.height - toolbar.PanelRect.height);
		}

		private void OnGUI()
		{
			if (!init)
				Initiate();

			toolbar.OnGUI();
			if (tree == null)
			{

			}
			else
			{
				HandleEvents();
				treeViewerPanel.OnGUI();
				nodeInspector.OnGUI();
			}

			if (currentPopup != null)
			{
				BeginWindows();
				currentPopup.OnGUI(1);
				EndWindows();
			}
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
				}
				// if not handled by node, handle as background click
				if (!eventHandled)
					HandleEventOnBackground();
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

		public void SetTree(NodeTree tree)
		{
			this.tree = tree;
			nodes = tree.Nodes
				.Select(n => CreateNewTreeViewerNode(n))
				.ToList();
		}

		public void AddNodeToTree()
		{
			TreeNode n = new TreeNode();
			tree.AddNode(n);
			nodes.Add(CreateNewTreeViewerNode(n));
		}

		#endregion
	}
}
