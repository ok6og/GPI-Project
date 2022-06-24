using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	[Serializable]
	public class TestShape : Shape
	{
		#region Constructor

		public TestShape(RectangleF rect) : base(rect)
		{
		}

		public TestShape(TestShape rectangle) : base(rectangle)
		{
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
			base.DrawSelf(grfx);

			var pointA = new PointF(Rectangle.X + 29, Rectangle.Y + 29);
			var pointB = new PointF(Rectangle.X + Rectangle.Height - 30, Rectangle.Y + Rectangle.Height - 30);
			var pointC = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y);
			var pointD = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height);
			var pointE = new PointF(Rectangle.X + Rectangle.Height - 30, Rectangle.Y + 30);
			var pointF = new PointF(Rectangle.X + 29, Rectangle.Y + Rectangle.Height - 30);


			grfx.FillEllipse(new SolidBrush(Color.FromArgb(Opacity, FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(OutlineColor, OutlineWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			//grfx.DrawLine(new Pen(StrokeColor), (int)smallPiece, (int)smallPiece, (int)bigPiece+300, (int)smallPiece+300);
			//grfx.DrawLine(new Pen(StrokeColor), 300, Rectangle.X, 300, 400);
			grfx.DrawLine(new Pen(OutlineColor), pointA, pointB);
			grfx.DrawLine(new Pen(OutlineColor), pointC, pointD);
			grfx.DrawLine(new Pen(OutlineColor), pointE, pointF);


			grfx.ResetTransform();

		}
	}
}
