using System;

public class StringValueAttribute : System.Attribute
{
	public string StringValue { get; protected set; }

	public StringValueAttribute(string value) {
		this.StringValue = value;
	}
}