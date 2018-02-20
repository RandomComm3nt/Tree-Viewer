
#region Using Statements
using UnityEditor;
using UnityEngine;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	public class TreeViewerNode
	{
		#region Fields

		public static Color backgroundColour = Color.blue;
		public static Color outlineColour = Color.black;

		private float x;
		private float y;
		private float width;
		private float height;

		private bool mouseDown;

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

		#endregion
	}
}
