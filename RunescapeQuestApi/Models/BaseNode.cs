using System.Runtime.Serialization;

namespace RunescapeQuestApi.Models
{
    public abstract class BaseNode : ISerializable
    {
        public abstract NodeType Type { get; }
        public abstract object Value { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Type), Type.ToString());
            info.AddValue(nameof(Value), Value);
        }
    }
}