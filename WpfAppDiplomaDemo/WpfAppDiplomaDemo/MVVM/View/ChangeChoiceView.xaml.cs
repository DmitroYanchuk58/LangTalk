using System;
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
    /// Interaction logic for ChangeViewChoice.xaml
    /// </summary>
    public partial class ChangeChoiceView : UserControl
    {
        public ChangeChoiceView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ChangeChoiceViewModel)DataContext;
            vm.ChangeInfoViewShow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var vm = (ChangeChoiceViewModel)DataContext;
            vm.AddUserLanguageViewShow();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var vm = (ChangeChoiceViewModel)DataContext;
            vm.AddUserHobbyViewShow();
        }
    }
}
