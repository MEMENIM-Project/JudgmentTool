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

namespace JudgmentTool.Widgets
{
    /// <summary>
    /// Interaction logic for MessageWidget.xaml
    /// </summary>
    public partial class MessageWidget : UserControl
    {
        public MessageData Message { get; set; }

        public string ImageSource { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }

        public MessageWidget()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Message;
        }
    }
}
