using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data
{
    public class SectionReaderContextDXF : AnyCAD.Platform.TopoShapeReaderContext
    {
        public List<TopoShape> Section { get; set; }

        public SectionReaderContextDXF()
        {
            Section = new List<TopoShape>();
        }


        public override void OnBeginGroup(String name)
        {
        }

        public override void OnEndGroup()
        {

        }

        public override bool OnBeiginComplexShape(TopoShape shape)
        {
            return true;
        }

        public override void OnEndComplexShape()
        {
      
        }

        public override void OnFace(TopoShape shape)
        {
 
        }

        public override void OnWire(TopoShape shape)
        {
            Section.Add(shape);
        }

        public override void OnEdge(TopoShape shape)
        {
            Section.Add(shape);
        }

        public override void OnPoint(TopoShape shape)
        {

        }
    }
}
