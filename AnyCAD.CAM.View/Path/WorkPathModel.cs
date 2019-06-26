using AnyCAD.CAM.Data;
using AnyCAD.CAM.Data.Path;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.View
{
    public class WorkPathModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(e));
        }

        public event PathChangeEventHandler PathChanged;
        public void OnPathChanged()
        {
            if(PathChanged != null)
            {
                PathChanged(this);
            }
        }

        public WorkPath Shape { set; get; }

        public WorkPathModel(WorkPath shape)
        {
            Shape = shape;
        }

    }



}
