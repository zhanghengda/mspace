using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Mmc.Windows.Utils
{
	public class SoftKeyBoard
	{
		//[CompilerGenerated]
		//private static class <>o__0
		//{
		//	public static CallSite<Func<CallSite, Type, object, object>> <>p__0;

		//	public static CallSite<Func<CallSite, object, object>> <>p__1;

		//	public static CallSite<Func<CallSite, object, bool>> <>p__2;

		//	public static CallSite<Action<CallSite, Type, object>> <>p__3;
		//}

		public static void Show()
		{
			try
			{
				Version version = Environment.OSVersion.Version;
				Version value = new Version("6.2");
				bool flag = version.CompareTo(value) < 0;
				//if (!flag)
				//{
				//	object arg = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
				//	if (SoftKeyBoard.<>o__0.<>p__2 == null)
				//	{
				//		SoftKeyBoard.<>o__0.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(SoftKeyBoard), new CSharpArgumentInfo[]
				//		{
				//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				//		}));
				//	}
				//	Func<CallSite, object, bool> arg_121_0 = SoftKeyBoard.<>o__0.<>p__2.Target;
				//	CallSite arg_121_1 = SoftKeyBoard.<>o__0.<>p__2;
				//	if (SoftKeyBoard.<>o__0.<>p__1 == null)
				//	{
				//		SoftKeyBoard.<>o__0.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(SoftKeyBoard), new CSharpArgumentInfo[]
				//		{
				//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				//		}));
				//	}
				//	Func<CallSite, object, object> arg_11C_0 = SoftKeyBoard.<>o__0.<>p__1.Target;
				//	CallSite arg_11C_1 = SoftKeyBoard.<>o__0.<>p__1;
				//	if (SoftKeyBoard.<>o__0.<>p__0 == null)
				//	{
				//		SoftKeyBoard.<>o__0.<>p__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Exists", null, typeof(SoftKeyBoard), new CSharpArgumentInfo[]
				//		{
				//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
				//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				//		}));
				//	}
				//	bool flag2 = arg_121_0(arg_121_1, arg_11C_0(arg_11C_1, SoftKeyBoard.<>o__0.<>p__0.Target(SoftKeyBoard.<>o__0.<>p__0, typeof(File), arg)));
				//	if (!flag2)
				//	{
				//		if (SoftKeyBoard.<>o__0.<>p__3 == null)
				//		{
				//			SoftKeyBoard.<>o__0.<>p__3 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Start", null, typeof(SoftKeyBoard), new CSharpArgumentInfo[]
				//			{
				//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
				//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				//			}));
				//		}
				//		SoftKeyBoard.<>o__0.<>p__3.Target(SoftKeyBoard.<>o__0.<>p__3, typeof(Process), arg);
				//	}
				//}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void Hide()
		{
			try
			{
				IntPtr intPtr = new IntPtr(0);
				intPtr = WindowsApi.FindWindow("IPTip_Main_Window", null);
				bool flag = intPtr == IntPtr.Zero;
				if (!flag)
				{
					WindowsApi.PostMessage(intPtr, 274, 61536u, 0u);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
