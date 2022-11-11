using UnityEngine;

namespace LimitedNumbers
{
    [AddComponentMenu("LimitedNumbers/Limited Number")]
    public sealed class MonoLimitedNumber : MonoBehaviour
    {
        public event ValueSetupedDelegate OnValueSetuped
        {
            add { this.@delegate.OnValueSetuped += value; }
            remove { this.@delegate.OnValueSetuped -= value; }
        }

        public event ValueIncrementedDelegate OnValueIncremented
        {
            add { this.@delegate.OnValueIncremented += value; }
            remove { this.@delegate.OnValueIncremented -= value; }
        }

        public event ValueDecrementedDelegate OnValueDecremented
        {
            add { this.@delegate.OnValueDecremented += value; }
            remove { this.@delegate.OnValueDecremented -= value; }
        }

        public event ValueChangedDelegate OnValueChanged
        {
            add { this.@delegate.OnValueChanged += value; }
            remove { this.@delegate.OnValueChanged -= value; }
        }

        public bool IsLimit
        {
            get { return this.@delegate.IsLimit; }
        }

        public bool IsZero
        {
            get { return this.@delegate.IsZero; }
        }

        public int Value
        {
            get { return this.@delegate.Value; }
        }

        public int AvailableRange
        {
            get { return this.@delegate.AvailableRange; }
        }

        public int Limit
        {
            get { return this.@delegate.Limit; }
        }

        [SerializeField]
        private LimitedNumber @delegate = new LimitedNumber();

        public void Increment(int range)
        {
            this.@delegate.Increment(range);
        }

        public void Decrement(int range)
        {
            this.@delegate.Decrement(range);
        }

        public void Setup(int count, int capacity)
        {
            this.@delegate.Setup(count, capacity);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.@delegate.OnValidate();
        }
#endif
    }
}