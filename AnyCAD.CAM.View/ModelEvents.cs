using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.CAM.Data;

namespace AnyCAD.CAM.View
{
    public enum EnumDataChange
    {
        DC_ADD,
        DC_DELETE,
        DC_MODIFIED,
    }

    public enum EnumModelType
    {
        MT_WorkShape,
        MT_KnifeTool,
        MT_Task,
    }

    /// <summary>
    /// Model更新事件
    /// </summary>
    public class ModelChangeEvent : EventArgs
    {
        public readonly EnumModelType modelType;
        public readonly EnumDataChange changeType;
        public ModelChangeEvent(EnumModelType mt, EnumDataChange ct)
        {
            this.modelType = mt;
            this.changeType = ct;
        }
    }
    public delegate void ModelChangeEventHandler(object sender, ModelChangeEvent args);

    public delegate void TaskChangeEventHandler(object sender);

    public delegate void PathChangeEventHandler(object sender);

}
