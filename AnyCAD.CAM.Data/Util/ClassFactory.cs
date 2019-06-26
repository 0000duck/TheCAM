using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using AnyCAD.CAM.Data.Geometry;
using AnyCAD.CAM.Data.Path;

namespace AnyCAD.CAM.Data
{


    public class ClassFactory
    {
        List<Type> Types { get; set; }

        protected ClassFactory()
        {
            Types = new List<Type>();
            Types.Add(typeof(KnifeToolTable));
            Types.Add(typeof(MachineTask));
            Types.Add(typeof(KnifeTool));
            Types.Add(typeof(KnifeToolInstance));

            Types.Add(typeof(Shape));
            Types.Add(typeof(LineShape));
            Types.Add(typeof(ArcShape));

            Types.Add(typeof(WorkPath));
            Types.Add(typeof(KeyPath));
            Types.Add(typeof(CirclePath));
       
        }

        static ClassFactory _Instance = null;
        public static ClassFactory Instance()
        {
            if (_Instance == null)
            {
                _Instance = new ClassFactory();
            }
            return _Instance;
        }

        XmlSerializer CreateWorkspaceXS()
        {
            return new XmlSerializer(typeof(Workspace), Types.ToArray());
        }

        public bool Save(Workspace ws, String fileName)
        {
            XmlSerializer serialize = CreateWorkspaceXS();
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                serialize.Serialize(fs, ws);
            }

            return true;
        }

        public Workspace Load(String fileName)
        {
            XmlSerializer serializer = CreateWorkspaceXS();
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
               return serializer.Deserialize(reader) as Workspace;
            }
        }

        public bool SaveDrillTools(KnifeToolTable drills, String fileName)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(KnifeToolTable), Types.ToArray());
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                serialize.Serialize(fs, drills);
            }

            return true;
        }
    }
}
