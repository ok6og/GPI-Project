using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection {
			get { return selection; }
			set { selection = value; }
		}

		private List<GroupShape> groups = new List<GroupShape>();
		public List<GroupShape> Groups
		{
			get { return groups; }
			set { groups = value; }
		}

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;
			rect.OutlineColor = Color.Black;
			rect.Opacity = 255;
			rect.OutlineWidth = 1;

			ShapeList.Add(rect);
		}

		public void AddRandomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			ElipseShape elipse = new ElipseShape(new Rectangle(x, y, 150, 300));
			elipse.FillColor = Color.White;
			elipse.OutlineColor = Color.Black;
			elipse.Opacity = 255;
			elipse.OutlineWidth = 1;

			ShapeList.Add(elipse);
		}

		public void AddRandomTriangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			TriangleShape triangle = new TriangleShape(new Rectangle(x, y, 100, 200));
			triangle.FillColor = Color.White;
			triangle.OutlineColor = Color.Black;
			triangle.Opacity = 255;
			triangle.OutlineWidth = 1;

			ShapeList.Add(triangle);
		}

		public void AddRandomTest()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			TestShape test = new TestShape(new Rectangle(x, y, 200, 200));

			test.FillColor = Color.White;
			test.OutlineColor = Color.Black;
			test.Opacity = 255;
			test.OutlineWidth = 1;

			ShapeList.Add(test);
		}

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){

						
					return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (selection.Count > 0)
			{
				foreach (Shape item in Selection)
				{
					item.Location = new PointF(
						item.Location.X + p.X - lastLocation.X,
						item.Location.Y + p.Y - lastLocation.Y);
				}

				lastLocation = p;
			}
		}

        public override void DrawShape(Graphics grfx, Shape item)
        {
			base.DrawShape(grfx, item);
			if (Selection.Contains(item))
			{
				grfx.DrawRectangle(
					new Pen(Color.Gray),
					item.Location.X - 3,
					item.Location.Y - 3,
					item.Width + 6,
					item.Height + 6
				);
			}
		}
		public static void ConvertToStream(object obj, string filePath = null)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream;
			if (filePath != null)
			{
				stream = new FileStream(filePath + ".bin",FileMode.Create);
			}
			else
			{
				stream = new FileStream("random.bin",FileMode.Create,FileAccess.Write, FileShare.None);
			}
			formatter.Serialize(stream, obj);
			stream.Close();
		}

		public static object ConvertStream(string filePath = null)
		{
			
			IFormatter formatter = new BinaryFormatter();
			Stream stream;
			object obj;
			if (filePath != null)
			{
				stream = new FileStream(filePath,FileMode.Open,FileAccess.Read, FileShare.None);
			}
			else
			{
				stream = new FileStream("random.bin",FileMode.Open);
			}
			obj = formatter.Deserialize(stream);
			stream.Close();
			return obj;
		}

		public void Copy()
		{
			ConvertToStream(Selection);
		}

		public void Paste()
		{
			List<Shape> copied = (List<Shape>)ConvertStream();
			ShapeList.AddRange(copied);
		}

		public void ClearShapeList()
		{			
			ShapeList.Clear();			
		}

		public void DeleteSelected()
		{
			foreach (var item in Selection)
			{
				ShapeList.Remove(item);
			}
		}
	

	}
}
