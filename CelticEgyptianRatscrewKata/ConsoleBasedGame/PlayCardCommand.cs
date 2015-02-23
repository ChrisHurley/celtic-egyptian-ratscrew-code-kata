using System;
using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    internal class PlayCardCommand : ICommand
    {
        private readonly IGameController m_Game;
        private readonly IPlayer m_Player;

        public PlayCardCommand(IGameController game, IPlayer player)
        {
            m_Game = game;
            m_Player = player;
        }

        public void Execute(ILogger log)
        {
            var card = m_Game.PlayCard(m_Player);

            if (card != null)
            {
                log.Write(string.Format("{0} has played the {1}", m_Player.Name, card));
            }

            var nextPlayer = m_Game.NextPlayer(m_Player);

            log.Write(String.Format("Next player is {0}", nextPlayer.Name));
        }
    }
}