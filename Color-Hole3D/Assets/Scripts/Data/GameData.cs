using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public static class GameData
    {
        private static string buildIndex = "buildIndex";


        private static string Level = "level";
        public static UnityAction OnLevelUpdate;

        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt(Level, 0);

            set
            {
                PlayerPrefs.SetInt(Level, value);
                OnLevelUpdate?.Invoke();
            }
        }

        private static int DefaultCurrencyAmount = 100;
        private static string CurrencyKey = "currency";
        public static UnityAction OnCurrencyUpdate;

        public static int Currency
        {
            get => PlayerPrefs.GetInt(CurrencyKey, DefaultCurrencyAmount);
            set
            {
                PlayerPrefs.SetInt(CurrencyKey, value);
                OnCurrencyUpdate?.Invoke();
            }
        }

        private static int DefaultKeyAmount = -1;
        private static string Keycurrency = "keyCurrency";
        public static UnityAction OnKeyUpdate;

        public static int Key
        {
            get => PlayerPrefs.GetInt(Keycurrency, DefaultKeyAmount);
            set
            {
                PlayerPrefs.SetInt(Keycurrency, value);
                OnKeyUpdate?.Invoke();
            }
        }


        public static bool Vibration
        {
            get => PlayerPrefs.GetInt("vibration", 1) == 1;
            set => PlayerPrefs.SetInt("vibration", value ? 1 : 0);
        }
    }
}