using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data.Geometry
{
    /// <summary>
    /// 点
    /// </summary>
    [Serializable]
    public class PointShape : Shape
    {
        public Vector3 Position { get; set; }

        public PointShape()
        {
            Position = new Vector3();
        }

        public override void BuildGeometry()
        {
            Geometry = GlobalInstance.BrepTools.MakePoint(Position);
        }
    }
}
