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

namespace JudgmentTool.Widgets
{
    /// <summary>
    /// Interaction logic for AnonimProfileWidget.xaml
    /// </summary>
    public partial class AnonimProfileWidget : UserControl
    {
        public AnonymProfileData ProfileData { get; set; }

        public AnonimProfileWidget()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = ProfileData;
        }
    }
}
