using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;
using CelticEgyptianRatscrewKata.GameSetup;
using CelticEgyptianRatscrewKata.SnapRules;
using NUnit.Framework;

namespace CelticEgyptianRatscrewKata.Tests
{
    public class GameControllerTests
    {
        [Test]
        public void RedRouteValidSnapAfterSeveralCardsLaid()
        {
            // Arrange
            var gameController = CreateGameController();
            var playerA = new Player("playerA");
            var playerB = new Player("playerB");
            var playerC = new Player("playerC");
            var playerD = new Player("playerD");
            var deck = CreateNewSimpleDeck();

            // Act
            gameController.AddPlayer(playerA);
            gameController.AddPlayer(playerB);
            gameController.AddPlayer(playerC);
            gameController.AddPlayer(playerD);
            gameController.StartGame(deck);

            gameController.PlayCard(playerA);
            gameController.PlayCard(playerB);
            gameController.PlayCard(playerC);
            gameController.PlayCard(playerD);
            gameController.PlayCard(playerA);
            gameController.PlayCard(playerB);
            gameController.AttemptSnap(playerC);

            gameController.PlayCard(playerC);
            gameController.PlayCard(playerD);
            gameController.PlayCard(playerA);
            gameController.PlayCard(playerB);
            gameController.AttemptSnap(playerC);

            // Assert
            IPlayer winner;
            var hasWinner = gameController.TryGetWinner(out winner);
            Assert.True(hasWinner);
            Assert.That(winner.Name, Is.EqualTo(playerC.Name));
        }

        //Turn into test fixture
        [Test]
        public void GetsNextPlayer()
        {
            var gameController = CreateGameController();
            var playerA = new Player("Jane");
            var playerB = new Player("Joe");
            var playerC = new Player("Jeff");

            gameController.AddPlayer(playerA);
            gameController.AddPlayer(playerB);
            gameController.AddPlayer(playerC);

            Assert.AreEqual(gameController.NextPlayer(playerA), playerB);
        }

        [Test]
        public void GetsNextPlayerAtStartOfList()
        {
            var gameController = CreateGameController();
            var playerA = new Player("Jane");
            var playerB = new Player("Joe");
            var playerC = new Player("Jeff");

            gameController.AddPlayer(playerA);
            gameController.AddPlayer(playerB);
            gameController.AddPlayer(playerC);

            Assert.AreEqual(gameController.NextPlayer(playerC), playerA);
        }

        private static GameController CreateGameController()
        {
            var gameState = new GameState();
            var completeSnapValidator = CreateCompleteSnapValidator();
            var dealer = new Dealer();
            var noneShufflingShuffler = new NoneShufflingShuffler();

            return new GameController(gameState, completeSnapValidator, dealer, noneShufflingShuffler);
        }

        private static ISnapValidator CreateCompleteSnapValidator()
        {
            var rules = new List<IRule>
                        {
                            new DarkQueenSnapRule(),
                            new SandwichSnapRule(),
                            new StandardSnapRule()
                        };
            return new SnapValidator(rules);
        }

        public static Cards CreateNewSimpleDeck()
        {
            return Cards.With(
                new Card(Suit.Clubs, Rank.Three),
                new Card(Suit.Diamonds, Rank.Three),
                new Card(Suit.Clubs, Rank.Five),
                new Card(Suit.Clubs, Rank.Four),
                new Card(Suit.Clubs, Rank.Six),
                new Card(Suit.Diamonds, Rank.Seven),
                new Card(Suit.Clubs, Rank.Eight),
                new Card(Suit.Clubs, Rank.Seven),
                new Card(Suit.Clubs, Rank.Ten),
                new Card(Suit.Clubs, Rank.Nine)
                );
        }
    }

    public class NoneShufflingShuffler : IShuffler
    {
        public Cards Shuffle(Cards deck)
        {
            return new Cards(deck);
        }
    }
}