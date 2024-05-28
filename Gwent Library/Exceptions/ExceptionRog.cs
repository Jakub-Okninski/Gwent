[Serializable]
public class ExceptionRog : Exception
{
	public ExceptionRog() { }
	public ExceptionRog(string message) : base(message) { }
	public ExceptionRog(string message, Exception inner) : base(message, inner) { }
	protected ExceptionRog(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
