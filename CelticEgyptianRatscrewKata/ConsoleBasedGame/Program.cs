using System;
using System.Collections.Generic;
using System.Linq;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController game = new GameFactory().Create();

            var userInterface = new UserInterface();
            IEnumerable<PlayerInfo> playerInfos = userInterface.GetPlayerInfoFromUserLazily().ToList();

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                game.AddPlayer(new Player(playerInfo.PlayerName));
            }

            game.StartGame(GameFactory.CreateFullDeckOfCards());

            var inputProcessor = new InputProcessor(game, playerInfos);

            char userInput;
            while (userInterface.TryReadUserInput(out userInput))
            {
                var command = inputProcessor.ProcessKey(userInput);
                command.Execute();
            } 
        }
    }

    public interface ICommand
    {
        void Execute();
    }
}
