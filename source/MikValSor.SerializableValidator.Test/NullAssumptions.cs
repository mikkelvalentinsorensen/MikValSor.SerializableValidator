using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MikValSor.Runtime.Serialization.Test
{
	[TestFixture]
	public class NullAssumptions
	{
		[Test]
		public void NullNotSerializable()
		{
			//Arrange
			var m = new MemoryStream();
			var formatter = new BinaryFormatter();

			//Act
			try
			{
				formatter.Serialize(m, null);
			}

			//Assert
			catch (ArgumentNullException)
			{
				Assert.Pass();
			}
			Assert.Fail();
		}
	}
}
