using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{
    public class KnifeToolShape
    {
        private GroupSceneNode _VisibleNode;
        public GroupSceneNode VisualNode
        {
            get
            {
                if (_VisibleNode == null)
                {
                    _VisibleNode = CreateGeometry();
                    UpdateKnifeTool(new KnifeToolInstance());
                }
                   
                return _VisibleNode;
            }
        }

        public void UpdateKnifeTool(KnifeToolInstance tool)
        {
            Matrix4 translate = GlobalInstance.MatrixBuilder.MakeTranslate(tool.Position - tool.Direction * tool.ZSafeHeight);
            VisualNode.SetTransform(translate);
        }

        GroupSceneNode CreateGeometry()
        {
            var shapeNode = new GroupSceneNode();
            shapeNode.SetPickable(false);

            PhongMaterial material = GlobalInstance.Application.FindSystemMaterial("obsidian");
            FaceStyle fs = new FaceStyle();
            fs.SetMaterial(material);
            shapeNode.SetFaceStyle(fs);

            float bottomHeight = 10;
            TopoShape bottom = GlobalInstance.BrepTools.MakeCylinder(Vector3.ZERO, Vector3.UNIT_Z, 5, bottomHeight, 0);
            TopoShape top = GlobalInstance.BrepTools.MakeCylinder(new Vector3(0, 0, bottomHeight), Vector3.UNIT_Z, 10, 15, 0);

            shapeNode.AddNode(GlobalInstance.TopoShapeConvert.ToEntityNode(bottom, 0.1f));
            shapeNode.AddNode(GlobalInstance.TopoShapeConvert.ToEntityNode(top, 0.1f));

           

            return shapeNode;
        }
    }
}
