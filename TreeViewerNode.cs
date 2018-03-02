
#region Using Statements
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Model.Data.TreeViewer;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public abstract class TreeViewerNode
	{
		#region Fields

		public static Color backgroundColour = Color.blue;
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

		public TreeNode Node
		{
			get
			{
				return node;
			}

			set
			{
				node = value;
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

		#endregion

		#region Methods

		public TreeViewerNode(TreeViewerWindow window)
		{
			x = 0;
			y = 0;
			width = 100;
			height = 100;
			this.window = window;
		}

		public void Draw()
		{
			Handles.DrawSolidRectangleWithOutline(new Rect(x, y, width, height), backgroundColour, outlineColour);
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

		#endregion
	}
}
