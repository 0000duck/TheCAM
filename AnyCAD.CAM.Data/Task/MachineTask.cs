using AnyCAD.CAM.Data.Path;
using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class MachineTask
    {
        public WorkPath Path { get; set; }
        public PathLayout Layout { get; set; }
        public int Id { get; set; }
        public bool CompleteOrByPath { get; set; }
        public String LogMessage { get; set; }

        public KnifeToolInstance Tool { get; set; }



        public MachineTask()
        {
            Path = new WorkPath();
            Layout = new PathLayout();
            Id = 0;
        }

        public MachineTask(WorkPath shape, KnifeToolInstance drill)
        {
            Tool = drill;
            Path = shape;
            Layout = new PathLayout();
            CompleteOrByPath = true;

        }

        [NonSerialized]
        private GroupSceneNode _VisibleNode;
        public GroupSceneNode VisualNode
        {
            get
            {
                if (_VisibleNode == null)
                    UpdateGeometry();
                return _VisibleNode;
            }
        }

        public void UpdateGeometry()
        {
            if(_VisibleNode == null)
            {
                _VisibleNode = new GroupSceneNode();
                FaceStyle fs = new FaceStyle();
                fs.SetColor(new ColorValue(0, 0, 1.0f, 0.5f));
                fs.SetTransparent(true);
                _VisibleNode.SetFaceStyle(fs);
            }

            _VisibleNode.ClearAll();

            Path.BuildGeometry();

            TopoShape start = GlobalInstance.BrepTools.Extrude(Path.Section, Tool.ZStart, Vector3.UNIT_Z);
            TopoShape end = GlobalInstance.BrepTools.Extrude(Path.Section, Tool.ZEnd, Vector3.UNIT_Z);

            _VisibleNode.AddNode(GlobalInstance.TopoShapeConvert.ToEntityNode(start, 0.1f));
            _VisibleNode.AddNode(GlobalInstance.TopoShapeConvert.ToEntityNode(end, 0.1f));

            Matrix4 trf = GlobalInstance.MatrixBuilder.MakeTranslate(Tool.Position);

            _VisibleNode.SetTransform(trf);
        }
    }
}
