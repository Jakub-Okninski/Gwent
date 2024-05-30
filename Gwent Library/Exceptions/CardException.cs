[Serializable]
public class CardException : Exception
{
	public CardException() { }
	public CardException(string message) : base(message) { }
	public CardException(string message, Exception inner) : base(message, inner) { }
	protected CardException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}