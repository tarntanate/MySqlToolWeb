using System;

namespace Ookbee.Ads.Common.Exceptions
{
    public class UnexpectedEnumValueException<T> : Exception
    {
        public UnexpectedEnumValueException(T value)
            : base($"Value {value} of enum {typeof(T).Name} is not supported.")
        {
            
        }
    }
}