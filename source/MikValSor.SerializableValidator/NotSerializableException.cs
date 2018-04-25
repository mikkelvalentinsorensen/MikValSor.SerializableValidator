using System;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Base class for exceptions throwen when classes are being validatet for serilizability.
	/// </summary>
	public abstract class NotSerializableException : Exception
	{
		internal NotSerializableException(string message) : base(message)
		{
		}
	}
}
