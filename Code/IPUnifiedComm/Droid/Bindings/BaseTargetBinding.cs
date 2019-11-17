using MvvmCross.Binding;
using MvvmCross.Platforms.Android.Binding.Target;
using System;

namespace IPUnifiedComm.Droid.Bindings
{
    public abstract class BaseTargetBinding<TValue, TTarget> : MvxAndroidTargetBinding
         where TTarget : class
    {
        private TTarget targetObject;

        public override Type TargetType { get { return typeof(TValue); } }
        public override MvxBindingMode DefaultMode { get { return MvxBindingMode.OneWay; } }

        protected TTarget TargetTyped
        {
            get { return targetObject; }
            set
            {
                targetObject = value;
                SubscribeToTargetEvents(targetObject);
            }
        }

        protected BaseTargetBinding(TTarget targetObject) : base(targetObject)
        {
            TargetTyped = targetObject;
        }

        protected override void SetValueImpl(object target, object value)
        {
            var obj = ConvertValue(value);
            var convertedTarget = ConvertTarget(target);
            if (convertedTarget == null)
                return;

            DoSetValueImpl(convertedTarget, obj);
        }

        protected virtual TValue ConvertValue(object value)
        {
            return (TValue)value;
        }

        protected virtual TTarget ConvertTarget(object target)
        {
            return target as TTarget;
        }

        protected virtual void DoSetValueImpl(TTarget target, TValue value)
        {
        }

        protected override void Dispose(bool isDisposing)
        {
            UnsubscribeFromTargetEvents(TargetTyped);
            DoOnDispose();
            base.Dispose(isDisposing);
        }

        protected virtual void DoOnDispose()
        {
        }

        protected virtual void SubscribeToTargetEvents(TTarget target)
        {
        }

        protected virtual void UnsubscribeFromTargetEvents(TTarget target)
        {
        }
    }
}