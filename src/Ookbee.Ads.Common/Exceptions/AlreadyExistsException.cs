using System;

namespace Ookbee.Ads.Common.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) Already Exists.")
        {
        }
    }
}
