[Serializable]
public class RogException : Exception
{
	public RogException() { }
	public RogException(string message) : base(message) { }
	public RogException(string message, Exception inner) : base(message, inner) { }
	protected RogException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
