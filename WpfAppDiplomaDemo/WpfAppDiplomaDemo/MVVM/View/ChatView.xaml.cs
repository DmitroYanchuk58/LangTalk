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
using WpfAppDiplomaDemo.MVVM.ViewModel;

namespace WpfAppDiplomaDemo.MVVM.View
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ChatViewModel)DataContext;
            var textMessage = MessageTB.Text;
            if (!string.IsNullOrWhiteSpace(textMessage))
            {
                vm.SendMessage(textMessage);
            }
        }
    }
}
