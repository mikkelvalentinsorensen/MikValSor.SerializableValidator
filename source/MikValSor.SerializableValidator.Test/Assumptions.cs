using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MikValSor.Runtime.Serialization.Test
{
	[TestFixture]
	public class Assumptions
	{
		[Test]
		public void Guid_Serializable()
		{
			//Arange
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			Guid value = Guid.NewGuid();

			//Act
			binaryFormatter.Serialize(memoryStream, value);

			//Assert
			Assert.Pass();
		}

		[Test]
		public void String_Serializable()
		{
			//Arange
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			string value = "string";

			//Act
			binaryFormatter.Serialize(memoryStream, value);

			//Assert
			Assert.Pass();
		}

		[Test]
		public void Object_Serializable()
		{
			//Arange
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			object value = new object();

			//Act
			binaryFormatter.Serialize(memoryStream, value);

			//Assert
			Assert.Pass();
		}

		[Test]
		public void Int_Serializable()
		{
			//Arange
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			int value = 0;

			//Act
			binaryFormatter.Serialize(memoryStream, value);

			//Assert
			Assert.Pass();
		}

		private class myClass
		{
		}

		[Test]
		public void MyClass_NotSerializable()
		{
			//Arange
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			
			myClass value = new myClass();

			//Act
			try
			{
				binaryFormatter.Serialize(memoryStream, value);
			}

			//Assert
			catch (Exception)
			{
				Assert.Pass();
				return;
			}
			Assert.Fail();
		}
	}
}
