using System.Collections.Generic;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    internal class InputProcessor
    {
        private readonly IGameController m_Game;
        private IDictionary<char, ICommand> m_KeyToCommand;

        public InputProcessor(IGameController game, IEnumerable<PlayerInfo> playerInfos)
        {
            m_Game = game;
            m_KeyToCommand = GenerateKeysToCommandsMap(playerInfos);
        }

        private IDictionary<char, ICommand> GenerateKeysToCommandsMap(IEnumerable<PlayerInfo> playerInfos)
        {
            var map = new Dictionary<char, ICommand>();

            foreach (var playerInfo in playerInfos)
            {
                map.Add(playerInfo.PlayCardKey, new PlayCardCommand(m_Game, playerInfo.Player));
            }

            return map;
        }

        public ICommand ProcessKey(char userInput)
        {
            return m_KeyToCommand[userInput];
        }
    }
}