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
    /// Interaction logic for AddUserLanguage.xaml
    /// </summary>
    public partial class AddUserLanguageView : UserControl
    {
        public AddUserLanguageView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AddUserLanguageViewModel)DataContext;
            var language = Languages.SelectedItem as string;
            var level = Levels.SelectedItem as string;
            var isSpoken = IsSpoken.IsChecked ?? false;
            vm.AddLanguage(language, level, isSpoken);
            vm.ChangeViewToProfile();
        }
    }
}
