using AnyCAD.CAM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AnyCAD.CAM.View
{
    public class SectionBarModel : WorkShapeModel
    {
        public SectionBarModel()
        {
            ModelFileFilter = "DXF (*.dxf)|*.dxf";
        }
        public new event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);

            if (ActiveWorkShape == null)
                return;

            var sectionBar = ActiveWorkShape as SectionBar;

            sectionBar.Length = this.SectionBarLength;
            sectionBar.UpdateGeometry();
            OnModelChanged(EnumModelType.MT_WorkShape, EnumDataChange.DC_MODIFIED);

        }

        public override bool LoadWorkShape(string fileName, string barName)
        {
            SectionBar sb = new SectionBar();
            sb.FileName = fileName;
            sb.Name = barName;
            sb.Length = this.SectionBarLength;
            sb.StartWH.X = this.SxW;
            sb.StartWH.Y = this.SxH;
            sb.EndWH.X = this.DxW;
            sb.EndWH.Y = this.DxH;

            if (!sb.Load())
            {
                return false;
            }

            ActiveWorkspace.AddWorkShape(sb);
            ActiveWorkShape = sb;

            SxW = (long)sb.StartWH.X;
            SxH = (long)sb.StartWH.Y;

            DxW = (long)sb.EndWH.X;
            DxH = (long)sb.EndWH.Y;
            SectionBarLength = (long)sb.Length;

            OnModelChanged(EnumModelType.MT_WorkShape, EnumDataChange.DC_ADD);


            if (KnifeShape == null)
            {
                KnifeShape = new KnifeToolShape();
                KnifeShape.UpdateKnifeTool(ActiveKnifeTool);
                OnModelChanged(EnumModelType.MT_KnifeTool, EnumDataChange.DC_ADD);
            }

            return true;
        }

        #region SectionBarBindings
        private long _sectionBarLength = 1000;
        public long SectionBarLength
        {
            get
            {
                return _sectionBarLength;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("型材的长度太小.");
                }
                else
                {
                    _sectionBarLength = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SectionBarLength"));
                }
            }
        }

        private long dxW;
        public long DxW
        {
            get
            {
                return dxW;
            }
            set
            {
                dxW = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DxW"));
            }
        }
        private long dxH;
        public long DxH
        {
            get
            {
                return dxH;
            }
            set
            {
                dxH = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DxH"));
            }
        }
        private long sxW;
        public long SxW
        {
            get
            {
                return sxW;
            }
            set
            {
                sxW = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SxW"));
            }
        }
        private long sxH;
        public long SxH
        {
            get
            {
                return sxH;
            }
            set
            {
                sxH = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SxH"));
            }
        }
        #endregion
    }
}
