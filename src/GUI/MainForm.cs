using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				Shape temp = dialogProcessor.ContainsPoint(e.Location);
				if (temp != null)
				{
					if (!dialogProcessor.Selection.Contains(temp))
					{
						if (dialogProcessor.Groups.Any(w => w.Grouped.Contains(temp)))
						{
							dialogProcessor.Selection.AddRange(dialogProcessor.Groups.First(w => w.Grouped.Contains(temp)).Grouped);
						}
						else
						{
							dialogProcessor.Selection.Add(temp);
						}

					}
					else
					{
						if (dialogProcessor.Groups.Any(w => w.Grouped.Contains(temp)))
						{
							foreach (var item in dialogProcessor.Groups.First(w => w.Grouped.Contains(temp)).Grouped)
							{
								dialogProcessor.Selection.Remove(item);
							}
						}
						else
						{
							dialogProcessor.Selection.Remove(temp);
						}
					}
				}

				if (dialogProcessor.Selection != null)
				{
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void elipseStrButton_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomEllipse();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

        private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void triangle_Button(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomTriangle();

			statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

			viewPort.Invalidate();
		}

        private void test_Figure_Button(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomTest();

			statusBar.Items[0].Text = ("Последно действие: Рисуване на фигура");
			viewPort.Invalidate();
		}

        private void fillColor_Button(object sender, EventArgs e)
        {
			ColorDialog dlg = new ColorDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				
				foreach (Shape item in dialogProcessor.Selection)
				{
					item.FillColor = dlg.Color;
				}

			}
			statusBar.Items[0].Text = ("Последно действие: Смяна на цвят");
			viewPort.Invalidate();
		}

        private void outlineColorChanger(object sender, EventArgs e)
        {
			ColorDialog dlg = new ColorDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{

				foreach (Shape item in dialogProcessor.Selection)
				{
					item.OutlineColor = dlg.Color;
				}

			}
			statusBar.Items[0].Text = ("Последно действие: Смяна на цвят");
			viewPort.Invalidate();
		}

        private void Height(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void heightBox(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
			foreach (Shape item in dialogProcessor.Selection)
			{
				if (toolStripTextBox1.Text != string.Empty)
				{
					item.Height = int.Parse(toolStripTextBox1.Text);
				}
			}
			statusBar.Items[0].Text = ("Последно действие: Промяна на височина");
			viewPort.Invalidate();
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
			foreach (Shape item in dialogProcessor.Selection)
			{
				if (toolStripTextBox2.Text != string.Empty)
				{
					item.Width = int.Parse(toolStripTextBox2.Text);
				}
			}
			statusBar.Items[0].Text = ("Последно действие: Промяна на дебелина");
			viewPort.Invalidate();
		}

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
			foreach (Shape item in dialogProcessor.Selection)
			{
				if (toolStripTextBox3.Text != string.Empty)
				{
					item.OutlineWidth = int.Parse(toolStripTextBox3.Text);
				}
			}
			statusBar.Items[0].Text = ("Последно действие: Промяна на дебелина контур");
			viewPort.Invalidate();
		}

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
			foreach (var item in dialogProcessor.Selection)
			{
				
				item.Opacity = trackBar1.Value;

			}
			statusBar.Items[0].Text = ("Последно действие: Смяна на прозрачност");
			viewPort.Invalidate();
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void copy(object sender, EventArgs e)
        {
			dialogProcessor.Copy();
			statusBar.Items[0].Text = ("Последно действие: Copy");
		}

        private void paste(object sender, EventArgs e)
        {
			
			dialogProcessor.Paste();
			statusBar.Items[0].Text = ("Последно действие: Paste");
			
		}

        private void removeSelected(object sender, EventArgs e)
        {
			dialogProcessor.DeleteSelected();
			statusBar.Items[0].Text = ("Последно действие: Delete Selected");
		}

        private void clearAll(object sender, EventArgs e)
        {
			dialogProcessor.ClearShapeList();
			statusBar.Items[0].Text = ("Последно действие: Clear All");
		}

        private void saveOption(object sender, EventArgs e)
        {
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{

				DialogProcessor.ConvertToStream(dialogProcessor.ShapeList, dlg.FileName);
			}
			statusBar.Items[0].Text = ("Последно действие: Saving file");
		}

        private void loadOption(object sender, EventArgs e)
        {
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.ShapeList = (List<Shape>)DialogProcessor.ConvertStream(dlg.FileName);
				viewPort.Invalidate();
			}
			statusBar.Items[0].Text = ("Последно действие: Loading file");
		}

        private void selectGroup(object sender, EventArgs e)
        {
            foreach (var item in dialogProcessor.ShapeList)
            {
                if (item.GetType() == dialogProcessor.Selection[0].GetType())
                {
					dialogProcessor.Selection.Add(item);

				}
            }
			statusBar.Items[0].Text = ("Последно действие: Select group");
		}

        private void delselectAll(object sender, EventArgs e)
        {
			dialogProcessor.Selection.Clear();
			statusBar.Items[0].Text = ("Последно действие: Deselect group");
		}

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void Selector(object sender, EventArgs e)
        {
			var gr = new GroupShape();
			foreach (var gr1 in dialogProcessor.Selection)
			{
				if (gr1.GroupedIs)
				{
					GroupShape.Degroup(dialogProcessor.Groups.First(w => w.Grouped.Contains(gr1)));
					gr.Grouped.Add(gr1);
				}
				if (!gr1.GroupedIs)
				{
					if (!gr.Grouped.Contains(gr1))
					{
						gr1.GroupedIs = true;
						gr.Grouped.Add(gr1);
					}
				}
			}
			dialogProcessor.Groups.Add(gr);
		}

        private void unGrp(object sender, EventArgs e)
        {
			foreach (var gr in dialogProcessor.Selection.ToList())
			{
				if (gr.GroupedIs)
				{
					GroupShape.Degroup(dialogProcessor.Groups.FirstOrDefault(x => x.Grouped.Contains(gr)));
					break;
				}
			}
		}


    }
}
