namespace CardPack;

public class CardSuit
{
    public string Name { get; }
    public Color Color { get; }
    public string Symbol { get; }
    
    public CardSuit(string name, Color color, string symbol)
    {
        Name = name;
        Color = color;
        Symbol = symbol;
    }

}

public enum Color
{
    Red,
    Black
}