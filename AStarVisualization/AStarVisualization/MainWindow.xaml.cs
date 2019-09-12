﻿using AStarVisualization.WPF.WPF;
using System.Windows;

namespace AStarVisualization.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAStarVisualization();
        }

        private void InitializeAStarVisualization()
        {
            var aStarVisualizer = new AStarVisualizer(this);
        }
    }
}