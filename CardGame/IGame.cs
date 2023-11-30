using CardPack;
using WarCardGame;

namespace CardGame;

public interface IGame
{
    public IReadOnlyCollection<IPlayer> Players { get; init; }
    public void Play();

}