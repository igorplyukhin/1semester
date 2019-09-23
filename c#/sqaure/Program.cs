using System;
using System.Diagnostics;
using System.Drawing;

namespace RefactorMe
{
	// ## Прочитайте! ##
	//
	// Ваша задача привести код в этом файле в порядок. 
	// Для начала запустите эту программу. Для этого в VS в проект подключите сборку System.Drawing.
	
	// Переименуйте всё, что называется неправильно. Это можно делать комбинацией клавиш Ctrl+R, Ctrl+R (дважды нажать Ctrl+R).
	// Повторяющиеся части кода вынесите во вспомогательные методы. Это можно сделать выделив несколько строк кода и нажав Ctrl+R, Ctrl+M
	// Избавьтесь от всех зашитых в коде числовых констант — положите их в переменные с понятными именами.
	// 
	// После наведения порядка проверьте, что ваш код стал лучше. 
	// На сколько проще после ваших переделок стало изменить размер фигуры? Повернуть её на некоторый угол? 
	// Научиться рисовать невозможный треугольник, вместо квадрата?

	class Drawer
	{
		static Bitmap image = new Bitmap(800, 600);
		static float x, y;
		static Graphics graphics;

		public static void Initialize()
		{
			image = new Bitmap(800, 600);
			graphics = Graphics.FromImage(image);
		}

		public static void SetPosition(float x0, float y0)
		{
			x = x0;
			y = y0;
		}

		public static void Move(double L, double angle)
		{
			//Делает шаг длиной L в направлении angle и рисует пройденную траекторию
			var x1 = (float)(x + L * Math.Cos(angle));
			var y1 = (float)(y + L * Math.Sin(angle));
			graphics.DrawLine(Pens.Yellow, x, y, x1, y1);
			x = x1;
			y = y1;
		}

		public static void ShowResult()
		{
			image.Save("result.bmp");
			Process.Start("result.bmp");
		}
	}

	public class Figure
	{
		public static void Main()
		{
			Drawer.Initialize();

			//Рисуем четыре одинаковые части невозможного квадрата.
			// Часть первая:
			Drawer.set_pos(10, 0);
			Drawer.Move(100, 0);
			Drawer.Move(10 * Math.Sqrt(2), Math.PI / 4);
			Drawer.Move(100, Math.PI);
			Drawer.Move(100 - (double) 10, Math.PI / 2);

			// Часть вторая:
			Drawer.set_pos(120, 10);
			Drawer.Move(100, Math.PI / 2);
			Drawer.Move(10 * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
			Drawer.Move(100, Math.PI / 2 + Math.PI);
			Drawer.Move(100 - (double) 10, Math.PI / 2 + Math.PI / 2);

			// Часть третья:
			Drawer.set_pos(110, 120);
			Drawer.Move(100, Math.PI);
			Drawer.Move(10 * Math.Sqrt(2), Math.PI + Math.PI / 4);
			Drawer.Move(100, Math.PI + Math.PI);
			Drawer.Move(100 - (double) 10, Math.PI + Math.PI / 2);

			// Часть четвертая:
			Drawer.set_pos(0, 110);
			Drawer.Move(100, -Math.PI / 2);
			Drawer.Move(10 * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
			Drawer.Move(100, -Math.PI / 2 + Math.PI);
			Drawer.Move(100 - (double) 10, -Math.PI / 2 + Math.PI / 2);

			Drawer.ShowResult();
		}
	}
}
