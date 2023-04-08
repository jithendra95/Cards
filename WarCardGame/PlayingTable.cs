using CardGame;
using CardPack;

namespace WarCardGame;

public class PlayingTable
{
    List<(ICard card, IPlayer player)> cardsInPlayByPlayer = new();
    List<ICard> winningPot = new();

    public void ReceiveCard(ICard card, IPlayer player)
    {
        cardsInPlayByPlayer.Add((card, player));
    }

    public IPlayer FindWinner()
    {
        if (IsThereAWar())
        {
            Console.WriteLine("There is a war!");
            return ResolveWar();
        }

        var winner = cardsInPlayByPlayer[0];
        foreach (var (card, player) in cardsInPlayByPlayer)
        {
            if (card.Value > winner.card.Value)
            {
                winner = (card, player);
            }
        }
        winningPot.AddRange(cardsInPlayByPlayer.Select(x => x.card));
        return winner.player;

        // winner.AddWonCards(cardsInPlay.Select(x => x.card).ToList());
        // if (winningPot != null)
        //     winner.AddWonCards(winningPot);
        //
        // Console.WriteLine($"Winner {winner.Name}  \n");
    }

    public List<ICard> GetWinningCards()
    {
        return winningPot;
    }


    private IPlayer ResolveWar()
    {
        var warValue = cardsInPlayByPlayer.GroupBy(x => x.card.Value)
            .Where(x => x.Count() > 1)
            .Select(i => i.Key)
            .First();
        Console.WriteLine($" War on {warValue}");
        var warPlayers = cardsInPlayByPlayer.Where(x => x.card.Value == warValue).Select(x => x.player).ToList();
        winningPot.AddRange(cardsInPlayByPlayer.Select(x => x.card).ToList());
        cardsInPlayByPlayer.Clear();

        foreach (var player in warPlayers)
        {
            
        }
        foreach (var player in warPlayers)
        {
            var cardsOfPlayer = new List<ICard>();
            for (int i = 0; i < 3; i++)
            {
                if (player.HasCards())
                {
                    cardsOfPlayer.Add(player.PlayCard());
                }
            }

            if (cardsOfPlayer.Count == 0) continue;
            winningPot.AddRange(cardsOfPlayer);
            cardsInPlayByPlayer.Add((player.PlayCard(), player));
        }

        return FindWinner();
    }

    private bool IsThereAWar()
    {
        return cardsInPlayByPlayer.Select(cardAndPlayer => cardAndPlayer.card).GroupBy(x => x.Value)
            .Any(x => x.Count() > 1);
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
}