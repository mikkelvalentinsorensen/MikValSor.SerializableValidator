using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace MikValSor.Runtime.Serialization
{
	/// <summary>
	///		Class for validating value types, object and type definitions to check if they are serializable.
	/// </summary>
	public sealed class SerializableValidator
	{
		/// <summary>
		///		Construct a new instance of SerializableValidator.
		/// </summary>
		public SerializableValidator()
		{
		}


		/// <summary>
		///		Checks if target object is serializable.
		/// </summary>
		/// <param name="target">
		///		Targeted object for serializable validation.
		/// </param>
		/// <returns>
		///		Returns True if object is serializable.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Throws System.ArgumentNullException if target is null.
		/// </exception>
		public bool IsSerializable(object target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			try
			{
				EnsureSerializable(target.GetType());
			}
			catch (NotSerializableException)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		///		Checks if type garenties object is serializable. 
		/// </summary>
		/// <param name="targetType">
		///		Targeted type for serializable validation.
		/// </param>
		/// <returns>
		///		Return True if type is serializable.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Throws System.ArgumentNullException if targetType is null.
		/// </exception>
		public bool IsSerializable(Type targetType)
		{
			if (targetType == null) throw new ArgumentNullException(nameof(targetType));
			try
			{
				EnsureSerializable(targetType);
			}
			catch (NotSerializableException)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		///		Checks if type garenties object is serializable. 
		/// </summary>
		/// <param name="target">
		///		Targeted object for serializable validation.
		/// </param>
		/// <exception cref="TypeDoesNotImplementISerializable">
		///		Throws TypeDoesNotImplementISerializable is target class does not implement System.Runtime.Serialization.ISerializable.
		/// </exception>
		/// <exception cref="TypeHasNoDeserializeConstructor">
		///		Throws TypeHasNoDeserializeConstructor is target does not have class constructor with parameters System.Runtime.Serialization.SerializationInfo and System.Runtime.Serialization.StreamingContext.
		/// </exception>
		/// <exception cref="TypeHasNoSerializableAttribute">
		///		Throws TypeHasNoSerializableAttribute is target does not have System.SerializableAttribute attribute on class.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		///		Throws System.ArgumentNullException if target is null.
		/// </exception>
		public void EnsureSerializable(object target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			EnsureSerializable(target.GetType());
		}

		/// <summary>
		///		Checks if type garenties object is serializable. 
		/// </summary>
		/// <param name="targetType">
		///		Targeted type for serializable validation.
		/// </param>
		/// <exception cref="TypeDoesNotImplementISerializable">
		///		Throws TypeDoesNotImplementISerializable is targetType does not implement System.Runtime.Serialization.ISerializable.
		/// </exception>
		/// <exception cref="TypeHasNoDeserializeConstructor">
		///		Throws TypeHasNoDeserializeConstructor is targetType does not have class constructor with parameters System.Runtime.Serialization.SerializationInfo and System.Runtime.Serialization.StreamingContext.
		/// </exception>
		/// <exception cref="TypeHasNoSerializableAttribute">
		///		Throws TypeHasNoSerializableAttribute is targetType does not have System.SerializableAttribute attribute on class.
		/// </exception>
		public void EnsureSerializable(Type targetType)
		{
			if (TryGetPreviousResult(targetType, out bool previousResult))
			{
				if (previousResult) return;
			}

			EnsureSerializableUncached(targetType);

			AddResult(targetType, true);
		}

		private readonly Type ISerializableType = typeof(ISerializable);
		private void EnsureSerializableUncached(Type targetType)
		{
			if (!targetType.IsSerializable) throw new TypeHasNoSerializableAttribute(targetType);

			if (targetType.IsClass)
			{
				if (!targetType.GetInterfaces().Contains(ISerializableType)) throw new TypeDoesNotImplementISerializable(targetType);
				EnsureDeserializeConstructors(targetType);
			}
		}

		private readonly Type[] ConstructorParameterTypes = new Type[] { typeof(SerializationInfo), typeof(StreamingContext) };
		private void EnsureDeserializeConstructors(Type targetType)
		{
			var publicConstructor = targetType.GetConstructor(ConstructorParameterTypes);
			if (publicConstructor != null) return;

			var privateConstructor = targetType.GetConstructor((BindingFlags.NonPublic | BindingFlags.Instance), null, ConstructorParameterTypes, null);
			if (privateConstructor != null) return;

			throw new TypeHasNoDeserializeConstructor(targetType);
		}

		private readonly Dictionary<string, bool> PreviousResults = new Dictionary<string, bool>()
			{
				{typeof(int).FullName, true },
				{typeof(bool).FullName, true },
				{typeof(byte).FullName, true },
				{typeof(char).FullName, true },
				{typeof(decimal).FullName, true },
				{typeof(double).FullName, true },
				{typeof(float).FullName, true },
				{typeof(long).FullName, true },
				{typeof(sbyte).FullName, true },
				{typeof(short).FullName, true },
				{typeof(string).FullName, true },
				{typeof(uint).FullName, true },
				{typeof(ulong).FullName, true },
				{typeof(ushort).FullName, true },
				{typeof(object).FullName, true }
			};
		private readonly object InsertionLockObject = new object();

		private bool TryGetPreviousResult(Type type, out bool result)
		{
			return PreviousResults.TryGetValue(type.FullName, out result);
		}

		private void AddResult(Type type, bool result)
		{
			var name = type.FullName;

			if (PreviousResults.ContainsKey(name)) return;

			lock (InsertionLockObject)
			{
				if (PreviousResults.ContainsKey(name)) return;
				PreviousResults.Add(name, result);
			}
		}
	}
}
