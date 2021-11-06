using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

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

        #region Player Info Data

        
        public static void SetHighestStage(int val)
        {
            int hStage;
            if (SO_DataController.Singleton != null)
            {
                hStage = SO_DataController.Singleton.playerStat._highestStage + val;
                SO_DataController.Singleton.playerStat._highestStage = hStage;
            }
            
        }
        
        public static int GetHighestStage()
        {
            int hStage = 0;
            if (SO_DataController.Singleton != null)
            {
                hStage = SO_DataController.Singleton.playerStat._highestStage;
            }
            return hStage;
        }
        
        public static void SetMostWin(int val)
        {
            int mWin;
            if (SO_DataController.Singleton != null)
            {
                mWin = SO_DataController.Singleton.playerStat._mostWins + val;
                SO_DataController.Singleton.playerStat._mostWins = mWin;
            }
            
        }
        
        public static int GetMostWin()
        {
            int mWin = 0;
            if (SO_DataController.Singleton != null)
            {
                mWin = SO_DataController.Singleton.playerStat._mostWins;
            }
            return mWin;
        }
        
        public static void SetSoloVictories(int val)
        {
            int sVictories;
            if (SO_DataController.Singleton != null)
            {
                sVictories = SO_DataController.Singleton.playerStat._soloVictories + val;
                SO_DataController.Singleton.playerStat._soloVictories = sVictories;
            }
            
        }
        
        public static int GetSoloVictories()
        {
            int sVictories = 0;
            if (SO_DataController.Singleton != null)
            {
                sVictories = SO_DataController.Singleton.playerStat._soloVictories;
            }
            return sVictories;
        }
        
        public static void SetDuoVictories(int val)
        {
            int dVictories;
            if (SO_DataController.Singleton != null)
            {
                dVictories = SO_DataController.Singleton.playerStat._duoVictories + val;
                SO_DataController.Singleton.playerStat._duoVictories = dVictories;
            }
            
        }
        
        public static int GetDuoVictories()
        {
            int dVictories = 0;
            if (SO_DataController.Singleton != null)
            {
                dVictories = SO_DataController.Singleton.playerStat._duoVictories;
            }
            return dVictories;
        }
        
        public static void Set3V3Victories(int val)
        {
            int threeVictories;
            if (SO_DataController.Singleton != null)
            {
                threeVictories = SO_DataController.Singleton.playerStat._3V3Victories + val;
                SO_DataController.Singleton.playerStat._3V3Victories = threeVictories;
            }
            
        }
        
        public static int Get3V3Victories()
        {
            int threeVictories = 0;
            if (SO_DataController.Singleton != null)
            {
                threeVictories = SO_DataController.Singleton.playerStat._3V3Victories;
            }
            return threeVictories;
        }
        
        public static void SetBossVictories(int val)
        {
            int bVictories;
            if (SO_DataController.Singleton != null)
            {
                bVictories = SO_DataController.Singleton.playerStat._bossVictories + val;
                SO_DataController.Singleton.playerStat._bossVictories = bVictories;
            }
            
        }
        
        public static int GetBossVictories()
        {
            int bVictories = 0;
            if (SO_DataController.Singleton != null)
            {
                bVictories = SO_DataController.Singleton.playerStat._bossVictories;
            }
            return bVictories;
        }
        
        public static void SetTotalPlay(int val)
        {
            int tPlay;
            if (SO_DataController.Singleton != null)
            {
                tPlay = SO_DataController.Singleton.playerStat._totalPlay + val;
                SO_DataController.Singleton.playerStat._totalPlay = tPlay;
            }
            
        }
        
        public static int GetTotalPlay()
        {
            int tPlay = 0;
            if (SO_DataController.Singleton != null)
            {
                tPlay = SO_DataController.Singleton.playerStat._totalPlay;
            }
            return tPlay;
        }
        
        public static void SetDestroyDamage(int val)
        {
            int dDamage;
            if (SO_DataController.Singleton != null)
            {
                dDamage = SO_DataController.Singleton.playerStat._distroyDamage + val;
                SO_DataController.Singleton.playerStat._distroyDamage = dDamage;
            }
            
        }
        
        public static int GetDestroyDamage()
        {
            int dDamage = 0;
            if (SO_DataController.Singleton != null)
            {
                dDamage = SO_DataController.Singleton.playerStat._distroyDamage;
            }
            return dDamage;
        }
        
        
        
        
        
        
        #endregion
        
        
        
    
    }
}

