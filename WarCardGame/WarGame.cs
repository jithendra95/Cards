using System.Runtime.InteropServices;
using CardPack;

namespace WarCardGame;

public class WarGame : IGame
{
    public IReadOnlyCollection<IPlayer> Players { get; init; }
    public ICardPack CardPack { get; init; }

    public WarGame(ICardPack cardPack, IReadOnlyCollection<IPlayer> players)
    {
        CardPack = cardPack;
        Players = players;
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
        var cardsInPlay = new List<(ICard card, IPlayer player)>();
        foreach (var player in Players)
        {
            if (!player.HasCards()) continue;
            var card = player.PlayCard();
            cardsInPlay.Add((card, player));

            Console.WriteLine(
                $"Player {player.Name} ({player.CardsInHand.CardsInDeck() + player.CardsWon.CardsInDeck()}) played {card.GetCardInfo()}");
        }

        FindWinner(cardsInPlay, null);

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

    private void FindWinner(List<(ICard card, IPlayer player)> cardsInPlay, List<ICard>? winningPot)
    {
        if (IsThereAWar(cardsInPlay.Select(x => x.card).ToList()))
        {
            Console.WriteLine("There is a war!");
            ResolveWar(cardsInPlay, winningPot);
            return;
        }

        var winner = GetWinner(cardsInPlay);
        winner.AddWonCards(cardsInPlay.Select(x => x.card).ToList());
        if (winningPot != null)
            winner.AddWonCards(winningPot);

        Console.WriteLine($"Winner {winner.Name}  \n");
    }

    private void ResolveWar(List<(ICard card, IPlayer player)> cardsInPlayByPlayer, List<ICard>? winningPot)
    {
        var warValue = cardsInPlayByPlayer.GroupBy(x => x.card.Value)
            .Where(x => x.Count() > 1)
            .Select(i => i.Key)
            .First();
        Console.WriteLine($" War on {warValue}");
        var warPlayers = cardsInPlayByPlayer.Where(x => x.card.Value == warValue).Select(x => x.player).ToList();
        var allWinnings = cardsInPlayByPlayer.Select(x => x.card).ToList();
        if(winningPot != null)
            allWinnings.AddRange(winningPot);
        
        var newCardsInPlay = new List<(ICard card, IPlayer player)>();

        foreach (var player in warPlayers)
        {
            var cardOfPlayer = new List<ICard>();
            for (int i = 0; i < 4; i++)
            {
                if (player.HasCards())
                {
                    cardOfPlayer.Add(player.PlayCard());
                }
            }

            if (cardOfPlayer.Count == 0) continue;
            var lastPlayedCard = cardOfPlayer.Last();
            cardOfPlayer.Remove(
                lastPlayedCard); // Remove the last card because it will added to the winnings in FindWinner.
            newCardsInPlay.Add((lastPlayedCard, player));
            allWinnings.AddRange(cardOfPlayer);
        }

        FindWinner(newCardsInPlay, allWinnings);
    }

    private bool IsThereAWar(List<ICard> cardsInPlay)
    {
        return cardsInPlay.GroupBy(x => x.Value).Any(x => x.Count() > 1);
    }

    private IPlayer GetWinner(List<(ICard card, IPlayer player)> cardsInPlay)
    {
        var winner = cardsInPlay[0];
        foreach (var (card, player) in cardsInPlay)
        {
            if (card.Value > winner.card.Value)
            {
                winner = (card, player);
            }
        }

        return winner.player;
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