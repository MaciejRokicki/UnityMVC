namespace MVC.Common
{
    public struct ChangedValue<T>
    {
        public T PreviousValue { get; set; }
        public T NewValue { get; set; }

        public ChangedValue(T previousValue, T newValue)
        {
            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}
