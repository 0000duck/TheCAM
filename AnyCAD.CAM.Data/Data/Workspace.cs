using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{


    [Serializable]
    public class Workspace
    {
        public List<WorkShape> WorkShapeList { get; set;}
        public List<MachineTask> TaskList { get; set; }
   
        public Workspace()
        {
            TaskList = new List<MachineTask>();
            WorkShapeList = new List<WorkShape>();
        }

        public void AddWorkShape(SectionBar sb)
        {
            WorkShapeList.Add(sb);
        }

        public void AddTask(MachineTask task)
        {
            task.Id = TaskList.Count;
            task.UpdateGeometry();
            TaskList.Add(task);
        }
    }
}
