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
    public class PathUIDef
    {
        public virtual  UserControl CreateUI()
        {
            return null;
        }
        public String Title { get; set; }
        public virtual WorkPathModel CreateShapeModel(WorkPath ps)
        {
            return null;
        }

    }

    public class PathShapeUIFactory
    {
        public Dictionary<Type, PathUIDef> ShapeUIDefs { get; set; }

        public PathShapeUIFactory()
        {
            ShapeUIDefs = new Dictionary<Type, PathUIDef>();

            ShapeUIDefs[typeof(CirclePath)] = new CircleShapeUIDef();
        }

        public PathUIDef FindUI(Type type)
        {
            return ShapeUIDefs[type];
        }
    }
}
