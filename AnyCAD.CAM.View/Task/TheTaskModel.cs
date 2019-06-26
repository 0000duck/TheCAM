using AnyCAD.CAM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.View
{

    public class TheTaskModel : INotifyPropertyChanged
    {
        public MachineTask CurrentTask { get; set; }
        public WorkPathModel ThePath { get; set; }
        public KnifeToolModel TheKnifeTool { get; set; }

        public TheTaskModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e));
        }

        public event TaskChangeEventHandler TaskChanged;
        public void OnTaskChanged()
        {
            if (TaskChanged != null)
                TaskChanged(this);
        }


        //完全加工/路径加工
        public bool ByComplete
        {
            get { return CurrentTask.CompleteOrByPath; }
            set {
                CurrentTask.CompleteOrByPath = value;
                OnPropertyChanged("ByComplete");
                OnPropertyChanged("ByPath");
            }
        }
        public bool ByPath
        {
            get { return !CurrentTask.CompleteOrByPath; }
            set
            {
                CurrentTask.CompleteOrByPath = !value;
                OnPropertyChanged("ByComplete");
                OnPropertyChanged("ByPath");
            }
        }

        public string LogMessage
        {
            get { return CurrentTask.LogMessage;  }
            set {
                CurrentTask.LogMessage = value;
                OnPropertyChanged("LogMessage");
            }
        }

        public double LayoutX
        {
            get { return CurrentTask.Tool.Position.X; }
            set
            {
                CurrentTask.Tool.Position.X = value;
                OnPropertyChanged("LayoutX");
                OnTaskChanged();
            }
        }
        public double LayoutW
        {
            get { return -CurrentTask.Tool.Position.Y; }
            set
            {
                CurrentTask.Tool.Position.Y = -value;
                OnPropertyChanged("LayoutW");
                OnTaskChanged();
            }
        }
        public double LayoutH
        {
            get { return CurrentTask.Tool.Position.Z; }
            set
            {
                CurrentTask.Tool.Position.Z = value;
                OnPropertyChanged("LayoutH");
                OnTaskChanged();
            }
        }
        public float Step
        {
            get { return CurrentTask.Layout.Step; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("步长太小！");
                }
                else
                {
                    CurrentTask.Layout.Step = value;
                    OnPropertyChanged("Step");
                }

            }
        }

        public float ZStart
        {
            get { return CurrentTask.Tool.ZStart; }
            set
            {
                CurrentTask.Tool.ZStart = value;
                OnPropertyChanged("ZStart");
                OnTaskChanged();
            }
        }
        public float ZEnd
        {
            get { return CurrentTask.Tool.ZEnd; }
            set
            {
                CurrentTask.Tool.ZEnd = value;
                OnPropertyChanged("ZEnd");
                OnTaskChanged();
            }
        }
        public float ZSafeHeight
        {
            get { return CurrentTask.Tool.ZSafeHeight; }
            set
            {
                CurrentTask.Tool.ZSafeHeight = value;
                OnPropertyChanged("ZSafeHeight");
                OnTaskChanged();
            }
        }
        public int ZNumber
        {
            get { return CurrentTask.Tool.ZNumber; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("不能小于1！");
                }
                else
                {
                    CurrentTask.Tool.ZNumber = value;
                    OnPropertyChanged("ZNumber");
                }

            }
        }

    }
}
