using AnyCAD.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.CAM.Data.Geometry;

namespace AnyCAD.CAM.Data.Path
{
    [Serializable]
    public class WorkPath
    {
        public float Rotation { get; set; }
        public List<Shape>  Loops { get; set; }     
  
        public String Icon { get; set; }
      
        public WorkPath()
        {
            Rotation = 0;
            Loops = new List<Shape>();
        }

        protected virtual bool BuildShapes() 
        {
            Loops.Clear();

            return false;
        }

        protected void AddShape(Shape shape)
        {
            shape.Id = Loops.Count;
            Loops.Add(shape);
        }

        
        public TopoShape Section { get; set; }

        public void BuildGeometry()
        {
            BuildShapes();
            
            if(Loops.Count == 1)
            {
                Loops[0].BuildGeometry();
                Section = Loops[0].Geometry;
            }
            else
            {
                TopoShapeGroup list = new TopoShapeGroup();
                foreach(Shape sp in Loops)
                {
                    sp.BuildGeometry();
                    list.Add(sp.Geometry);
                }
                Section = GlobalInstance.BrepTools.MakeWire(list);
            }

        }
    }



}
