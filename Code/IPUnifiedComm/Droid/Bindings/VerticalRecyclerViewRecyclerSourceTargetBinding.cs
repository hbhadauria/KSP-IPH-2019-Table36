using System;
using System.Collections;
using IPUnifiedComm.Droid.Views.Controls;

namespace IPUnifiedComm.Droid.Bindings
{
    public class VerticalRecyclerViewRecyclerSourceTargetBinding : BaseTargetBinding<IEnumerable, VerticalRecyclerView>
    {
        public VerticalRecyclerViewRecyclerSourceTargetBinding(VerticalRecyclerView targetObject) : base(targetObject)
        {
        }

        protected override void DoSetValueImpl(VerticalRecyclerView target, IEnumerable value)
        {
            target.ItemsSource = value;
        }
    }
}