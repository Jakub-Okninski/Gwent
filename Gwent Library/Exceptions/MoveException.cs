[Serializable]
public class MoveException : Exception
{
	public MoveException() { }
	public MoveException(string message) : base(message) { }
	public MoveException(string message, Exception inner) : base(message, inner) { }
	protected MoveException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}