using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
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
}