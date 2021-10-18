using System.Collections.Generic;
using UnityEngine;

namespace TD.gameeconomics
{
	[CreateAssetMenu(fileName = "so_playerLevelProgress", menuName = "Scriptable Objects/PlayerProgress/LevelProgress")]
	public class SO_PlayerLevelProgress : ScriptableObject
	{
		[SerializeField] public List<PlayerLevelProgressData> playerProgressData;


	}
}
