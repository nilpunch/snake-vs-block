using System;
using Snake.Domain;
using UnityEngine;

namespace Snake.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuContext _mainMenuContext = null;
        [SerializeField] private SessionContext _sessionContext = null;
        [SerializeField] private ResultContext _resultContext = null;
        
        private StateMachine _stateMachine;
        
        private void Awake()
        {
            GameProgressService gameProgressService = new GameProgressService(new JsonSaveLoader<GameProgress>());

            _stateMachine = new StateMachine();
            
            _stateMachine.Register(new MainMenuState(_stateMachine, _mainMenuContext));
            _stateMachine.Register(new SessionState(_stateMachine, _sessionContext));
            _stateMachine.Register(new ResultState(_stateMachine, _resultContext));
            
            _stateMachine.Enter<MainMenuState>();
        }

        private void OnDestroy()
        {
            _stateMachine.Dispose();
        }
    }
}