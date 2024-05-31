[Serializable]
public class EndGameException : Exception
{
	public EndGameException() { }
	public EndGameException(string message) : base(message) { }
	public EndGameException(string message, Exception inner) : base(message, inner) { }
	protected EndGameException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}