using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.CAM.Data.Geometry;

namespace AnyCAD.CAM.Data.Path
{

    [Serializable]
    public class KeyPath : WorkPath
    {
        public float Radius { get; set; }
        public float Width { get; set; }

        public KeyPath()
        {
            Radius = 10;
            Width = 1;
        }

        protected override bool BuildShapes()
        {
            Loops.Clear();

            LineShape line1 = new LineShape();
            ArcShape arc1 = new ArcShape();
            LineShape line2 = new LineShape();
            ArcShape arc2 = new ArcShape();

            AddShape(line1);
            AddShape(arc1);
            AddShape(line2);
            AddShape(arc2);

            return true;
        }

    }
}
