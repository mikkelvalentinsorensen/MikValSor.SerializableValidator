using NUnit.Framework;
using System;

namespace MikValSor.Runtime.Serialization.Test
{
	[TestFixture]
	public class SerializableValidatorTest
	{
		[Test]
		public void SerializableValidator_construct()
		{
			//Act
			new MikValSor.Runtime.Serialization.SerializableValidator();

			//Assert
			Assert.Pass();
		}

		[Serializable]
		public class CorrectSerializableTestClass : System.Runtime.Serialization.ISerializable
		{
			private readonly string m_Name;

			public CorrectSerializableTestClass(string name)
			{
				this.m_Name = name;
			}
			private CorrectSerializableTestClass(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				m_Name = (string)info.GetValue("Name", typeof(string));
			}

			public String Name
			{
				get
				{
					return this.m_Name;
				}
			}

			void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				info.AddValue("Name", m_Name, typeof(string));
			}
		}

		[Test]
		public void IsSerializable_object_CorrectSerializableTestClass()
		{
			//Arrange
			var validator = new SerializableValidator();
			var targetClass = new CorrectSerializableTestClass(String.Empty);

			//Act
			bool actual = validator.IsSerializable(targetClass);

			//Assert
			Assert.IsTrue(actual);
		}

		[Serializable]
		public class SecondCorrectSerializableTestClass : System.Runtime.Serialization.ISerializable
		{
			private readonly string m_Name;

			public SecondCorrectSerializableTestClass(string name)
			{
				this.m_Name = name;
			}
			private SecondCorrectSerializableTestClass(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				m_Name = (string)info.GetValue("Name", typeof(string));
			}

			public String Name
			{
				get
				{
					return this.m_Name;
				}
			}

			public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				info.AddValue("Name", m_Name, typeof(string));
			}
		}

		[Test]
		public void IsSerializable_object_SecondCorrectSerializableTestClass()
		{
			//Arrange
			var validator = new SerializableValidator();
			var targetClass = new SecondCorrectSerializableTestClass(String.Empty);

			//Act
			bool actual = validator.IsSerializable(targetClass);

			//Assert
			Assert.IsTrue(actual);
		}

		[Serializable]
		public class MissingConstructorSerializableTestClass : System.Runtime.Serialization.ISerializable
		{
			private readonly string m_Name;

			public MissingConstructorSerializableTestClass(string name)
			{
				this.m_Name = name;
			}

			public String Name
			{
				get
				{
					return this.m_Name;
				}
			}

			void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				info.AddValue("Name", m_Name, typeof(string));
			}
		}

		[Test]
		public void IsSerializable_object_MissingConstructorSerializableTestClass()
		{
			//Arrange
			var validator = new SerializableValidator();
			var targetClass = new MissingConstructorSerializableTestClass(String.Empty);

			//Act
			bool actual = validator.IsSerializable(targetClass);

			//Assert
			Assert.IsFalse(actual);
		}

		[Serializable]
		public class MissingInterfaceSerializableTestClass
		{
			private readonly string m_Name;

			public MissingInterfaceSerializableTestClass(string name)
			{
				this.m_Name = name;
			}
			private MissingInterfaceSerializableTestClass(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				m_Name = (string)info.GetValue("Name", typeof(string));
			}

			public String Name
			{
				get
				{
					return this.m_Name;
				}
			}

			void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				info.AddValue("Name", m_Name, typeof(string));
			}
		}

		[Test]
		public void IsSerializable_object_MissingInterfaceSerializableTestClass()
		{
			//Arrange
			var validator = new SerializableValidator();
			var targetClass = new MissingInterfaceSerializableTestClass(String.Empty);

			//Act
			bool actual = validator.IsSerializable(targetClass);

			//Assert
			Assert.IsFalse(actual);
		}

		public class NoSerializableAttributeSerializableTestClass : System.Runtime.Serialization.ISerializable
		{
			private readonly string m_Name;

			public NoSerializableAttributeSerializableTestClass(string name)
			{
				this.m_Name = name;
			}
			private NoSerializableAttributeSerializableTestClass(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				m_Name = (string)info.GetValue("Name", typeof(string));
			}

			public String Name
			{
				get
				{
					return this.m_Name;
				}
			}

			void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			{
				info.AddValue("Name", m_Name, typeof(string));
			}
		}

		[Test]
		public void IsSerializable_object_NoSerializableAttributeSerializableTestClass()
		{
			//Arrange
			var validator = new SerializableValidator();
			var targetClass = new NoSerializableAttributeSerializableTestClass(String.Empty);

			//Act
			bool actual = validator.IsSerializable(targetClass);

			//Assert
			Assert.IsFalse(actual);
		}
	}
}
