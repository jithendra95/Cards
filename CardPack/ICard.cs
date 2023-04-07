namespace CardPack;

public interface ICard
{
    public string Name { get; init; }
    public Guid Id { get; }

    public CardSuit Suit { get; init;}

    public int Value { get; init;}
    
    public string GetCardInfo();
}