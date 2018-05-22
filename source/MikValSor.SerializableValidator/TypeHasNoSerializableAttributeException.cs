using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Exception class used for signaling when classes are missing System.SerializableAttribute attribute.
	/// </summary>
	public sealed class TypeHasNoSerializableAttributeException : NotSerializableException
	{
		internal TypeHasNoSerializableAttributeException(Type type) : base($"Type: {type.FullName}")
		{
			Data.Add("Type", type);
		}
	}
}
