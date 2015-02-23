using CelticEgyptianRatscrewKata.Game;

namespace ConsoleBasedGame
{
    internal class PlayerInfo
    {
        public IPlayer Player { get; private set; }
        public char PlayCardKey { get; private set; }
        public char SnapKey { get; private set; }

        public PlayerInfo(string playerName, char playCardKey, char snapKey)
        {
            SnapKey = snapKey;
            Player = new Player(playerName);
            PlayCardKey = playCardKey;
        }
    }
}