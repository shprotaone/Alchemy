using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LimitedNumbers
{
    [Serializable]
    public sealed class LimitedNumber
    {
        public event ValueSetupedDelegate OnValueSetuped;

        public event ValueIncrementedDelegate OnValueIncremented;

        public event ValueDecrementedDelegate OnValueDecremented;

        public event ValueChangedDelegate OnValueChanged;

        public int Value
        {
            get { return this.value; }
        }

        public int Limit
        {
            get { return this.limit; }
        }

        public bool IsLimit
        {
            get { return this.value >= this.limit; }
        }

        public bool IsZero
        {
            get { return this.value <= 0; }
        }

        public int AvailableRange
        {
            get { return this.limit - this.value; }
        }

        [SerializeField]
        private int value;

        [SerializeField]
        private int limit = 10;

        public LimitedNumber()
        {
        }

        public LimitedNumber(int value, int limit)
        {
            this.limit = Math.Max(limit, 0);
            this.value = Mathf.Clamp(value, 0, this.limit);
        }

        public void Setup(int value, int limit)
        {
            this.limit = Math.Max(limit, 0);
            this.value = Mathf.Clamp(value, 0, this.limit);
            this.OnValueSetuped?.Invoke(this.value);
        }

        public void Decrement(int range)
        {
            if (this.IsZero)
            {
                return;
            }

            var previousValue = this.value;
            var newValue = this.value - range;
            newValue = Math.Max(newValue, 0);
            this.value = newValue;
            this.OnValueChanged?.Invoke(previousValue, newValue);
            this.OnValueDecremented?.Invoke(previousValue, newValue);
        }

        public void Increment(int range)
        {
            if (this.IsLimit)
            {
                return;
            }

            var previousValue = this.value;
            var newValue = this.value + range;
            newValue = Math.Min(newValue, this.limit);

            this.value = newValue;
            this.OnValueChanged?.Invoke(previousValue, newValue);
            this.OnValueIncremented?.Invoke(previousValue, newValue);
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            this.Setup(this.value, this.limit);
        }
#endif
    }
}