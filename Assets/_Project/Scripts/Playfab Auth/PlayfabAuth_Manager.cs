using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace TD.gameeconomics
{
	public class PlayfabAuth_Manager : MonoBehaviour
	{
		#region Variables

		[SerializeField] private bool clearPlayerPrefs;

		[Header("Authentication Service :")] 
		[SerializeField] private Button loginEmailButton;
		[SerializeField] private Button errorOKButton;
		[SerializeField] private Button LoginButton;
		[SerializeField] private Button RegisterButton;
		[SerializeField] private Button CancelRegisterButton;

		[Header("Input :")] 
		[SerializeField] private TMP_InputField email;
		[SerializeField] private TMP_InputField password;
		[SerializeField] private TMP_InputField username;
		[SerializeField] private Toggle rememberMe;
		
		
		[Header("UI Screen :")] 
		[SerializeField]private List<UI_Screen> _UIMenus;
		
		[Header("What Data Need :")]
		[SerializeField] private GetPlayerCombinedInfoRequestParams InfoRequestParams;
		
		
		
		private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;

		private const string authPanelName = "Authentication";
		private const string errorPanel = "NoInternet";
		private const string logInPanel = "LogIn";
		private const string loadingPanel = "Loading";
		private const string registerPanel = "Register";
		private const string startGame = "StartGame";


		#endregion

		#region Function

		private void Awake()
		{
			MDisableAllScreens();
			
			//Reset
			if (clearPlayerPrefs)
			{
				_AuthService.UnlinkSilentAuth();
				_AuthService.ClearRememberMe();
				_AuthService.AuthType = Authtypes.None;
				clearPlayerPrefs = false;
			}
			
			// Remember me
			rememberMe.isOn = _AuthService.RememberMe;
			rememberMe.onValueChanged.AddListener((toggle) =>
			{
				_AuthService.RememberMe = toggle;
			});
		}

		private void Start()
		{

			// At start show auth panel
			if (!_AuthService.RememberMe)
			{
				ShowMenu(authPanelName);
			}
			
			ButtonPress();
			
			//
			PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
			PlayFabAuthService.OnPlayFabError += OnPlayFaberror;
			

			LoginButton.onClick.AddListener(OnLoginClicked);
			RegisterButton.onClick.AddListener(OnRegisterButtonClicked);
			 CancelRegisterButton.onClick.AddListener(OnCancelRegisterButtonClicked);


			_AuthService.InfoRequestParams = InfoRequestParams;
			//Start the authentication process.
			_AuthService.Authenticate();

		}


		void ButtonPress()
		{
			loginEmailButton.onClick.RemoveAllListeners();
			loginEmailButton.onClick.AddListener(() =>
			{
				CheckInternetConnection();
			});
			
			errorOKButton.onClick.RemoveAllListeners();
			errorOKButton.onClick.AddListener(() =>
			{
				ShowMenu(loadingPanel);
				this.Wait(1f, () =>
				{
					ShowMenu(authPanelName);
				});
			});
		}

		void CheckInternetConnection()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable) // No Internet
			{
				ShowMenu(loadingPanel);
				this.Wait(1f, () =>
				{
					ShowMenu(errorPanel);
				});
			}
			else // Internet has
			{
				ShowMenu(loadingPanel);
				this.Wait(1f, () =>
				{
					ShowMenu(logInPanel);
				});
			}
		}

		private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
		{
			if (SO_DataController.Singleton != null)
			{
				PlayerPrefs.SetInt(GlobalData.AuthType, 1); // playfab Auth
				SO_DataController.Singleton.SetAuthenticationType();
			}

			Debug.Log("Log In Success");
			ShowMenu(loadingPanel);
			this.Wait(0.5f, () => { ShowMenu(startGame); });
			var request = new GetAccountInfoRequest();
			PlayFabClientAPI.GetAccountInfo(request, result =>
				{
					
					//Debug.Log("username : " + result.AccountInfo.Username);
					PlayerPrefs.SetString(GlobalData.playerUsername, result.AccountInfo.Username);
					
				},
				error =>
				{
					Debug.LogError(error.GenerateErrorReport());
				});
			
			if (_AuthService.Username != null)
			{
				var req = new UpdateUserTitleDisplayNameRequest 
				{
					DisplayName = _AuthService.Username,
				}; 
				PlayFabClientAPI.UpdateUserTitleDisplayName(req, OnDisplayNameUpdated, error =>
				{
					Debug.LogError(error.GenerateErrorReport());
				});
			}

		}

		void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
		{
			
		}
		
		

		private void OnPlayFaberror(PlayFabError error)
		{
			//Error List
			switch (error.Error)
			{
				case PlayFabErrorCode.InvalidEmailAddress:
				case PlayFabErrorCode.InvalidPassword:
				case PlayFabErrorCode.InvalidEmailOrPassword:
					Debug.Log("Invalid Email or Password");
					break;

				case PlayFabErrorCode.AccountNotFound:
					ShowMenu(registerPanel);
					return;
				case PlayFabErrorCode.DuplicateUsername:
					Debug.Log("Duplicate UserName");
					break;
				default:
					Debug.Log(error.GenerateErrorReport());
					break;
                
			}
			
			
			Debug.Log(error.Error);
			Debug.LogError(error.GenerateErrorReport());
		}
		
		/// <summary>
		/// Play As a guest, which means they are going to silently authenticate
		/// by device ID or Custom ID
		/// </summary>
		private void OnPlayAsGuestClicked()
		{
			_AuthService.Authenticate(Authtypes.Silent);
		}
		
		private void OnLoginClicked()
		{
			_AuthService.Email = email.text;
			_AuthService.Password = password.text;
			_AuthService.Authenticate(Authtypes.EmailAndPassword);
		}

		private void OnRegisterButtonClicked()
		{
			_AuthService.Email = email.text;
			_AuthService.Username = username.text;
			_AuthService.Password = password.text;
			_AuthService.Authenticate(Authtypes.RegisterPlayFabAccount);
			
			
		}
		
		private void OnCancelRegisterButtonClicked()
		{
			//Reset all forms
			username.text = string.Empty;
			password.text = string.Empty;
			email.text = string.Empty;
			//Show panels
			ShowMenu(authPanelName);
		}

		#region UI MANAGEMENT

		public void ShowMenu(string name)
		{
			ShowMenuFunc(name, true);
		}
		
		public void ShowMenu(string name , bool isAllUIDisable)
		{
			ShowMenuFunc(name, isAllUIDisable);
		}
		
		private void ShowMenuFunc(string name, bool disableAllScreens){
			if(disableAllScreens) MDisableAllScreens();

			foreach (UI_Screen UI in _UIMenus){
				if (UI.UI_name == name) {
                
					if (UI.UI_Object != null) {
						UI.UI_Object.SetActive(true);
                    

					} else {
						Debug.Log ("no menu found with name: " + name);
					}
				}
			}
		}
    
		public void CloseMenu(string name){
			foreach (UI_Screen UI in _UIMenus){
				if (UI.UI_name == name)	UI.UI_Object.SetActive (false);
			}
		}
    
		public void MDisableAllScreens(){
			foreach (UI_Screen UI in _UIMenus){ 
				if(UI.UI_Object != null) 
					UI.UI_Object.SetActive(false);
				else 
					Debug.Log("Null ref found in UI with name: " + UI.UI_name);
			}
		}
		
		
		
		

		#endregion

		#endregion

	}
}

