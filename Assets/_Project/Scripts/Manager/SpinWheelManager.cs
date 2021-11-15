using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TD.gameeconomics
{
    public class SpinWheelManager : MonoBehaviour
    {
	#region Variable

	[Header("UI Refference :")] 
	[SerializeField] private TMP_Text spinCountTo;

	[Space(15)] 
	[SerializeField] private Image[] spinItemIcon; 
	[SerializeField] private TMP_Text[] spinItemCount;

	[Header("UI Button :")] 
	[SerializeField] private Button spinButton;
	[SerializeField] private Button dailySpinButtonAd;
	
	
	[Header ("Picker wheel settings :")]
	[SerializeField] private Transform wheelCircle ;
	
	[Space]
	[Range (1, 20)] public int spinDuration = 8 ;
	[SerializeField] [Range (.2f, 2f)] private float wheelSize = 1f ;
	[SerializeField] private int pieceCount;
	
	// Events
	private UnityAction onSpinStartEvent ;
	private UnityAction<int> onSpinEndEvent;


	private bool _isSpinning = false ;

	public bool IsSpinning { get { return _isSpinning ; } }
	
	
	
	
	
	
	private float pieceAngle ;
	private float halfPieceAngle ;
	private float halfPieceAngleWithPaddings ;
	

	private SO_SpinWheelData _spinWheel;
	
	#endregion

	#region Function

	private void Awake()
	{
		if (!PlayerPrefs.HasKey(GlobalData.spinWheel))
		{
			PlayerPrefs.SetInt(GlobalData.spinWheel, 0);
		}
	}
	private void Start()
	{
		if (_spinWheel == null)
		{
			_spinWheel = SO_DataController.Singleton.spinWheel;
		}

		pieceAngle = 360 / pieceCount;
		halfPieceAngle = pieceAngle / 2f;
		halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f);
		

		UpgradeUI();
		ButtonPress();
		
	}


	void UpgradeUI()
	{
		int index = PlayerPrefs.GetInt(GlobalData.spinWheel, 0);

		for (int i = 0; i < spinItemIcon.Length; i++)
		{
			spinItemIcon[i].sprite = _spinWheel.spinWheelDatas[index].spinWheelData[i].Icon;
			spinItemCount[i].text = _spinWheel.spinWheelDatas[index].spinWheelData[i].count.ToString();
		}

		spinCountTo.text = _spinWheel.dailySpinCountTo.ToString() + "/" + _spinWheel.dailySpinCountToMax.ToString();

	}
	void ButtonPress()
	{
		spinButton.onClick.RemoveAllListeners();
		spinButton.onClick.AddListener(() =>
		{
			int spin = _spinWheel.dailySpinCountTo;
			int spinMax = _spinWheel.dailySpinCountToMax;

			if (spin < spinMax)
			{
				spin += 1;
				_spinWheel.dailySpinCountTo = spin;
				UpgradeUI();

				Spin();
			}
			
		});
		
		dailySpinButtonAd.onClick.RemoveAllListeners();
		dailySpinButtonAd.onClick.AddListener(() =>
		{
			//Show Ad
			OnAdClose();

		});
	}
	
	
	
	
	// -------------------------         SPIN             ------------------------
	void Spin()
	{
		if (!_isSpinning)
		{
			_isSpinning = true;
			SpinStart();
			if (onSpinStartEvent != null)
			{
				onSpinStartEvent.Invoke();
			}

			int index = GetRandomIndex();

			float angle = -(pieceAngle * index);
			
			
			
			float rightOffset = (angle - halfPieceAngleWithPaddings) % 360 ;
			float leftOffset = (angle + halfPieceAngleWithPaddings) % 360 ;

			float randomAngle = Random.Range (leftOffset, rightOffset) ;

			Vector3 targetRotation = Vector3.back * (randomAngle + 2 * 360 * spinDuration) ;
			
			float prevAngle, currentAngle ;
			prevAngle = currentAngle = wheelCircle.eulerAngles.z ;

			bool isIndicatorOnTheLine = false ;

			wheelCircle.DORotate(targetRotation, spinDuration, RotateMode.Fast).SetEase(Ease.InOutQuart).OnUpdate(() =>
			{
				float diff = Mathf.Abs (prevAngle - currentAngle) ;
				if (diff >= halfPieceAngle) {
					if (isIndicatorOnTheLine) {
						// audioSource.PlayOneShot (audioSource.clip) ;
					}
					prevAngle = currentAngle ;
					isIndicatorOnTheLine = !isIndicatorOnTheLine ;
				}
				currentAngle = wheelCircle.eulerAngles.z ;
			}).OnComplete(() =>
			{
				_isSpinning = false ;
				if (onSpinEndEvent != null)
					onSpinEndEvent.Invoke (index) ;

				SpinEnd(index);
				//onSpinStartEvent = null ; 
				//onSpinEndEvent = null ;
			});



		}
	}
	private int GetRandomIndex()
	{
		return Random.Range(0, pieceCount);
	}
	
	
	
	
	//-------------    EVENT CALL   -------------------
	public void SpinStart()
	{
		spinButton.interactable = false;
	}
	public void SpinEnd(int index)
	{
		spinButton.interactable = true;

		
		// 
		int indexData = PlayerPrefs.GetInt(GlobalData.spinWheel, 0);
		LuckySpinType spinType = _spinWheel.spinWheelDatas[indexData].spinWheelData[index]._spinType;

		switch (spinType)
		{
			case LuckySpinType.Coin:
				GameCoinGemEnergyCount.CoinAddCount
					(_spinWheel.spinWheelDatas[indexData].spinWheelData[index].count);
				break;
			case LuckySpinType.Energy:
				GameCoinGemEnergyCount.EnergyAddCount
					(_spinWheel.spinWheelDatas[indexData].spinWheelData[index].count);
				break;
			case LuckySpinType.Gem:
				GameCoinGemEnergyCount.GemAddCount
					(_spinWheel.spinWheelDatas[indexData].spinWheelData[index].count);
				break;
			case LuckySpinType.NormalChest:
				if (SO_DataController.Singleton != null)
				{
					SO_DataController.Singleton.AddItemInventory(_spinWheel.spinWheelDatas[indexData].spinWheelData[index].chest);
				}
				break;
			case LuckySpinType.Rune:
				if (SO_DataController.Singleton != null)
				{
					SO_DataController.Singleton.AddItemInventory(_spinWheel.spinWheelDatas[indexData].spinWheelData[index].chest);
				}
				break;
		}

	}
	
	// -------------------------------------------------

	public void OnAdClose()
	{
		int spinMax = _spinWheel.dailySpinCountToMax;
		spinMax += 1;
		_spinWheel.dailySpinCountToMax = spinMax;
		UpgradeUI();
	}
	
	#endregion
        
    }
}


