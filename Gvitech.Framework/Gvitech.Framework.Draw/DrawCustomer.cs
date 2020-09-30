using System;

//管理所有使用RCEvent的对象

namespace Mmc.Framework.Draw
{
    public delegate void OnRCRestored();

    /// <summary>
    ///     接口：Rc事件的消费者
    /// </summary>
    public interface IDrawCustomer
    {
        /// <summary>
        ///     恢复
        /// </summary>
        void Restore();

        void Register(OnRCRestored restoredEventHandle);
        bool BeChooseFacility();
    }

    public abstract class DrawCustomer : IDrawCustomer
    {
        public string _customerName;
        public DrawCustomerType _type = DrawCustomerType.UserControl;


        public DrawCustomer(string name, DrawCustomerType type)
        {
            _customerName = name;
            _type = type;
        }

        public virtual void Restore()
        {
            if (OnRestore != null)
            {
                OnRestore();
                OnRestore = null;
            }
        }

        //废弃，caoronglong 2013-6-4
        //public void UnRegister() 
        //{
        //    if (OnRestore != null)
        //    {
        //        this.OnRestore = null;              
        //    }
        //}
        public void Register(OnRCRestored restoredEventHandle)
        {
            OnRestore = restoredEventHandle;
        }

        /// <summary>
        ///     判断是否为选择设施功能
        /// </summary>
        /// <returns></returns>
        public virtual bool BeChooseFacility()
        {
            var ec = new DrawCustomerUC("选择设施", DrawCustomerType.EventAllowSuspend);
            if (Equals(ec))
                return true;
            return false;
        }

        public event OnRCRestored OnRestore;

        public override bool Equals(object obj)
        {
            try
            {
                var uc = obj as DrawCustomer;
                if (uc != null)
                {
                    return _customerName.Equals(uc._customerName) && _type == uc._type;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    /// <summary>
    ///     用户自定义控件的
    /// </summary>
    public class DrawCustomerUC : DrawCustomer
    {
        public DrawCustomerUC(string name, DrawCustomerType type)
            : base(name, type)
        {
        }
    }

    /// <summary>
    ///     事件恢复者，允许事件暂停和恢复的，来实现系统中一些复杂的功能交互，
    ///     如实现通视分析过程中，平移设施后直接再次分析的功能用例
    /// </summary>
    public class DrawCustomerAllowSuspend : DrawCustomer
    {
        public DrawCustomerAllowSuspend(string name, DrawCustomerType type)
            : base(name, type)
        {
            IsSuspended = false;
        }

        /// <summary>
        ///     事件是否被暂停
        /// </summary>
        public bool IsSuspended { get; private set; }

        public event OnRCRestored OnSuspend;
        public event OnRCRestored OnResume;

        public void RegisterSuspend(OnRCRestored suspendEventHandle)
        {
            OnSuspend = suspendEventHandle;
        }

        public void RegisterResume(OnRCRestored resumeEventHandle)
        {
            OnResume = resumeEventHandle;
        }

        /// <summary>
        ///     暂停事件
        /// </summary>
        public virtual void Suspend()
        {
            if (OnSuspend != null && !IsSuspended)
            {
                OnSuspend();
                IsSuspended = true;
            }
        }

        /// <summary>
        ///     唤醒事件
        /// </summary>
        public virtual void Resume()
        {
            if (OnResume != null && IsSuspended)
            {
                OnResume();
                IsSuspended = false;
            }
        }
    }


    public enum DrawCustomerType
    {
        UserControl,
        MenuCommand,
        RightMenu,

        /// <summary>
        ///     事件允许挂起，当有新的工具注册事件时，它的事件会被挂起
        ///     当事件退出时，它的事件会被恢复，这个工具系统中只允许有一个
        /// </summary>
        EventAllowSuspend,
        Other
    }
}