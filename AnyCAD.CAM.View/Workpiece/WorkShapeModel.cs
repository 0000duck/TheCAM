using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AnyCAD.CAM.Data;

namespace AnyCAD.CAM.View
{

    /// <summary>
    /// 
    /// </summary>
    public class WorkShapeModel : INotifyPropertyChanged
    {
        public WorkShape ActiveWorkShape{set; get;}
        public Workspace ActiveWorkspace { set; get; }
        public KnifeToolInstance ActiveKnifeTool { set; get; }
        public KnifeToolShape KnifeShape { set; get; } 
        public String ModelFileFilter { set; get; }
        public MachineTask ActiveTask
        {
            get { return TaskModel.CurrentTask;  }
        }

        public TheTaskModel TaskModel { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WorkShapeModel()
        {
            ActiveWorkspace = new Workspace();
            ActiveKnifeTool = new KnifeToolInstance();

            TaskModel = new TheTaskModel();
            TaskModel.TaskChanged += OnTaskChanged;
        }

        public void BeginAddTask(WorkPathModel shapeModel)
        {
            MachineTask task = new MachineTask(shapeModel.Shape, ActiveKnifeTool);

            shapeModel.PathChanged += OnPathChanged;

            TaskModel.ThePath = shapeModel;
            TaskModel.CurrentTask = task;
            TaskModel.TheKnifeTool = new KnifeToolModel(ActiveKnifeTool);

            OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_ADD);
        }

        public void FinishAddTask()
        {
            ActiveWorkspace.AddTask(TaskModel.CurrentTask);

            ActiveKnifeTool = new KnifeToolInstance();
            KnifeShape.UpdateKnifeTool(ActiveKnifeTool);
        }

        public void CancelTask()
        {
            OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_DELETE);

            TaskModel.CurrentTask = null;
        }

        public void BeginEditTask(MachineTask task, WorkPathModel shapeModel)
        {
            ActiveKnifeTool = task.Tool;

            shapeModel.PathChanged += OnPathChanged;

            TaskModel.ThePath = shapeModel;
            TaskModel.CurrentTask = task;
            TaskModel.TheKnifeTool = new KnifeToolModel(ActiveKnifeTool);

            KnifeShape.UpdateKnifeTool(ActiveKnifeTool);

            OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_MODIFIED);
        }
        public void FinishEditTask()
        {
            ActiveKnifeTool = new KnifeToolInstance();
            KnifeShape.UpdateKnifeTool(ActiveKnifeTool);

        }
        #region EVENT
        public event ModelChangeEventHandler ModelChanged;
        public void OnModelChanged(EnumModelType mt, EnumDataChange dc)
        {
            if (ModelChanged != null)
            {
                ModelChanged(this, new ModelChangeEvent(mt, dc));
            }
        }

        public void OnTaskChanged(Object sender)
        {
            ActiveTask.UpdateGeometry();
            
            KnifeShape.UpdateKnifeTool(ActiveKnifeTool);
            OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_MODIFIED);
        }

        public void OnPathChanged(Object sender)
        {
            ActiveTask.UpdateGeometry();
            OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_MODIFIED);
        }
        #endregion
        public virtual bool LoadWorkShape(string fileName, string barName)
        {
            return false;
        }

        public bool LoadWorkspace(Workspace ws)
        {
            if (ws.WorkShapeList.Count < 1)
                return false;

            foreach(SectionBar sb in ws.WorkShapeList)
            {
                sb.Load();
            }

            ActiveWorkShape = ws.WorkShapeList[0];
            ActiveWorkspace = ws;
            OnModelChanged(EnumModelType.MT_WorkShape, EnumDataChange.DC_ADD);

            foreach(MachineTask task in ws.TaskList)
            {
                TaskModel.CurrentTask = task;
                OnModelChanged(EnumModelType.MT_Task, EnumDataChange.DC_ADD);
            }

            if (KnifeShape == null)
            {
                KnifeShape = new KnifeToolShape();
                KnifeShape.UpdateKnifeTool(ActiveKnifeTool);
                OnModelChanged(EnumModelType.MT_KnifeTool, EnumDataChange.DC_ADD);
            }

            return true;
        }

    }
}
