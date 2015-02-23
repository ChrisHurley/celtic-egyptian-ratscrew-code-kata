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

        public void Execute()
        {
            m_Game.PlayCard(m_Player);
        }
    }
}