﻿namespace com
{
    public class CheatCode
    {
        public static int cheatCode = 401;
        private int _currentCheatCode;
        private static CheatCode _instance;

        public static CheatCode GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CheatCode();
                _instance._currentCheatCode = 0;
            }

            return _instance;
        }

        public static void Add(int v)
        {
            var inst = GetInstance();
            inst._currentCheatCode += v;
        }

        public static void Clear()
        {
            var inst = GetInstance();
            inst._currentCheatCode = 0;
        }

        public static bool IsEnabled()
        {
            var inst = GetInstance();
            return inst._currentCheatCode == cheatCode;
        }
    }
}