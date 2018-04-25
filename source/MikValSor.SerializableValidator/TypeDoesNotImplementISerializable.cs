using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Exception class used for signaling when classes does not implement System.Runtime.Serialization.ISerializable interface.
	/// </summary>
	public sealed class TypeDoesNotImplementISerializable : NotSerializableException
	{
		internal TypeDoesNotImplementISerializable(Type type) : base($"Type: {type.FullName}")
		{
			Data.Add("Type", type);
		}
	}
}
