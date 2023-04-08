using System.Runtime.InteropServices;
using CardGame;
using CardPack;

namespace WarCardGame;

public class WarGame : IGame
{
    public IReadOnlyCollection<IPlayer> Players { get; init; }
    public ICardPack CardPack { get; init; }
    public PlayingTable PlayingTable { get; init; }

    public WarGame(ICardPack cardPack, IReadOnlyCollection<IPlayer> players)
    {
        CardPack = cardPack;
        Players = players;
        PlayingTable = new PlayingTable();
    }

    public void Play()
    {
        CardPack.Shuffle();

        DealCards();

        while (Players.Count(x => x.HasCards()) > 1)
        {
            PlayRound();
        }
    }

    private void DealCards()
    {
        while (CardPack.HasCards())
        {
            foreach (var player in Players)
            {
                if (!CardPack.HasCards()) break;
                player.AddCardToHand(CardPack.DrawCard());
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
            PlayingTable.ReceiveCard(card, player);

            Console.WriteLine(
                $"Player {player.Name} ({player.CardsInHand.CardsInDeck() + player.CardsWon.CardsInDeck()}) played {card.GetCardInfo()}");
        }

        //FindWinner(cardsInPlay, null);

        // var totalCards = 0;
        // foreach (var player in Players)
        // {
        //     // Console.WriteLine(
        //     //     $"Player {player.Name} ({player.CardsInHand.CardsInDeck() + player.CardsWon.CardsInDeck()})");
        //     totalCards += player.CardsInHand.CardsInDeck() + player.CardsWon.CardsInDeck();
        // }
        //
        // if (totalCards != 52)
        // {
        //     throw new ExternalException();
        // }
    }

    

    // private void PrintPlayerCards(IPlayer player)
    // {
    //     Console.WriteLine(player.Name);
    //     var cardInfo = player.CardsInHand.Cards.Select(card => card.GetCardInfo());
    //     var i = 0;
    //     foreach (var card in cardInfo)
    //     {
    //         Console.WriteLine(card + " " + i);
    //         i++;
    //     }
    //
    //     Console.WriteLine();
    // }
}