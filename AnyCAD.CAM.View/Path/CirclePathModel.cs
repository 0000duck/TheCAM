using AnyCAD.CAM.Data.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.View
{
    public class CirclePathModel : WorkPathModel
    {
        public CirclePath PathShape { get; set; }

        public float Radius
        {
            get { return PathShape.Radius * 2; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("半径太小！");
                }
                else
                {
                    PathShape.Radius = value * 0.5f;
                    OnPropertyChanged("Radius");
                    OnPathChanged();
                }
            }
        }

        public CirclePathModel(CirclePath ps)
            : base(ps)
        {
            PathShape = ps;
        }
    }
}
