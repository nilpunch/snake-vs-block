using System;
using Snake.Boot;

namespace Snake.Domain
{
    public class GameProgressService
    {
        private readonly ISaveLoader<GameProgress> _saveLoader;
        private readonly GameProgress _gameProgress;

        public GameProgressService(ISaveLoader<GameProgress> saveLoader)
        {
            _saveLoader = saveLoader;
            _gameProgress = _saveLoader.Load();
        }

        public int MaxScore => _gameProgress.MaxScore;

        public void SetHigherMaxScore(int maxScore)
        {
            if (maxScore <= MaxScore)
                throw new InvalidOperationException();

            _gameProgress.MaxScore = maxScore;
        }

        public void Save()
        {
            _saveLoader.Save(_gameProgress);
        }
    }
}