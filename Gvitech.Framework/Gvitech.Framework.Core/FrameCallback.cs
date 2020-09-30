using System;
using System.Runtime.InteropServices;

namespace Mmc.Framework.Core
{
	[ComVisible(true)]
	public class FrameCallback
	{
		public FrameHandler onFrame;

		public KeyHandler onKey;

		public MouseButtonHandler onMouseButton;

		public bool OnMouseButton(int x, int y, int buttonAction, int flags, int scroll)
		{
			bool flag = this.onMouseButton != null;
			return flag && this.onMouseButton(x, y, buttonAction, flags, scroll);
		}

		public bool OnKey(uint keySymbol, byte keyMask)
		{
			bool flag = this.onKey != null;
			return flag && this.onKey(keySymbol, keyMask);
		}

		public void OnFrame(int frameIndex, double refTime)
		{
			bool flag = this.onFrame != null;
			if (flag)
			{
				this.onFrame(frameIndex, refTime);
			}
		}
	}
}
