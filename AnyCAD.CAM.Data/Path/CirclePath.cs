using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.CAM.Data.Geometry;

namespace AnyCAD.CAM.Data.Path
{
    [Serializable]
    public class CirclePath : WorkPath
    {
        public float Radius { get; set; }
        public CirclePath()
        {
            Radius = 10;
            Icon = @"Images\Work\Hole.png";
        }

        protected override bool BuildShapes()
        {
            Loops.Clear();

            ArcShape circle = new ArcShape();
            circle.Radius = Radius;

            AddShape(circle);
            return true;
        }
    }
}
