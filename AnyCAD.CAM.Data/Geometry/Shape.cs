using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data.Geometry
{
    /// <summary>
    /// 基本形状的基类
    /// </summary>
    [Serializable] 
    public class Shape
    {
        public int Id { get; set; }

        public Shape()
        {
            Id = 0;
        }

        [NonSerialized]
        public TopoShape Geometry;
        public virtual void BuildGeometry()
        {

        }
    }
}
