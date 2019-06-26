using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Presentation;
using AnyCAD.Platform;
using AnyCAD.CAM.Data;

namespace AnyCAD.CAM.View
{
    public enum EnumViewDirection
    {
        VD_WH,
        VD_XH,
        VD_3D,
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ModelView3d : UserControl
    {
        private RenderWindow3d m_RenderView = null;
        private ElementId axisWidgetId = new ElementId(100);
        private ElementId axisWidgetId2 = new ElementId(101);
        
        public ModelView3d()
        {
            InitializeComponent();

            //GlobalInstance.RegisterSDK("TEST", "TEST", "2427baa951f0ecc85a072a27ef1df119");
            m_RenderView = new RenderWindow3d();
            m_RenderView.Size = this.ClientSize;
            m_RenderView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(m_RenderView);
        }

        public void ZoomToFit()
        {
            m_RenderView.FitAll();
            m_RenderView.RequestDraw();
        }

        public void Zoom(int factor)
        {
            m_RenderView.Renderer.GetCameraOperator().Zoom(factor);
            m_RenderView.RequestDraw();
        }

        public void SetViewDirection(EnumViewDirection vd)
        {
            if( vd == EnumViewDirection.VD_WH)
            {
                m_RenderView.Renderer.SetStandardView(EnumStandardView.SV_Left);
            }
            else if(vd == EnumViewDirection.VD_XH)
            {
                m_RenderView.Renderer.SetStandardView(EnumStandardView.SV_Back);
            }
            else
            {
                m_RenderView.Renderer.SetStandardView(EnumStandardView.SV_3D);
            }
            m_RenderView.FitAll();
            m_RenderView.RequestDraw();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (m_RenderView != null)
            {
                m_RenderView.RequestDraw();
            }
        }

        private void ModelView3d_Load(object sender, EventArgs e)
        {
            {
                AxesWidget xwh = new AxesWidget();
                xwh.EnableLeftHandCS();
                xwh.SetArrowText((int)EnumAxesDirection.Axes_Y, "w");
                xwh.SetArrowText((int)EnumAxesDirection.Axes_Z, "h");
                xwh.SetArrowText((int)EnumAxesDirection.Axes_X, "x");
                ScreenWidget coordWidget = new ScreenWidget();
                coordWidget.SetNode(xwh);
                coordWidget.SetId(axisWidgetId);
                coordWidget.SetWidgetPosition((int)EnumWidgetPosition.WP_BottomLeft);
                m_RenderView.Renderer.AddWidgetNode(coordWidget);
            }
            {
                AxesWidget yz = new AxesWidget();
                yz.ShowArrow((int)EnumAxesDirection.Axes_X, false);
                ScreenWidget coordWidget = new ScreenWidget();
                coordWidget.SetNode(yz);
                coordWidget.SetId(axisWidgetId2);
                coordWidget.SetWidgetPosition((int)EnumWidgetPosition.WP_BottomRight);
                m_RenderView.Renderer.AddWidgetNode(coordWidget);
            }
            m_RenderView.ShowWorkingGrid(false);
            m_RenderView.ShowCoordinateAxis(false);
        }



           public void UpdateWorkingGrid()
        {
            m_RenderView.SceneManager.ComputeBBox();

            WorkingPlane wp = m_RenderView.Renderer.GetWorkingPlane();
        
            GridNode gridNode = new GridNode();
            Vector3 modelSize = m_RenderView.SceneManager.GetBBox().Size();
            Vector2 cellSize = gridNode.GetCellSize();
            int nCountX = (int)(modelSize.X / cellSize.X + 0.5f) + 1;
            int nCountY = (int)(modelSize.Y / cellSize.Y + 0.5f) + 1;
            if (nCountX < 2)
                nCountX = 2;
            if (nCountY < 2)
                nCountY = 2;

            gridNode.SetCellCount(nCountX, nCountY);

            LineStyle lineStyle = new LineStyle();
            lineStyle.SetColor(new ColorValue(1.0f, 1.0f, 1.0f));
            lineStyle.SetPatternStyle((int)EnumLinePattern.LP_DotLine);
            {
                //Z
                LineNode lineNode = new LineNode();
                lineNode.Set(new Vector3(0, 0, -1000), new Vector3(0, 0, 1000));
                lineNode.SetLineStyle(lineStyle);
                gridNode.AddNode(lineNode);
            }
            {
                //X
                LineNode lineNode = new LineNode();
                lineNode.Set(new Vector3(-1000, 0, 0), new Vector3(1000, 0, 0));
                lineNode.SetLineStyle(lineStyle);
                gridNode.AddNode(lineNode);
            }
            {
                //Y
                LineNode lineNode = new LineNode();
                lineNode.Set(new Vector3(0, -1000, 0), new Vector3(0, 1000, 0));
                lineNode.SetLineStyle(lineStyle);
                gridNode.AddNode(lineNode);
            }

            lineStyle = new LineStyle();
            lineStyle.SetColor(new ColorValue(0.9f, 0.9f, 0.9f));
            gridNode.SetLineStyle(lineStyle);
            for (int ii = -1; ii <= nCountX; ++ii)
            {
                if (ii == 0)
                    continue;

                LineNode lineNode = new LineNode();
                lineNode.Set(new Vector3(ii * cellSize.X, cellSize.Y, 0), new Vector3(ii * cellSize.X, -nCountY * cellSize.Y, 0));

                gridNode.AddNode(lineNode);
            }
            for (int ii = -1; ii <= nCountY; ++ii)
            {
                if (ii == 0)
                    continue;

                LineNode lineNode = new LineNode();
                lineNode.Set(new Vector3(-cellSize.X, -ii * cellSize.Y, 0), new Vector3(nCountX * cellSize.X, -ii * cellSize.Y, 0));
                gridNode.AddNode(lineNode);
            }
            gridNode.Update();
            wp.SetGridNode(gridNode);
            m_RenderView.ShowWorkingGrid(true);
        }

        public void OnModelChanged(object sender, ModelChangeEvent e)
        {
            if (e.changeType == EnumDataChange.DC_ADD)
            {
                WorkShapeModel mv = sender as WorkShapeModel;

                if(e.modelType == EnumModelType.MT_WorkShape)
                {
                    m_RenderView.ShowSceneNode(mv.ActiveWorkShape.GetVisualNode());
                    UpdateWorkingGrid();
                    m_RenderView.FitAll();
                }
                else if(e.modelType == EnumModelType.MT_KnifeTool)
                {
                    m_RenderView.ShowSceneNode(mv.KnifeShape.VisualNode);
                }
                else if (e.modelType == EnumModelType.MT_Task)
                {
                    m_RenderView.ShowSceneNode(mv.ActiveTask.VisualNode);
                }

            }
            else if(e.changeType == EnumDataChange.DC_MODIFIED)
            {
                if(e.modelType == EnumModelType.MT_WorkShape)
                    UpdateWorkingGrid();
                //if(e.modelType == EnumModelType.MT_Task)

            }
            else if (e.changeType == EnumDataChange.DC_DELETE)
            {
                WorkShapeModel mv = sender as WorkShapeModel;

                if(e.modelType == EnumModelType.MT_Task)
                {
                    m_RenderView.SceneManager.RemoveNode(mv.ActiveTask.VisualNode);
                }
            }
            m_RenderView.RequestDraw();
        }

        public void RequestDraw()
        {
            m_RenderView.RequestDraw();
        }
    }
}
