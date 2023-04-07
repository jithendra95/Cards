namespace CardPack;

public class CardPack : ICardPack
{
    public string Name { get; set; }
    private IList<ICard> Cards { get; set; }

    private readonly List<CardSuit> _suits = new()
    {
        new CardSuit("Hearts", Color.Red, "♥"),
        new CardSuit("Diamonds", Color.Red, "♦"),
        new CardSuit("Spades", Color.Black, "♠"),
        new CardSuit("Clubs", Color.Black, "♣")
    };

    private readonly List<(string name, int value)> _cardNamesAndValues = new()
    {
        ("Ace", 1),
        ("Two", 2),
        ("Three", 3),
        ("Four", 4),
        ("Five", 5),
        ("Six", 6),
        ("Seven", 7),
        ("Eight", 8),
        ("Nine", 9),
        ("Ten", 10),
        ("Jack", 11),
        ("Queen", 12),
        ("King", 13)
    };

    public CardPack(string name)
    {
        Name = name;
        Cards = new List<ICard>();
    }

    public ICard? DrawCard(Guid id)
    {
        return Cards.FirstOrDefault(x => x.Id == id, null);
    }

    public ICard DrawCard()
    {
        var card = Cards.Last();
        Cards.Remove(card);
        return card;
    }

    public ICard DrawRandomCard()
    {
        var rand = new Random();
        var index = rand.Next(Cards.Count);
        return Cards.ElementAt(index);
    }

    public void InsertCard(ICard card)
    {
        Cards.Add(card);
    }

    public bool HasCards()
    {
        return Cards.Count > 0;
    }

    public void Shuffle()
    {
        var rand = new Random();
        Cards = Cards.OrderBy(_ => rand.Next()).ToList();
    }

    public void InitializeCardPack()
    {
        _suits.ForEach(suit =>
            _cardNamesAndValues.ForEach(
                cardNameAndValue =>
                {
                    var card = new Card(cardNameAndValue.name, suit, cardNameAndValue.value);
                    Cards.Add(card);
                }));
    }

    public int CardsInDeck() => Cards.Count;
}