﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Liberfy
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			switch(RenderCapability.Tier >> 16)
			{
				case 0:
					Title += " [SWレンダリング]"; break;

				case 1:
					Title += " [HWレンダリング(制限)]"; break;

				case 2:
					Title += " [HWレンダリング]"; break;
			}
		}
	}
}
