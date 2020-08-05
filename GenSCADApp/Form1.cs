using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
//using System.Diagnostics;



namespace GenSCADApp
{
	public partial class ScadConstructorForm : Form
	{
		public string[] param = System.IO.File.ReadAllLines(@"Parametrs.sys", Encoding.UTF8);

		private string ReplaceSeparator(string value)
		{
			string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			return value.Replace(",", dec_sep).Replace(".", dec_sep);
		}

		public ScadConstructorForm()
		{
			InitializeComponent();

			TextBox_H_FERM.Text = ReplaceSeparator(param[1]);
			TextBox_L_FERM.Text = ReplaceSeparator(param[3]);
			TextBox_COUNT_PANELS.Text = ReplaceSeparator(param[5]);
			TextBox_RAD.Text = ReplaceSeparator(param[7]);
			TextBox_d_RAM.Text = ReplaceSeparator(param[9]);
			TextBox_Hk.Text= ReplaceSeparator(param[11]);
			TextBox_Np.Text = ReplaceSeparator(Convert.ToString(Convert.ToInt32(param[13]) - 1));
			TextBox_Q1.Text = ReplaceSeparator(param[15]);
			TextBox_Q2.Text = ReplaceSeparator(param[17]);
			TextBox_Q3.Text = ReplaceSeparator(param[19]);

			ComboBox_SnowDistrict.SelectedIndex = Convert.ToInt32(ReplaceSeparator(param[21])) - 1;
			ComboBox_WindDistrict.SelectedIndex = Convert.ToInt32(ReplaceSeparator(param[23]));
			ComboBox_TypeArea.SelectedIndex = Convert.ToInt32(ReplaceSeparator(param[25]));

			TextBox_Count_PRG.Text = ReplaceSeparator(param[27]);
			TextBox_KF.Text = ReplaceSeparator(param[29]);
			TextBox_HR.Text = ReplaceSeparator(param[31]);

			ComboBox_Type.SelectedIndex = Convert.ToInt32(ReplaceSeparator(param[33]));

			TextBox_Kw.Text = ReplaceSeparator(param[35]);

			TextBox_L.Text = Convert.ToString(Convert.ToDouble(TextBox_d_RAM.Text) * Convert.ToDouble(TextBox_Np.Text));
		}

		private void StartButton_Click(object sender, EventArgs e)
		{
			param[1] = ReplaceSeparator(TextBox_H_FERM.Text);
			param[3] = ReplaceSeparator(TextBox_L_FERM.Text);
			param[5] = ReplaceSeparator(TextBox_COUNT_PANELS.Text);
			param[7] = ReplaceSeparator(TextBox_RAD.Text);
			param[9] = ReplaceSeparator(TextBox_d_RAM.Text);
			param[11] = ReplaceSeparator(TextBox_Hk.Text);
			param[13] = ReplaceSeparator(Convert.ToString(Convert.ToInt32(TextBox_Np.Text)+1));
			param[15] = ReplaceSeparator(TextBox_Q1.Text);
			param[17] = ReplaceSeparator(TextBox_Q2.Text);
			param[19] = ReplaceSeparator(TextBox_Q3.Text);
			param[21] = ReplaceSeparator(Convert.ToString(ComboBox_SnowDistrict.SelectedIndex + 1));
			param[23] = ReplaceSeparator(Convert.ToString(ComboBox_WindDistrict.SelectedIndex));
			param[25] = ReplaceSeparator(Convert.ToString(ComboBox_TypeArea.SelectedIndex));
			param[27] = ReplaceSeparator(TextBox_Count_PRG.Text);
			param[29] = ReplaceSeparator(TextBox_KF.Text);
			param[31] = ReplaceSeparator(TextBox_HR.Text);
			param[33] = ReplaceSeparator(Convert.ToString(ComboBox_Type.SelectedIndex));
			param[35] = ReplaceSeparator(TextBox_Kw.Text);

			System.IO.File.WriteAllLines(@"Parametrs.sys", param);

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			//p.StartInfo.FileName = @"C:\\Users\maxim\source\repos\SCAD_API1_Create_Ferms\Release\SCAD_API1_Create_Ferms.exe";
			p.StartInfo.FileName = @"SCAD_API1_Create_Ferms.exe";
			p.Start();
		}

		

		private void ComboBox_SnowDistrict_SelectedIndexChanged(object sender, EventArgs e)
		{
			StartButton.Focus();
		}

		private void ComboBox_SnowDistrict_DrawItem(object sender, DrawItemEventArgs e)
		{
			// By using Sender, one method could handle multiple ComboBoxes
			ComboBox cbx = sender as ComboBox;
			if (cbx != null)
			{
				// Always draw the background Всегда рисовать фон
				e.DrawBackground();

				// Drawing one of the items?
				if (e.Index >= 0)
				{
					// Set the string alignment.  Choices are Center, Near and Far Задайте выравнивание строки.  Выбор - Center, Near and Far
					StringFormat sf = new StringFormat
					{
						LineAlignment = StringAlignment.Center,
						Alignment = StringAlignment.Center
					};

					// Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings Установить кисти в ComboBox ForeColor для поддержки любых параметров цвета ComboBox 
					// Assumes Brush is solid предполагается, что кисть является твердой
					Brush brush = new SolidBrush(cbx.ForeColor);

					// If drawing highlighted selection, change brush Если в чертеже присутствует выделение, измените кисть
					if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
					brush = SystemBrushes.HighlightText;

					// Draw the string Окрашивание строки
					e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
				}
			}
		}

		private void TextBox_H_FERM_TextChanged(object sender, EventArgs e)
		{
			try
			{
				TextBox_L.Text = Convert.ToString(Convert.ToDouble(TextBox_d_RAM.Text) * Convert.ToDouble(TextBox_Np.Text));
			}
			catch (Exception)
			{
				
			}
		}
	}
}
