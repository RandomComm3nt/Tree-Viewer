
#region Using Statements
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Model.Data.TreeViewer;
using System.Linq;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public abstract class TreeViewerNode
	{
		#region Fields

		public static Color labelBackgroundColour = new Color(0.4f, 0.4f, 0.4f);
		public static Color backgroundColour = new Color(0.3f, 0.3f, 0.3f);
		public static Color outlineColour = Color.black;

		private float x;
		private float y;
		private float width;
		private float height;

		private bool mouseDown;
		private Vector2 mouseDownPosition;

		private TreeNode node;

		private bool selected;
		private TreeViewerWindow window;
		private bool clickedLastUpdate;
		public NodeLink parentLink;
		public List<NodeLink> childLinks;
		private List<TreeViewerNodeComponent> components;

		#endregion

		#region Properties

		public TreeNode Node
		{
			get
			{
				return node;
			}

			set
			{
				node = value;
				components = node.Components.Select(c =>
				{
					Type vc;
					if (window.ComponentViewerMap.TryGetValue(c.GetType(), out vc))
						return (TreeViewerNodeComponent)Activator.CreateInstance(vc);
					else return null;
				}).ToList();
			}
		}

		public bool Selected
		{
			get
			{
				return selected;
			}

			set
			{
				selected = value;
			}
		}

		public float X
		{
			get
			{
				return x;
			}

			set
			{
				x = value;
			}
		}

		public float Y
		{
			get
			{
				return y;
			}

			set
			{
				y = value;
			}
		}

		public List<TreeViewerNodeComponent> Components
		{
			get
			{
				return components;
			}
		}

		#endregion

		#region Methods

		public TreeViewerNode(TreeViewerWindow window)
		{
			x = 0;
			y = 0;
			width = 100;
			height = 100;
			this.window = window;
			childLinks = new List<NodeLink>();
			components = new List<TreeViewerNodeComponent>();
		}

		public void Draw()
		{
			Handles.DrawSolidRectangleWithOutline(new Rect(x, y + 20, width, height - 20), backgroundColour, outlineColour);
			Handles.DrawSolidRectangleWithOutline(new Rect(x, y, width, 20), labelBackgroundColour, outlineColour);
			Handles.Label(new Vector2(x+1, y+1), node.Name);
			float componentHeight = 0f;
			for (int i = 0; i < components.Count; i++)
			{
				componentHeight += components[i].DrawNodeContent(new Vector2(x, y + 20 + componentHeight));
			}
			height = componentHeight + 20;
		}

		public bool HandleMouseEvent()
		{
			// Unity can't handle changes in mouse events that cause UI elements to change
			// so we defer method to next update
			if (Event.current.type == EventType.Layout && clickedLastUpdate)
			{
				Click();
				clickedLastUpdate = false;
				return true;
			}

			if (new Rect(x, y, width, height).Contains(Event.current.mousePosition))
			{
				if (Event.current.type == EventType.MouseDown)
				{
					mouseDown = true;
					mouseDownPosition = Event.current.mousePosition;
				}
				else if (Event.current.type == EventType.MouseUp)
				{
					if (mouseDown)
					{
						mouseDown = false;
						if (Vector2.Distance(Event.current.mousePosition, mouseDownPosition) < 5f)
							clickedLastUpdate = true;
					}
				}
				return true;
			}
			return false;
		}

		public void MouseUpOutside()
		{
			mouseDown = false;
		}

		private void Click()
		{
			window.SelectedNode = this;
		}

		public abstract List<Type> GetAvailableNewComponents();

		public float LayoutChildNodes()
		{
			float childHeight = 0;
			for (int i = 0; i < childLinks.Count; i++)
			{
				if (i > 0)
					childHeight += 10;
				childLinks[i].childNode.Y = Y + childHeight;
				childLinks[i].childNode.X = X + width + 10;
				childHeight += childLinks[i].childNode.LayoutChildNodes();
			}
			return Mathf.Max(childHeight, height);
		}

		public void AddComponent(Type T)
		{
			Type vc;
			if (window.ComponentViewerMap.TryGetValue(node.AddComponent(T).GetType(), out vc))
			{
				components.Add((TreeViewerNodeComponent)Activator.CreateInstance(vc));
			}
		}

		#endregion
	}
}
