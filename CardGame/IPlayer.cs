using CardPack;

namespace CardGame;

public interface IPlayer
{
    public string Name { get;}
    public ICardPack CardsInHand { get; }
    public ICardPack CardsWon { get; init; }
    public int Score { get; set; }
    public void AddCardToHand(ICard card);
    
    public void AddWonCards(List<ICard>? card);
    public ICard PlayCard(Guid id);
    public ICard PlayCard();
    public bool HasCards();
    public int NumberOfCards();
}