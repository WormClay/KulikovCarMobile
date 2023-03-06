using UnityEngine;

namespace Features.Fight
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 2f;
        private const float KPower = 1.5f;
        private const float MaxHealthPlayer = 20;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;

        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = (_moneyPlayer * KMoney) - _healthPlayer;
            float powerRatio = _powerPlayer - (_moneyPlayer * KPower);
            float result = (moneyRatio + kHealth + powerRatio);
            return result < 0 ? 0 : (int)result;
        }

        private int CalcKHealth() =>
            _healthPlayer > MaxHealthPlayer ? 10 : 3;
    }
}

