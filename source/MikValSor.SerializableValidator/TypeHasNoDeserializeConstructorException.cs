using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Exception class used for signaling when classes are missing constructor for deserialization.
	/// </summary>
	public sealed class TypeHasNoDeserializeConstructorException : NotSerializableException
	{
		internal TypeHasNoDeserializeConstructorException(Type type) : base($"Type: {type.FullName}")
		{
			Data.Add("Type", type);
		}
	}
}
