using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_playerStat", menuName = "Scriptable Objects/Player/User Stat")]
    public class SO_PlayerStatInfo : ScriptableObject
    {

	    [Header("User Info :")] 
	    public int _highestScore;
	    public int _highestStage;
	    public int _mostWins;
	    public int _soloVictories;
	    public int _duoVictories;
	    public int _3V3Victories;
	    public int _bossVictories;
	    public int _totalPlay;
	    public int _distroyDamage;
    }
}


