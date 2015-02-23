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
            log.Write(string.Format("{0} has played the {1}", m_Player.Name, card.ToString()));
        }
    }
}