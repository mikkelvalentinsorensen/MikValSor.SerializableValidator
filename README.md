Library for validating that .NET types are serializable and deserialize with System.Runtime.Serialization. 

Nuget package: [https://www.nuget.org/packages/MikValSor.SerializableValidator](https://www.nuget.org/packages/MikValSor.SerializableValidator)

## Example:
```cs
class MyClass
{
	public string Value;
}

[System.Serializable]
class MyOtherClass : System.Runtime.Serialization.ISerializable
{
	public string Value;

	public MyOtherClass()
	{
	}

	private MyOtherClass(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
	{
		Value = (string)info.GetValue("Value", typeof(string));
	}

	void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
	{
		info.AddValue("Value", Value, typeof(string));
	}
}

void Validate()
{
	var validator = new MikValSor.Runtime.Serialization.SerializableValidator();

	var myObject = new MyClass();
	bool isMyObjectImmutable = validator.IsSerializable(myObject);
	System.Console.WriteLine($"Is myObject serializable: {isMyObjectImmutable}");

	var myOtherObject = new MyOtherClass();
	bool isMyOtherObjectImmutable = validator.IsSerializable(myOtherObject);
	System.Console.WriteLine($"Is myOtherObject serializable: {isMyOtherObjectImmutable}");
}
/**
	Output:
	Is myObject serializable: False
	Is myOtherObject serializable: True
**/
```
