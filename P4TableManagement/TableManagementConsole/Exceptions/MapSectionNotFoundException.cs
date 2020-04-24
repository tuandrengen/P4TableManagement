using System;
using System.Runtime.Serialization;

namespace TableManagementConsole
{
    [Serializable]
    internal class MapSectionNotFoundException : Exception
    {
        public MapSectionNotFoundException()
        {
        }

        public MapSectionNotFoundException(string message) : base(message)
        {
        }

        public MapSectionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MapSectionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}