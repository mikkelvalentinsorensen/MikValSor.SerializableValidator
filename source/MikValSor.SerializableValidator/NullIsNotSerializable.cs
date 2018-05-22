using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Exception class used for signaling when classes does not implement System.Runtime.Serialization.ISerializable interface.
	/// </summary>
	public sealed class NullIsNotSerializable : NotSerializableException
	{
		internal NullIsNotSerializable() : base($"Null is not serializable")
		{
		}
	}
}
