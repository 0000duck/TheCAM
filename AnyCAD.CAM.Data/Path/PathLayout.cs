using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class PathLayout
    {
        public bool FromStartPoint { get; set; }
        //步长
        public float Step { get; set; }

        public PathLayout()
        {
            FromStartPoint = true;
            Step = 0.07f;
        }
    }
}
