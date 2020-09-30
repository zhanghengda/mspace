using System;
using System.Runtime.InteropServices;

namespace Mmc.Platform.Core
{
	[ComVisible(true)]
	public class FrameCallback
	{
		public FrameHandler onFrame;

		public KeyHandler onKey;

		public MouseButtonHandler onMouseButton;

		public bool OnMouseButton(int x, int y, int buttonAction, int flags, int scroll)
		{
			return this.onMouseButton != null && this.onMouseButton(x, y, buttonAction, flags, scroll);
		}

		public bool OnKey(uint keySymbol, byte keyMask)
		{
			return this.onKey != null && this.onKey(keySymbol, keyMask);
		}

		public void OnFrame(int frameIndex, double refTime)
		{
			if (this.onFrame != null)
			{
				this.onFrame(frameIndex, refTime);
			}
		}
	}
}
