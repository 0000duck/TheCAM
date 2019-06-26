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
using System.Windows.Controls.Ribbon;
using Microsoft.Win32;

using AnyCAD.CAM.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AnyCAD.CAM.Data;
using AnyCAD.CAM.Data.Path;
namespace TheCAM
{
    class TreeNodeItem
    {
        public string Icon { get; set; }
        public string DisplayName { get; set; }
        public Object Tag { get; set; }

        public ObservableCollection<TreeNodeItem> Children { get; set; }

        public TreeNodeItem()
        {
            Children = new ObservableCollection<TreeNodeItem>();
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private WorkShapeModel m_ModelView;
        private ObservableCollection<TreeNodeItem> m_ItemList = new ObservableCollection<TreeNodeItem>();
        private AnyCAD.CAM.View.ModelView3d m_View3d = null;
        private ObservableCollection<KnifeTool> m_StockedDrills;

        private PathShapeUIFactory m_UIFactory;
        public MainWindow()
        {
            InitializeComponent();

            m_ModelView = new SectionBarModel();
            mainGrid.DataContext = m_ModelView;

            projectBrowser.ItemsSource = m_ItemList;
            
            // commands
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSave));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpen));

            CommandBindings.Add(new CommandBinding(TheCommands.LoadModel, OnLoadModel));
            CommandBindings.Add(new CommandBinding(TheCommands.Export, OnExport));

            CommandBindings.Add(new CommandBinding(TheCommands.AddCircleHole, OnAddCircleHole));
            
            CommandBindings.Add(new CommandBinding(TheCommands.ZoomToFit, OnZoomToFit));
            CommandBindings.Add(new CommandBinding(TheCommands.ZoomOut, OnZoomOut));
            CommandBindings.Add(new CommandBinding(TheCommands.ZoomIn, OnZoomIn));
            CommandBindings.Add(new CommandBinding(TheCommands.ViewWH, OnViewWH));
            CommandBindings.Add(new CommandBinding(TheCommands.ViewXH, OnViewXH));
            CommandBindings.Add(new CommandBinding(TheCommands.View3D, OnView3D));

            m_StockedDrills = new ObservableCollection<KnifeTool>();
            m_StockedDrills.Add(new KnifeTool());
            KnifeTool dp2 = new KnifeTool();
            dp2.Name = "2";
            dp2.RotateSpeed = 15000;
            m_StockedDrills.Add(dp2);

            m_UIFactory = new PathShapeUIFactory();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            m_View3d = new AnyCAD.CAM.View.ModelView3d();
            this.renderHost.Child = m_View3d;

            m_ModelView.ModelChanged += m_View3d.OnModelChanged;
            m_ModelView.ModelChanged += this.OnModelChanged;

            OnViewXH(this, null);
        }

        public void OnModelChanged(object sender, ModelChangeEvent e)
        {
            if (e.changeType == EnumDataChange.DC_ADD)
            {
                if(e.modelType == EnumModelType.MT_WorkShape)
                {
                    WorkShapeModel mv = sender as WorkShapeModel;

                    TreeNodeItem rootItem = new TreeNodeItem()
                    {
                        Icon = @"Images\Project\Bar.png",
                        DisplayName = "工件"
                    };

                    TreeNodeItem dxfItem = new TreeNodeItem()
                    {
                        Icon = @"Images\Start\LoadDXF.png",
                        DisplayName = mv.ActiveWorkShape.Name
                    };
                    rootItem.Children.Add(dxfItem);
                    m_ItemList.Add(rootItem);

                    TreeNodeItem toolItem = new TreeNodeItem()
                    {
                        Icon = @"Images\Project\Tool.png",
                        DisplayName = "加工"
                    };
                    m_ItemList.Add(toolItem);
                }
                else if(e.modelType == EnumModelType.MT_Task)
                {
                    TreeNodeItem rootItem = new TreeNodeItem()
                    {
                        Icon = m_ModelView.ActiveTask.Path.Icon,
                        DisplayName = String.Format("#{0}", m_ModelView.ActiveTask.Id),
                        Tag = m_ModelView.ActiveTask
                    };
                    m_ItemList[1].Children.Add(rootItem);
                }
            }
            else if(e.changeType == EnumDataChange.DC_DELETE)
            {
                if(e.modelType == EnumModelType.MT_Task)
                {
                    foreach(TreeNodeItem item in m_ItemList[1].Children)
                    {
                        if(item.Tag == m_ModelView.ActiveTask)
                        {
                            m_ItemList[1].Children.Remove(item);
                            break;
                        }
                    }
                }
            }
            
        }

        void OnNew(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("New...");
        }
        void OnSave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "TheCAM (*.cam)|*.cam";
            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            ClassFactory.Instance().Save(m_ModelView.ActiveWorkspace, dlg.FileName);
        }
        void OnOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "TheCAM (*.cam)|*.cam";

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            m_ItemList.Clear();
            Workspace ws =  ClassFactory.Instance().Load(dlg.FileName);
            if(ws == null)
            {
                MessageBox.Show("读取文件失败!");
                return;
            }

            m_ModelView.LoadWorkspace(ws);
        }
        void OnLoadModel(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = m_ModelView.ModelFileFilter;

            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            m_ModelView.LoadWorkShape(dlg.FileName, dlg.SafeFileName);
        }

        void OnExport(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML (*.xml)|*.xml";
            if (!dlg.ShowDialog().GetValueOrDefault())
                return;

            ClassFactory.Instance().Save(m_ModelView.ActiveWorkspace, dlg.FileName);
        }
        void OnAddCircleHole(object sender, RoutedEventArgs e)
        {
            BeginAddTask(new CirclePath());
        }

        void BeginAddTask(WorkPath ps)
        {
            propertyBrowser.Children.Clear();

            PathUIDef UIDef = m_UIFactory.FindUI(ps.GetType());
            if (UIDef != null)
            {
                m_ModelView.BeginAddTask(UIDef.CreateShapeModel(ps));
                CreateTaskUI(UIDef.CreateUI(), UIDef.Title, UIMode.UM_Add);
            }
        }

        void BeginEditTask(MachineTask ts)
        {
            propertyBrowser.Children.Clear();

            PathUIDef UIDef = m_UIFactory.FindUI(ts.Path.GetType());
            if (UIDef != null)
            {
                m_ModelView.BeginEditTask(ts, UIDef.CreateShapeModel(ts.Path));
                CreateTaskUI(UIDef.CreateUI(), UIDef.Title, UIMode.UM_Edit);
            }
        }

        enum UIMode
        {
            UM_Add,
            UM_Edit,
        }
        void CreateTaskUI(UserControl shapeUI, String title, UIMode mode)
        {
            {
                Expander expander = new Expander();
                expander.Header = "加工";
                TaskStepSetPath ui = new TaskStepSetPath();
                ui.DataContext = m_ModelView.TaskModel;
                expander.Content = ui;
                propertyBrowser.Children.Add(expander);
            }

            {
                Expander expander = new Expander();
                expander.Header = title;
                shapeUI.DataContext = m_ModelView.TaskModel.ThePath;
                expander.Content = shapeUI;
                propertyBrowser.Children.Add(expander);
                expander.IsExpanded = true;
            }
            {
                Expander expander = new Expander();
                expander.Header = "刀具";

                TaskStepDrill ui = new TaskStepDrill(m_StockedDrills);
                ui.DataContext = m_ModelView.TaskModel.TheKnifeTool;
                expander.Content = ui;
                propertyBrowser.Children.Add(expander);

            }
            {
                Expander expander = new Expander();
                expander.Header = "WHX";

                TaskStepHoleLayout ui = new TaskStepHoleLayout();
                ui.DataContext = m_ModelView.TaskModel;
                expander.Content = ui;
                propertyBrowser.Children.Add(expander);
                //expander.IsExpanded = true;
            }

            {
                Expander expander = new Expander();
                expander.Header = "Z值";

                TaskStepValueZ ui = new TaskStepValueZ();
                ui.DataContext = m_ModelView.TaskModel;
                expander.Content = ui;
                propertyBrowser.Children.Add(expander);
            }

            if(mode == UIMode.UM_Add)
            {

                {
                    Button button = new Button();
                    button.Content = "添加";
                    button.Click += button_AddTask;
                    propertyBrowser.Children.Add(button);
                }
                {
                    Button button = new Button();
                    button.Content = "取消";
                    button.Click += button_CancelTask;
                    propertyBrowser.Children.Add(button);
                }
            }
            else
            {
                {
                    Button button = new Button();
                    button.Content = "完成";
                    button.Click += button_EditTask;
                    propertyBrowser.Children.Add(button);
                }
            }
        }

        void button_AddTask(object sender, RoutedEventArgs e)
        {
            m_ModelView.FinishAddTask();

            propertyBrowser.Children.Clear();
        }

        void button_CancelTask(object sender, RoutedEventArgs e)
        {
            m_ModelView.CancelTask();
            propertyBrowser.Children.Clear();
        }

        void button_EditTask(object sender, RoutedEventArgs e)
        {
            m_ModelView.FinishEditTask();
            propertyBrowser.Children.Clear();
        }

        #region VIEW_COMMANDS
        void OnZoomToFit(object sender, RoutedEventArgs e)
        {
            m_View3d.ZoomToFit();
        }
        void OnZoomOut(object sender, RoutedEventArgs e)
        {
            m_View3d.Zoom(50);
        }
        void OnZoomIn(object sender, RoutedEventArgs e)
        {
            m_View3d.Zoom(-50);
        }
        void OnViewWH(object sender, RoutedEventArgs e)
        {
            m_View3d.SetViewDirection(EnumViewDirection.VD_WH);
        }
        void OnViewXH(object sender, RoutedEventArgs e)
        {
            m_View3d.SetViewDirection(EnumViewDirection.VD_XH);
        }
        void OnView3D(object sender, RoutedEventArgs e)
        {
            m_View3d.SetViewDirection(EnumViewDirection.VD_3D);
        }
        #endregion

        private void projectBrowser_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeNodeItem item = this.projectBrowser.SelectedItem as TreeNodeItem;
            if (item == null)
                return;

            MachineTask task = item.Tag as MachineTask;
            if(task != null)
            {
                BeginEditTask(task);
            }
        }
    }
}
