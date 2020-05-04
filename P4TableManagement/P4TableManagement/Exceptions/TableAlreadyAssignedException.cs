using System;
using System.Runtime.Serialization;

namespace P4TableManagement
{
    [Serializable]
    internal class TableAlreadyAssignedException : Exception
    {
        public TableAlreadyAssignedException()
        {
        }

        public TableAlreadyAssignedException(string message) : base(message)
        {
        }

        public TableAlreadyAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TableAlreadyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}