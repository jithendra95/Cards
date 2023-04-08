// See https://aka.ms/new-console-template for more information

using CardGame;
using WarCardGame;

var deck = new CardPack.CardPack("War Deck");
deck.InitializeCardPack();

for (int i = 0; i < 10; i++)
{
    deck.Shuffle();
}


var players = new List<IPlayer>()
{
    new Player("Player 1", 0),
    new Player("Player 2", 0)
};
var war = new WarGame(deck, players);
war.Play();