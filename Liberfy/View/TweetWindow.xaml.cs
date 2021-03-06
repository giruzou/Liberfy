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
using System.Windows.Shapes;

namespace Liberfy.View
{
	/// <summary>
	/// TweetWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class TweetWindow : Window
	{
		public TweetWindow()
		{
			InitializeComponent();
		}

		public TweetWindow(object parameter) : this()
		{
			var viewModel = DataContext as ViewModel.TweetWindow;

			if (parameter is Account account)
			{
				viewModel?.SetPostAccount(account);
			}
		}
	}
}
