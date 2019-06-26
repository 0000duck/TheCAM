using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data.Geometry
{
    /// <summary>
    /// 线段
    /// </summary>
    [Serializable]
    public class LineShape : Shape
    {
        public Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }

        public LineShape()
        {
            StartPoint = new Vector3(0, 0, 0);
            EndPoint = new Vector3(0, 1, 0);
        }

        public override void BuildGeometry()
        {
            Geometry = GlobalInstance.BrepTools.MakeLine(StartPoint, EndPoint);
        }
    }
}
