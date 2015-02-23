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
            inputProcessor.ProcessKey('a').Execute();
            gameController.Received().PlayCard(Arg.Is<Player>(p=>p.Name == "Fred"));
        }
    }
}
