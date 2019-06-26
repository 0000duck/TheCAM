using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace TheCAM
{
    class TheCommands
    {
        public static readonly RoutedUICommand LoadModel = new RoutedUICommand("Load Model", "LoadModel", typeof(MainWindow));
        public static readonly RoutedUICommand Export = new RoutedUICommand("Export", "Export", typeof(MainWindow));
        public static readonly RoutedUICommand AddCircleHole = new RoutedUICommand("Circle", "AddCircleHole", typeof(MainWindow));
        public static readonly RoutedUICommand Pick = new RoutedUICommand("Pick", "Pick", typeof(MainWindow));
        public static readonly RoutedUICommand ZoomToFit = new RoutedUICommand("ZoomToFit", "ZoomToFit", typeof(MainWindow));
        public static readonly RoutedUICommand ZoomOut = new RoutedUICommand("ZoomOut", "ZoomOut", typeof(MainWindow));
        public static readonly RoutedUICommand ZoomIn = new RoutedUICommand("ZoomIn", "ZoomIn", typeof(MainWindow));
        public static readonly RoutedUICommand ViewWH = new RoutedUICommand("ViewWH", "ViewWH", typeof(MainWindow));
        public static readonly RoutedUICommand ViewXH = new RoutedUICommand("ViewXH", "ViewXH", typeof(MainWindow));
        public static readonly RoutedUICommand View3D = new RoutedUICommand("View3D", "View3D", typeof(MainWindow));
    }
}
