// See https://aka.ms/new-console-template for more information

using CardGame;
using WarCardGame;

var players = new List<IPlayer>()
{
    new Player("Player 1", 0),
    new Player("Player 2", 0),
    new Player("Player 3", 0),
};
var war = new WarGame(players);
war.Play();