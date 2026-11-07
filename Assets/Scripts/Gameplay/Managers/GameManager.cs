using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Input;
using HelicopterTag.Gameplay.States;
using UnityEngine;

namespace HelicopterTag.Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        private IGameState _currentState;
        public IGameState CurrentState => _currentState;
        [SerializeField] private MatchInput _matchInput;
        [SerializeField] private GameplayConfig _gameplayConfig;

        bool m_phaseRequestSent;
        
        private void Awake()
        {
            ChangeState(new WaitingState(_matchInput));
        }

        private void Update()
        {
            _currentState?.Tick();

            if (_currentState == null || !_currentState.IsCompleted || m_phaseRequestSent)
                return;

            IMatchPhaseAuthority authority = LocalPlayerRegistry.MatchPhaseAuthority;

            if (authority == null)
                return;

            m_phaseRequestSent = true;
            authority.RequestAdvance();
        }
        
        public void ApplyNetworkPhase(MatchPhase phase)
        {
           m_phaseRequestSent = false;

            switch (phase)
            {
                case MatchPhase.Waiting:

                    if (_currentState is not WaitingState)
                        ChangeState(new WaitingState(_matchInput));

                    break;

                case MatchPhase.Countdown:

                    if (_currentState is not CountdownState)
                        ChangeState(new CountdownState(_gameplayConfig));

                    break;

                case MatchPhase.Playing:

                    if (_currentState is not PlayingState)
                        ChangeState(new PlayingState(_gameplayConfig));

                    break;

                case MatchPhase.Finished:

                    if (_currentState is not FinishedState)
                        ChangeState(new FinishedState());

                    break;
            }
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private void AdvanceState()
        {
            if (_currentState is WaitingState)
            {
                ChangeState(new CountdownState(_gameplayConfig));
                return;
            }

            if (_currentState is CountdownState)
            {
                ChangeState(new PlayingState(_gameplayConfig));
                return;
            }

            if (_currentState is PlayingState)
            {
                ChangeState(new FinishedState());
                return;
            }
        }
    }
}