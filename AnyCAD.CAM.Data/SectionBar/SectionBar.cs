using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class SectionBar : WorkShape
    {

        public string FileName { get; set; }

        public float Length { get; set; }
        public Vector2 StartWH { get; set; }
        public Vector2 EndWH { get; set; }

        [NonSerialized]
        public List<TopoShape> Section;
        [NonSerialized]
        public GroupSceneNode VisualNode;

        public SectionBar()
        {
            Length = 1000;
            StartWH = new Vector2();
            EndWH = new Vector2();
        }

        public override GroupSceneNode GetVisualNode()
        {
            return VisualNode;
        }

        public override void UpdateGeometry()
        {
            VisualNode.ClearAll();

            Coordinate3 coord = new Coordinate3();
            coord.X = -Vector3.UNIT_Y;
            coord.Y = Vector3.UNIT_Z;
            coord.Z = Vector3.UNIT_X;

            Matrix4 trf = GlobalInstance.MatrixBuilder.ToWorldMatrix(coord);

            foreach (TopoShape edge in Section)
            {
                TopoShape shape = GlobalInstance.BrepTools.Extrude(edge, Length, -Vector3.UNIT_Z);
                shape = GlobalInstance.BrepTools.Transform(shape, trf);

                VisualNode.AddNode(GlobalInstance.TopoShapeConvert.ToSceneNode(shape, 0.01f));
            }
        }

        public override bool Load()
        {
            AnyCAD.Exchange.DxfReader reader = new AnyCAD.Exchange.DxfReader();
            SectionReaderContextDXF context = new SectionReaderContextDXF();
            if (!reader.Read(FileName, context, true))
                return false;

            if (context.Section.Count < 1)
                return false;

            Section = context.Section;
            VisualNode = new GroupSceneNode();
            VisualNode.SetPickable(false);

            UpdateGeometry();

            return true;
        }
    }
}
