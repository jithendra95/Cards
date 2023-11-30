using CardGame;
using CardPack;

namespace WarCardGame;

public class WarGame : IGame
{
    public IReadOnlyCollection<IPlayer> Players { get; init; }
    private readonly ICardPack _cardPack;
    private readonly PlayingTable _playingTable;

    public WarGame( IReadOnlyCollection<IPlayer> players)
    {
        _cardPack = new CardPack.CardPack("War Deck");
        _cardPack.InitializeCardPack();

        for (var i = 0; i < 10; i++)
        {
            _cardPack.Shuffle();
        }
        
        Players = players;
        _playingTable = new PlayingTable();
    }

    public void Play()
    {
        _cardPack.Shuffle();

        DealCards();

        while (Players.Count(x => x.HasCards()) > 1)
        {
            PlayRound();
        }
    }

    private void DealCards()
    {
        while (_cardPack.HasCards())
        {
            foreach (var player in Players)
            {
                if (!_cardPack.HasCards()) break;
                player.AddCardToHand(_cardPack.DrawCard());
            }
        }

        foreach (var player in Players)
        {
            player.CardsInHand.Shuffle();
        }
    }

    private void PlayRound()
    {
        
        foreach (var player in Players)
        {
            if (!player.HasCards()) continue;
            var card = player.PlayCard();
            _playingTable.ReceiveCard(card, player);

            Console.WriteLine(
                $"Player {player.Name} ({player.CardsInHand.CardsInDeck() + player.CardsWon.CardsInDeck()}) played {card.GetCardInfo()}");
        }

        var winner = _playingTable.FindWinner();
        winner?.AddWonCards(_playingTable.GetWinningCards());

    }
}