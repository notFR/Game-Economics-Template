using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.gameeconomics
{
	public class UI_Item : MonoBehaviour
	{
		#region Variables

		public RewardType _rewardType;
		
		[EnumToggleButtons]
		public RewardType RewardEnum;
		
		[ShowIf("RewardEnum", RewardType.XP)]
		public TMP_Text _xpText;
		
		[ShowIf("RewardEnum", RewardType.Coin)]
		public TMP_Text _coinText;
		
		[ShowIf("RewardEnum", RewardType.Chest)]
		public Image _chestIcon;
		[ShowIf("RewardEnum", RewardType.Chest)]
		public TMP_Text _chestCountText;

		#endregion

		#region Functions

		#endregion

	}
}
