using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class WorkShape
    {

        public string Name { get; set; }


        public WorkShape()
        {

        }

        public virtual void UpdateGeometry()
        {

        }

        public virtual bool Load()
        {
            return false;
        }

        public virtual GroupSceneNode GetVisualNode()
        {
            return null;
        }
    }
}
