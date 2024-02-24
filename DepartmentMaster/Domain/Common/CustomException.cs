using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CMSBlogMaster_BL.Domain.Common
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomException : Exception, ISerializable
    {
        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Implement the ISerializable interface
        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
