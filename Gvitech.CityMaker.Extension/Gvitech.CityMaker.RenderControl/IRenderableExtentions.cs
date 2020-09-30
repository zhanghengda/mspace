using System;

namespace Gvitech.CityMaker.RenderControl
{
    public static class IRenderableExtentions
    {
        public static T SetVisibleParam<T>(this T renderable, double maxVisibleDistance = 100000.0, float minVisiblePixels = 15f, gviViewportMask vmask = gviViewportMask.gviViewAllNormalView, gviDepthTestMode dtm = gviDepthTestMode.gviDepthTestEnable, double viewingDistance = 0.0, double minVisibleDistance = -100000.0) where T : IRenderable
        {
            bool flag = renderable == null;
            T result;
            if (flag)
            {
                result = default(T);
            }
            else
            {
                renderable.MaxVisibleDistance = maxVisibleDistance;
                renderable.MinVisiblePixels = minVisiblePixels;
                renderable.MinVisibleDistance = minVisibleDistance;
                renderable.ViewingDistance = ((viewingDistance <= 0.0) ? ((double)renderable.Envelope.DiagonalDistance() * 1.5) : viewingDistance);
                renderable.DepthTestMode = dtm;
                renderable.VisibleMask = vmask;
                result = renderable;
            }
            return result;
        }

        /// <summary>
        /// 设置多屏图层可见状态
        /// </summary>
        /// <param name="renderable">图层</param>
        /// <param name="viewportMode">多屏状态</param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">是否可见</param>
        public static void SetVisibleMask(this IRenderable renderable, gviViewportMode viewportMode, int viewPortIndex, bool isVisilbe)
        {
            if (viewportMode == gviViewportMode.gviViewportSinglePerspective)//单屏状态
            {
                renderable.VisibleMask = isVisilbe ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
            }
            else if (viewportMode == gviViewportMode.gviViewportL1R1)// 左右双屏状态
            {
                SetL1R1VisibleMask(renderable, viewPortIndex, isVisilbe);
            }
            else if (viewportMode == gviViewportMode.gviViewportL1M1R1)//左中右三屏
            {
                SetL1M1R1VisibleMask(renderable, viewPortIndex, isVisilbe);
            }
            else if (viewportMode == gviViewportMode.gviViewportQuad)//四屏
            {
                SetQuadVisibleMask(renderable, viewPortIndex, isVisilbe);
            }
        }

        /// <summary>
        /// 设置双屏状态
        /// </summary>
        /// <param name="renderable"></param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">是否可见</param>
        private static void SetL1R1VisibleMask(IRenderable renderable, int viewPortIndex, bool isVisilbe)
        {
            if (viewPortIndex == 0)//第一屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView0;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView1;
                    else
                    {
                        SetIndexUnVisible(renderable, gviViewportMask.gviView0);
                    }
                }
            }
            else if (viewPortIndex == 1)
            {
                if (isVisilbe)
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView1;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView1;
                    else
                    {
                        SetIndexUnVisible(renderable, gviViewportMask.gviView1);
                    }
                }
            }
        }

        private static void SetIndexUnVisible(IRenderable renderable, gviViewportMask viewIndex)
        {
            var value = (int)renderable.VisibleMask;
            var index = (int)viewIndex;
            if ((value & index) == index)//第一屏可见
            {
                renderable.VisibleMask = renderable.VisibleMask ^ viewIndex;
            }
        }




        /// <summary>
        /// 设置三屏状态
        /// </summary>
        /// <param name="renderable"></param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">是否可见</param>
        private static void SetL1M1R1VisibleMask(IRenderable renderable, int viewPortIndex, bool isVisilbe)
        {
            if (viewPortIndex == 0)//第一屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView0;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView1 | gviViewportMask.gviView2;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView0);
                }
            }
            else if (viewPortIndex == 1)//第二屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView1;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView2;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView1);

                }

            }
            else if (viewPortIndex == 2)//第三屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView2;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView2);

                }
            }

        }

        /// <summary>
        /// 设置四屏状态
        /// </summary>
        /// <param name="renderable"></param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">是否可见</param>
        private static void SetQuadVisibleMask(IRenderable renderable, int viewPortIndex, bool isVisilbe)
        {
            if (viewPortIndex == 0)//第一屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView0;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView1 | gviViewportMask.gviView2 | gviViewportMask.gviView3;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView0);

                }
            }
            else if (viewPortIndex == 1)//第二屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView1;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView2 | gviViewportMask.gviView3;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView1);

                }

            }
            else if (viewPortIndex == 2)//第三屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView2;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView3;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView2);

                }
            }
            else if (viewPortIndex == 3)//第四屏
            {
                if (isVisilbe)//可见状态
                    renderable.VisibleMask = renderable.VisibleMask | gviViewportMask.gviView3;
                else
                {
                    if ((int)(renderable.VisibleMask) == 15 || (int)(renderable.VisibleMask) == 255)
                        renderable.VisibleMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView2;
                    else
                        SetIndexUnVisible(renderable, gviViewportMask.gviView3);

                }
            }
        }


    }
}
