using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class KnifeToolInstance
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        // Z
        public float ZStart { get; set; }
        public float ZEnd { get; set; }
        public float ZSafeHeight { get; set; }
        public int ZNumber { get; set; }
        public KnifeTool Parameters { get; set; }


        public KnifeToolInstance()
        {
            Position = new Vector3(0,0,0);
            Parameters = new KnifeTool();
            Direction = -Vector3.UNIT_Z;
            ZStart = 3;
            ZEnd = -5;
            ZSafeHeight = 150;
            ZNumber = 1;
        }
    }
}
