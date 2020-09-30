using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Utils;
using System;

namespace Gvitech.CityMaker.Models
{
    public class MarkPoint
    {
        private static readonly IPointSymbol PointSymbol = new SimplePointSymbol
        {
            FillColor = ColorConvert.UintToColor(4294967040u),
            Size = 15
        };

        private static readonly ITextSymbol TextSymbol = new TextSymbol
        {
            DrawLine = false,
            PivotAlignment = gviPivotAlignment.gviPivotAlignTopLeft,
            TextAttribute = new TextAttribute
            {
                TextColor = ColorConvert.UintToColor(4278222848u),
                Font = "Î¢ÈíÑÅºÚ",
                OutlineColor = ColorConvert.UintToColor(65280u),
                TextSize = 13
            }
        };

        public IRenderPoint Anchor
        {
            get;
            set;
        }

        public ILabel MarkText
        {
            get;
            set;
        }

        private MarkPoint(IRenderPoint point, ILabel text)
        {
            this.Anchor = point;
            this.MarkText = text;
        }

        public static MarkPoint CreateMarkPoint(IObjectManager omg, IPoint position, string text, IPointSymbol pointSymbol = null, ITextSymbol textSymbol = null)
        {
            bool flag = omg == null || position == null;
            MarkPoint result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IRenderPoint point = omg.CreateRenderPoint(position, pointSymbol ?? MarkPoint.PointSymbol);
                ILabel label = null;
                bool flag2 = !string.IsNullOrEmpty(text);
                if (flag2)
                {
                    label = omg.CreateLabel(position);
                    label.Text = text;
                    label.TextSymbol = (textSymbol ?? MarkPoint.TextSymbol);
                }
                result = new MarkPoint(point, label);
            }
            return result;
        }

        public MarkPoint UpData(IPoint position, string text = null)
        {
            bool flag = this.Anchor != null;
            if (flag)
            {
                this.Anchor.SetFdeGeometry(position);
            }
            bool flag2 = this.MarkText != null;
            if (flag2)
            {
                this.MarkText.SetPosition(position);
                this.MarkText.Text = text;
            }
            return this;
        }

        public MarkPoint ChangePointSymbol(IPointSymbol pointSymbol)
        {
            bool flag = this.Anchor != null;
            if (flag)
            {
                this.Anchor.Symbol = pointSymbol;
            }
            return this;
        }

        public MarkPoint ChangeTextSymbol(ITextSymbol textSymbol)
        {
            bool flag = this.MarkText != null;
            if (flag)
            {
                this.MarkText.TextSymbol = textSymbol;
            }
            return this;
        }

        public MarkPoint SetVisible(bool isVisible = true, gviViewportMask gvm = gviViewportMask.gviViewAllNormalView)
        {
            gvm = (isVisible ? gvm : gviViewportMask.gviViewNone);
            bool flag = this.Anchor != null;
            if (flag)
            {
                this.Anchor.VisibleMask = gvm;
            }
            bool flag2 = this.MarkText != null;
            if (flag2)
            {
                this.MarkText.VisibleMask = gvm;
            }
            return this;
        }

        public void Release(IObjectManager omg)
        {
            bool flag = omg == null;
            if (!flag)
            {
                omg.ReleaseRenderObject(new IRenderable[]
                {
                    this.Anchor,
                    this.MarkText
                });
                bool flag2 = this.Anchor != null;
                if (flag2)
                {
                    this.Anchor = null;
                }
                bool flag3 = this.MarkText != null;
                if (flag3)
                {
                    this.MarkText = null;
                }
            }
        }
    }
}
