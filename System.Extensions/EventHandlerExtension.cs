using System;

public static class EventHandlerExtension
{
	public static void RaiseEvent(this EventHandler @this, object sender)
	{
		bool flag = @this != null;
		if (flag)
		{
			@this(sender, null);
		}
	}

	public static void RaiseEvent(this EventHandler @this, object sender, EventArgs e)
	{
		bool flag = @this != null;
		if (flag)
		{
			@this(sender, e);
		}
	}

	public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender) where TEventArgs : EventArgs
	{
		bool flag = @this != null;
		if (flag)
		{
			@this(sender, Activator.CreateInstance<TEventArgs>());
		}
	}

	public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e) where TEventArgs : EventArgs
	{
		bool flag = @this != null;
		if (flag)
		{
			@this(sender, e);
		}
	}
}
