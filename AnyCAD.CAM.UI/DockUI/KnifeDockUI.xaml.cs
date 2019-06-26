using AnyCAD.CAM.Data;
using AnyCAD.CAM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TheCAM
{
    /// <summary>
    /// Interaction logic for TaskStepDrill.xaml
    /// </summary>
    public partial class TaskStepDrill : UserControl
    {
        public TaskStepDrill(ObservableCollection<KnifeTool> tools)
        {
            InitializeComponent();


            drillList.ItemsSource = tools;
            drillList.SelectedValue = "1";
        }

        private void drillList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KnifeToolModel drillModel = DataContext as KnifeToolModel;
            if (drillModel == null)
                return;

            //ObservableCollection<DrillParam> drills = drillList.ItemsSource as ObservableCollection<DrillParam>;

            KnifeTool drillParam = drillList.SelectedItem as KnifeTool;
            drillModel.SetToolParam(drillParam);
        }
    }
}
