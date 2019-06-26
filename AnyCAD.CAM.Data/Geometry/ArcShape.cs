using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data.Geometry
{
    /// <summary>
    /// 圆弧
    /// </summary>
    [Serializable]
    public class ArcShape : Shape
    {
        public float StartAngle { get; set; }
        public float EndAngle { get; set; }
        public Vector3 CenterPoint { get; set; }
        public float Radius { get; set; }

        public ArcShape()
        {
            StartAngle = 0;
            EndAngle = 0;
            CenterPoint = new Vector3();
            Radius = 1;
        }

        public override void BuildGeometry()
        {
            if (StartAngle == EndAngle)
            {
                Geometry = GlobalInstance.BrepTools.MakeCircle(CenterPoint, Radius, Vector3.UNIT_Z);
            }
            else
            {
                Geometry = GlobalInstance.BrepTools.MakeArc(CenterPoint, Radius, StartAngle, EndAngle, Vector3.UNIT_Z);
            }
        }
    }
}
