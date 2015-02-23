using CelticEgyptianRatscrewKata;
using CelticEgyptianRatscrewKata.Game;
using NSubstitute;
using NUnit.Framework;

namespace ConsoleBasedGame.Tests
{
    class InputProcessorTests
    {
        [Test]
        public void CardIsPlayedOnCorrectKey()
        {
            var gameController = Substitute.For<IGameController>();
            var playerInfo = new PlayerInfo("Fred", 'a', 'b');
            var inputProcessor = new InputProcessor(gameController, new [] {playerInfo});
            var logger = Substitute.For<ILogger>();
            inputProcessor.ProcessKey('a').Execute(logger);
            gameController.Received().PlayCard(Arg.Is<Player>(p=>p.Name == "Fred"));
        }

        [Test]
        public void PlayingCardLogsMessage()
        {
            var gameController = Substitute.For<IGameController>();
            gameController.PlayCard(Arg.Is<Player>(p => p.Name == "Fred")).Returns(new Card(Suit.Clubs, Rank.Eight));
            var playerInfo = new PlayerInfo("Fred", 'a', 'b');
            var inputProcessor = new InputProcessor(gameController, new[] { playerInfo });
            var logger = Substitute.For<ILogger>();
            inputProcessor.ProcessKey('a').Execute(logger);
            logger.Received().Write("Fred has played the Eight of Clubs");
        }
    }
}
