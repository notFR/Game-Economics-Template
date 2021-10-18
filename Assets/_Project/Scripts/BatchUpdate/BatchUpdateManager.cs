using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FXnRXn
{
	public class BatchUpdateManager : MonoBehaviour
	{
		#region Variables

		public enum UpdateMode { BucketA, BucketB, Always }
		public static BatchUpdateManager Instance { get; private set; }
		private readonly HashSet<IBatchUpdate> _slicedUpdateBehavioursBucketA = new HashSet<IBatchUpdate>();
		private readonly HashSet<IBatchUpdate> _slicedUpdateBehavioursBucketB = new HashSet<IBatchUpdate>();
		private bool _isCurrentBucketA;

		#endregion


		#region Functions

		public void RegisterSlicedUpdate(IBatchUpdate slicedUpdateBehaviour, UpdateMode updateMode)
		{
			if (updateMode == UpdateMode.Always)
			{
				_slicedUpdateBehavioursBucketA.Add(slicedUpdateBehaviour);
				_slicedUpdateBehavioursBucketB.Add(slicedUpdateBehaviour);
			}
			else
			{
				var targetUpdateFunctions = updateMode == UpdateMode.BucketA ? _slicedUpdateBehavioursBucketA : _slicedUpdateBehavioursBucketB;
				targetUpdateFunctions.Add(slicedUpdateBehaviour);
			}
		}
		
		public void DeregisterSlicedUpdate(IBatchUpdate slicedUpdateBehaviour)
		{
			_slicedUpdateBehavioursBucketA.Remove(slicedUpdateBehaviour);
			_slicedUpdateBehavioursBucketB.Remove(slicedUpdateBehaviour);
		}
    
		void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		void Update()
		{
			var targetUpdateFunctions = _isCurrentBucketA ? _slicedUpdateBehavioursBucketA : _slicedUpdateBehavioursBucketB;
			foreach (var slicedUpdateBehaviour in targetUpdateFunctions)
			{
				slicedUpdateBehaviour.BatchUpdate();
			}
			_isCurrentBucketA = !_isCurrentBucketA;
		}

		#endregion
		

	}
}

