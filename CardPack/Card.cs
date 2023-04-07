namespace CardPack;

public class Card: ICard
{
    public Card(string name, CardSuit suit, int value)
    {
        Name = name;
        Suit = suit;
        Value = value;
        Id = new Guid();
    }

    public Guid Id { get; }
    public string Name { get; init; }

    public CardSuit Suit { get; init;}

    public int Value { get; init;}
    
    public string GetCardInfo()
    {
        return $"{Name} of {Suit.Name} ";
    }
}