
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

		private TreeNode node;

		private bool selected;

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

		#endregion

		#region Methods

		public TreeViewerNode()
		{
			x = 0;
			y = 0;
			width = 100;
			height = 100;
		}

		public void Draw()
		{
			Handles.DrawSolidRectangleWithOutline(new Rect(x, y, width, height), backgroundColour, outlineColour);
		}

		public bool HandleMouseEvent()
		{
			if (new Rect(x, y, width, height).Contains(Event.current.mousePosition))
			{
				if (Event.current.type == EventType.MouseDown)
				{
					mouseDown = true;
				}
				else if (Event.current.type == EventType.MouseUp)
				{
					mouseDown = false;
				}
				return true;
			}
			return false;
		}

		public void MouseUpOutside()
		{
			mouseDown = false;
		}

		public abstract List<Type> GetAvailableNewComponents();

		#endregion
	}
}
