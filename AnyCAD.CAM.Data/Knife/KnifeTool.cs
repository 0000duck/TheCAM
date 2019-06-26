using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.Data
{
    [Serializable]
    public class KnifeTool : ICloneable
    {
        public String Name { get; set; }

        //刀具类型  0铣孔  1钻孔 2扩孔 3锯片 4攻丝
        public int EnumKnifeType { get; set; }
        //旋转类型 0 正传 1 反转 
        public int SpindleWay { get; set; }
        //是否T型刀
        public int IsTypeT { get; set; }
        //冷却频率
        public int CoolFrequence { get; set; }
        //转速
        public int RotateSpeed { get; set; }
        //进给速度/移动速度
        public int MoveSpeed { get; set; }
        //下刀速度
        public int DownSpeed { get; set; }
        //退刀速度
        public int UpSpeed { get; set; }

        //刀具长度
        public int DrillLength { get; set; }
        //刀具直径
        public int DrillDiameter { get; set; }
        //刀杆直径
        public int HolderDiameter { get; set; }
        // 夹头直径1  
        public int HeadOneDiameter { get; set; }
        // 夹头长度1
        public int HeadOneLength { get; set; }
        //总长度
        public int TotalLength { get; set; }
        //附加长度
        public int AuxLength { get; set; }
        // 夹头直径2  
        public int HeadTwoDiameter { get; set; }
        // 夹头长度2
        public int HeadTwoLength { get; set; }
        //刀具补长
        public int DrillAuxLength { get; set; }

        public KnifeTool()
        {
            Name = "1";
            EnumKnifeType = 0;
            CoolFrequence = 5;
            SpindleWay = 0;
            IsTypeT = 0;

            RotateSpeed = 12000;
            MoveSpeed = 1000;
            DownSpeed = 300;
            UpSpeed = 20000;

            DrillLength = 100;
            DrillDiameter = 8;
            HolderDiameter = 10;
            HeadOneDiameter = 10;
            HeadOneLength = 10;

            TotalLength = 300;
            AuxLength = 20;
            HeadTwoDiameter = 10;
            HeadTwoLength = 10;
            DrillAuxLength = 0;

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
