using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	[Serializable]
	public class GroupShape : Shape
	{
		#region Constructor

		private List<Shape> grouped = new List<Shape>();

		public List<Shape> Grouped
        {
			get { return grouped; }
			set { grouped = value; }
        }
		

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			foreach (var item in this.Grouped)
			{
				item.DrawSelf(grfx);
			}

		}

		public static void Degroup(GroupShape gr)
		{
			foreach (var item in gr.Grouped.ToList())
			{
				item.GroupedIs = false;
				gr.Grouped.Remove(item);
			}
		}
	}
}
