using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Exception class used for signaling when null.
	/// </summary>
	public sealed class NullIsNotSerializable : NotSerializableException
	{
		internal NullIsNotSerializable() : base($"Null is not serializable")
		{
		}
	}
}
