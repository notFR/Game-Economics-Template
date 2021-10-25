using UnityEngine;

namespace TD.gameeconomics
{
    public static class PlayerStatistics 
    {
        public static void SetPlayerWinCount(int c)
        {
            int total = PlayerPrefs.GetInt("PlayerWin") + c;
            PlayerPrefs.SetInt("PlayerWin", total);
        }
        
        public static int GetPlayerWinCount()
        {
            int count = 0;
            count = PlayerPrefs.GetInt("PlayerWin");
            return count;
        }
        
        public static void ResetPlayerWinCount()
        {
            PlayerPrefs.DeleteKey("PlayerWin");
        }
    
    }
}

