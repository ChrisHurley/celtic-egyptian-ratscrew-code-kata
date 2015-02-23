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

    internal class InputProcessor
    {
        private readonly IGameController m_Game;
        private readonly IEnumerable<PlayerInfo> m_PlayerInfos;

        public InputProcessor(IGameController game, IEnumerable<PlayerInfo> playerInfos)
        {
            m_Game = game;
            m_PlayerInfos = playerInfos;
        }

        public ICommand ProcessKey(char userInput)
        {
            return null;
        }
    }

    internal class PlayCardCommand : ICommand
    {
        private readonly IGameController m_Game;
        private readonly IPlayer m_Player;

        public PlayCardCommand(IGameController game, IPlayer player)
        {
            m_Game = game;
            m_Player = player;
        }

        public void Execute()
        {
            m_Game.PlayCard(m_Player);
        }
    }
}
