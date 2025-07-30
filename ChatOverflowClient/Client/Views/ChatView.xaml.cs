using Client.ViewModels;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Client.Views
{
    /// <summary>
    /// ChatView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView(ChatViewModel viewModel)
        {
            InitializeComponent();
            //viewModel = new ChatViewModel(name, seat);
            this.DataContext = viewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
