using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Messenger
{
    /// <summary>
    /// 提供松散耦合的消息通知机制，为防止内存泄漏，所有对象都使用了弱引用（WeakReference）
    /// </summary>
    public class Messenger
    {
        #region Constructor
        private static object _lock = new object();
        private static Messenger _messengers;

        public static Messenger Messengers
        {
            get
            {
                if (_messengers == null)
                {
                    lock (_lock)
                    {
                        if (_messengers == null)
                        {
                            _messengers = new Messenger();
                        }
                    }
                }
                return _messengers;
            }
        }
        public Messenger()
        {

        }

        #endregion // Constructor

        #region Register

        /// <summary>
        /// 注册消息监听
        /// </summary>
        public void Register(string message, Action callback)
        {
            this.Register(message, callback, null);
        }

        /// <summary>
        /// 注册消息监听
        /// token,key唯一性只能单个使用 无类型约束
        /// </summary>
        /// <typeparam name="T">注册类型</typeparam>
        /// <param name="message">key唯一性 只能单个使用 无类型约束</param>
        /// <param name="callback">触发函数</param>
        public void Register<T>(string message, Action<T> callback)
        {
            this.Register(message, callback, typeof(T));
        }
        /// <summary>
        /// token,回调函数,类型
        /// </summary>
        /// <param name="message">token</param>
        /// <param name="callback">触发函数</param>
        /// <param name="parameterType">类型</param>
        void Register(string message, Delegate callback, Type parameterType)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException("'message' cannot be null or empty.");

            if (callback == null)
                throw new ArgumentNullException("callback");

            this.VerifyParameterType(message, parameterType);

            _messageToActionsMap.AddAction(message, callback.Target, callback.Method, parameterType);
        }

        [Conditional("DEBUG")]
        void VerifyParameterType(string message, Type parameterType)
        {
            Type previouslyRegisteredParameterType = null;
            if (_messageToActionsMap.TryGetParameterType(message, out previouslyRegisteredParameterType))
            {
                if (previouslyRegisteredParameterType != null && parameterType != null)
                {
                    if (!previouslyRegisteredParameterType.Equals(parameterType))
                        throw new InvalidOperationException(string.Format(
                            "The registered action's parameter type is inconsistent with the previously registered actions for message '{0}'.\nExpected: {1}\nAdding: {2}",
                            message,
                            previouslyRegisteredParameterType.FullName,
                            parameterType.FullName));
                }
                else
                {
                    // One, or both, of previouslyRegisteredParameterType or callbackParameterType are null.
                    if (previouslyRegisteredParameterType != parameterType)   // not both null?
                    {
                        throw new TargetParameterCountException(string.Format(
                            "The registered action has a number of parameters inconsistent with the previously registered actions for message \"{0}\".\nExpected: {1}\nAdding: {2}",
                            message,
                            previouslyRegisteredParameterType == null ? 0 : 1,
                            parameterType == null ? 0 : 1));
                    }
                }
            }
        }

        #endregion // Register

        #region Notify

        /// <summary>
        /// 发送消息通知，触发监听执行
        /// </summary>
        /// <param name="message">出发的消息name</param>
        /// <param name="parameter">参数</param>
        public void Notify(string message, object parameter)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException("'message' cannot be null or empty.");

            Type registeredParameterType;
            if (_messageToActionsMap.TryGetParameterType(message, out registeredParameterType))
            {
                if (registeredParameterType == null)
                    throw new TargetParameterCountException(string.Format("Cannot pass a parameter with message '{0}'. Registered action(s) expect no parameter.", message));
            }

            var actions = _messageToActionsMap.GetActions(message);
            if (actions != null)
                actions.ForEach(action => action.DynamicInvoke(parameter));
        }

        /// <summary>
        /// 发送消息通知，触发监听执行
        /// </summary>
        /// <param name="message">出发的消息name</param>
        public void Notify(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException("'message' cannot be null or empty.");

            Type registeredParameterType;
            if (_messageToActionsMap.TryGetParameterType(message, out registeredParameterType))
            {
                if (registeredParameterType != null)
                    throw new TargetParameterCountException(string.Format("Must pass a parameter of type {0} with this message. Registered action(s) expect it.", registeredParameterType.FullName));
            }

            var actions = _messageToActionsMap.GetActions(message);
            if (actions != null)
                actions.ForEach(action => action.DynamicInvoke());
        }

        #endregion // NotifyColleauges

        #region MessageToActionsMap [nested class]

        /// <summary>
        /// 
        /// </summary>
        private class MessageToActionsMap
        {
            #region Constructor

            internal MessageToActionsMap() { }
            #endregion // Constructor

            #region AddAction

            /// <summary>
            /// 根据message 添加相应的action
            /// </summary>
            /// <param name="message"></param>
            /// <param name="target"></param>
            /// <param name="method"></param>
            /// <param name="actionType"></param>
            internal void AddAction(string message, object target, MethodInfo method, Type actionType)
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                if (method == null)
                    throw new ArgumentNullException("method");

                lock (_map)
                {
                    if (!_map.ContainsKey(message))
                        _map[message] = new List<WeakAction>();
                    else
                    {
                        _map.Remove(message);
                        _map[message] = new List<WeakAction>();
                    }
                    _map[message].Add(new WeakAction(target, method, actionType));
                }
            }

            #endregion // AddAction

            #region GetActions

            /// <summary>
            ///获取指定的message actions
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            internal List<Delegate> GetActions(string message)
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                List<Delegate> actions;
                lock (_map)
                {
                    if (!_map.ContainsKey(message))
                        return null;

                    List<WeakAction> weakActions = _map[message];
                    actions = new List<Delegate>(weakActions.Count);
                    for (int i = weakActions.Count - 1; i > -1; --i)
                    {
                        WeakAction weakAction = weakActions[i];
                        if (weakAction == null)
                            continue;

                        Delegate action = weakAction.CreateAction();
                        if (action != null)
                        {
                            actions.Add(action);
                        }
                        else
                        {
                            weakActions.Remove(weakAction);
                        }
                    }
                    if (weakActions.Count == 0)
                        _map.Remove(message);
                }

                actions.Reverse();

                return actions;
            }

            #endregion // GetActions

            #region TryGetParameterType

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="parameterType">
            /// <returns></returns>
            internal bool TryGetParameterType(string message, out Type parameterType)
            {
                if (message == null)
                    throw new ArgumentNullException("message");

                parameterType = null;
                List<WeakAction> weakActions;
                lock (_map)
                {
                    if (!_map.TryGetValue(message, out weakActions) || weakActions.Count == 0)
                        return false;
                }
                parameterType = weakActions[0].ParameterType;
                return true;
            }

            #endregion // TryGetParameterType

            #region Fields

            // 存储调用的回调列表
            readonly Dictionary<string, List<WeakAction>> _map = new Dictionary<string, List<WeakAction>>();

            #endregion // Fields
        }

        #endregion // MessageToActionsMap [nested class]

        #region WeakAction [nested class]

        /// <summary>
        /// 这个类是一个实现细节的信息 ToActionsMap类。
        /// </summary>
        private class WeakAction
        {
            #region Constructor

            /// <summary>
            /// Constructs a WeakAction.
            /// </summary>
            /// <param name="target"></param>
            /// <param name="method"></param>
            /// <param name="parameterType"></param>
            internal WeakAction(object target, MethodInfo method, Type parameterType)
            {
                if (target == null)
                {
                    _targetRef = null;
                }
                else
                {
                    _targetRef = new WeakReference(target);
                }

                _method = method;

                this.ParameterType = parameterType;

                if (parameterType == null)
                {
                    _delegateType = typeof(Action);
                }
                else
                {
                    _delegateType = typeof(Action<>).MakeGenericType(parameterType);
                }
            }

            #endregion // Constructor

            #region CreateAction

            /// <summary>
            /// 
            /// </summary>
            internal Delegate CreateAction()
            {
                // 将该方法重新注入到实际操作对象中，以便可以调用该方法
                if (_targetRef == null)
                {
                    return Delegate.CreateDelegate(_delegateType, _method);
                }
                else
                {
                    try
                    {
                        object target = _targetRef.Target;
                        if (target != null)
                            return Delegate.CreateDelegate(_delegateType, target, _method);
                    }
                    catch
                    {
                    }
                }
                return null;
            }

            #endregion // CreateAction

            #region Fields

            internal readonly Type ParameterType;

            readonly Type _delegateType;
            readonly MethodInfo _method;
            readonly WeakReference _targetRef;

            #endregion // Fields
        }

        #endregion // 

        #region Fields

        readonly MessageToActionsMap _messageToActionsMap = new MessageToActionsMap();

        #endregion // Fields
    }
}
