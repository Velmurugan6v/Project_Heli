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

        private void Awake()
        {
            ChangeState(new WaitingState(_matchInput));
        }

        private void Update()
        {
            _currentState?.Tick();

            if (_currentState.IsCompleted)
                AdvanceState();
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