using CardGame;
using CardPack;

namespace WarCardGame;

public class Player : IPlayer
{
    public Player(string name, int score)
    {
        Name = name;
        CardsInHand = new CardPack.CardPack($"{name} Cards In Hand");
        ;
        Score = score;
        CardsWon = new CardPack.CardPack($"{name} Cards Won");
    }

    public string Name { get; }
    public ICardPack CardsInHand { get; }
    public ICardPack CardsWon { get; init; }
    public int Score { get; set; }


    public void AddCardToHand(ICard card)
    {
        CardsInHand.InsertCard(card);
    }

    public void AddWonCards(List<ICard>? cards)
    {
        foreach (var card in cards)
        {
            CardsWon.InsertCard(card);
        }
    }

    public ICard PlayCard(Guid id)
    {
        ReloadWonCards();
        return CardsInHand.DrawCard(id) ?? throw new NullReferenceException();
    }

    private void ReloadWonCards()
    {
        if (CardsInHand.HasCards()) return;
        while (CardsWon.HasCards())
        {
            CardsInHand.InsertCard(CardsWon.DrawCard());
        }

        CardsInHand.Shuffle();
    }

    public ICard PlayCard()
    {
        ReloadWonCards();
        return CardsInHand.DrawCard() ?? throw new NullReferenceException();
    }


    public bool HasCards()
    {
        return CardsInHand.HasCards() || CardsWon.HasCards();
    }

    public int NumberOfCards()
    {
        return CardsInHand.CardsInDeck() + CardsWon.CardsInDeck();
    }
}