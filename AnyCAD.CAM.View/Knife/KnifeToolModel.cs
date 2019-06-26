using AnyCAD.CAM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.View
{
    public class KnifeToolModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e));
        }

        public KnifeToolInstance Tool { get; set; }

        public void SetToolParam(KnifeTool param)
        {
            RotationSpeed = param.RotateSpeed;
            MoveSpeed = param.MoveSpeed;
            DownSpeed = param.DownSpeed;
            UpSpeed = param.UpSpeed;
            CoolFrequence = param.CoolFrequence;

            Tool.Parameters = (KnifeTool)param.Clone();
        }

        public KnifeToolModel(KnifeToolInstance tool)
        {
            Tool = tool;
        }

        public int RotationSpeed
        {
            get { return Tool.Parameters.RotateSpeed;  }
            set 
            { 
                if(value < 1)
                {
                    throw new ArgumentException("转速太小.");
                }
                else
                {
                    Tool.Parameters.RotateSpeed = value;
                    OnPropertyChanged("RotationSpeed");
                }
            }
        }
        public int MoveSpeed
        {
            get { return Tool.Parameters.MoveSpeed; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("转速太小.");
                }
                else
                {
                    Tool.Parameters.MoveSpeed = value;
                    OnPropertyChanged("MoveSpeed");
                }
            }
        }
        public int DownSpeed
        {
            get { return Tool.Parameters.DownSpeed; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("转速太小.");
                }
                else
                {
                    Tool.Parameters.DownSpeed = value;
                    OnPropertyChanged("DownSpeed");
                }
            }
        }
        public int UpSpeed
        {
            get { return Tool.Parameters.UpSpeed; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("转速太小.");
                }
                else
                {
                    Tool.Parameters.UpSpeed = value;
                    OnPropertyChanged("UpSpeed");
                }
            }
        }
        public int CoolFrequence
        {
            get { return Tool.Parameters.CoolFrequence; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("冷却频率太小.");
                }
                else
                {
                    Tool.Parameters.CoolFrequence = value;
                    OnPropertyChanged("CoolFrequence");
                }
            }
        }
    }
}
