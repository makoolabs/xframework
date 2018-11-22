/*NEVER BUG*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XFramework{
	public class App : MonoBehaviour {
		public Version version;
		public Language language;
		private void Awake() {
			DontDestroyOnLoad(this);
		}
		private void OnApplicationPause(bool pauseStatus) {
			
		}
	}
}
