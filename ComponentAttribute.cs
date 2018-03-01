
#region Using Statements
using System;
#endregion

namespace Assets.Scripts.Editor.TreeViewer
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ComponentViewerAttribute : Attribute
	{
		private Type componentType;

		public ComponentViewerAttribute(Type componentType)
		{
			this.componentType = componentType;
		}

		public Type ComponentType
		{
			get
			{
				return componentType;
			}

			set
			{
				componentType = value;
			}
		}
	}
}
