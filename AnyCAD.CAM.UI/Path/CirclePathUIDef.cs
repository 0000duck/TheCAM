using AnyCAD.CAM.Data;
using AnyCAD.CAM.Data.Path;
using AnyCAD.CAM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheCAM
{
    class CircleShapeUIDef : PathUIDef
    {
        public CircleShapeUIDef()
        {
            Title = "圆孔";
        }
        public override UserControl CreateUI()
        {
            return new CirclePathUI();
        }

        public override WorkPathModel CreateShapeModel(WorkPath ps)
        {
            return new CirclePathModel(ps as CirclePath);
        }
    }
}
