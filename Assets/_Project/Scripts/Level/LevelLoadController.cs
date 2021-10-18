using System;
using System.Collections;
using System.Collections.Generic;
using FXnRXn;
using MoreMountains.Tools;
using UnityEngine;

namespace TD.gameeconomics
{
	public class LevelLoadController : MonoBehaviour
	{
		#region Variables

		public static LevelLoadController Singleton;

		#endregion


		#region Functions

		private void Awake()
		{
			if (Singleton == null)
			{
				Singleton = this;
			}
		}

		public void LoadTheLevel(string scenename)
		{
			LoadingSceneManager.LoadScene(scenename);
		}
		

		#endregion




	}
}


