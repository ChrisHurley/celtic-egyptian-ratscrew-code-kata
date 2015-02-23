﻿using System.Collections.Generic;
using System.Linq;
using CelticEgyptianRatscrewKata.GameSetup;
using CelticEgyptianRatscrewKata.SnapRules;

namespace CelticEgyptianRatscrewKata.Game
{
    public interface IGameController
    {
        bool AddPlayer(IPlayer player);
        Card PlayCard(IPlayer player);
        void AttemptSnap(IPlayer player);

        /// <summary>
        /// Starts a game with the currently added players
        /// </summary>
        void StartGame(Cards deck);

        bool TryGetWinner(out IPlayer winner);
        IPlayer NextPlayer(IPlayer player);
    }

    /// <summary>
    /// Controls a game of Celtic Egyptian Ratscrew.
    /// </summary>
    public class GameController : IGameController
    {
        private readonly ISnapValidator m_SnapValidator;
        private readonly IDealer m_Dealer;
        private readonly IShuffler m_Shuffler;
        private readonly IList<IPlayer> m_Players;
        private readonly IGameState m_GameState;

        public GameController(IGameState gameState, ISnapValidator snapValidator, IDealer dealer, IShuffler shuffler)
        {
            m_Players = new List<IPlayer>();
            m_GameState = gameState;
            m_SnapValidator = snapValidator;
            m_Dealer = dealer;
            m_Shuffler = shuffler;
        }

        public bool AddPlayer(IPlayer player)
        {
            if (m_Players.Any(x => x.Name == player.Name)) return false;

            m_Players.Add(player);
            m_GameState.AddPlayer(player.Name, Cards.Empty());
            return true;
        }

        public Card PlayCard(IPlayer player)
        {
            if (m_GameState.HasCards(player.Name))
            {
                return m_GameState.PlayCard(player.Name);
            }

            return null;
        }

        public IPlayer NextPlayer(IPlayer player)
        {
            var currentPlayer = m_Players.IndexOf(player);

            var nextPlayerId = (currentPlayer + 1)%m_Players.Count();

            return m_Players[nextPlayerId];
        }

        public void AttemptSnap(IPlayer player)
        {
            AddPlayer(player);

            if (m_SnapValidator.CanSnap(m_GameState.Stack))
            {
                m_GameState.WinStack(player.Name);
            }
        }

        /// <summary>
        /// Starts a game with the currently added players
        /// </summary>
        public void StartGame(Cards deck)
        {
            m_GameState.Clear();

            var shuffledDeck = m_Shuffler.Shuffle(deck);
            var decks = m_Dealer.Deal(m_Players.Count, shuffledDeck);
            for (var i = 0; i < decks.Count; i++)
            {
                m_GameState.AddPlayer(m_Players[i].Name, decks[i]);
            }
        }

        public bool TryGetWinner(out IPlayer winner)
        {
            var playersWithCards = m_Players.Where(p => m_GameState.HasCards(p.Name)).ToList();

            if (!m_GameState.Stack.Any() && playersWithCards.Count() == 1)
            {
                winner = playersWithCards.Single();
                return true;
            }

            winner = null;
            return false;
        }
    }
}
