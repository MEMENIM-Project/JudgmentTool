using MahApps.Metro.IconPacks;
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
    /// Interaction logic for RoomSelectorWidget.xaml
    /// </summary>
    public partial class RoomSelectorWidget : UserControl
    {
        public string RoomID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public ERoomType RoomType { get; set; }

        public PackIconModernKind Kind { get; set; } = PackIconModernKind.SmileyHappy;

        public RoomSelectorWidget()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
