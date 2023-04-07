namespace CardPack;

public interface ICardPack
{
    public string Name { get; set; }
    public ICard? DrawCard(Guid id);
    public ICard DrawCard();
    public ICard DrawRandomCard();
    public void InsertCard(ICard card);
    public bool HasCards();
    public void Shuffle();
    public void InitializeCardPack();
    public int CardsInDeck();
    
}