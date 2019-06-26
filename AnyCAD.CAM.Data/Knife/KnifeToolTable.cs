using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class KnifeToolTable
    {
        public Dictionary<String, KnifeTool> KnifeTools { get; set; }

        public KnifeToolTable()
        {
            KnifeTools = new Dictionary<String, KnifeTool>();

            KnifeTool tool = new KnifeTool();
            tool.Name = "默认";
            KnifeTools[tool.Name] = tool;
        }
    }
}
