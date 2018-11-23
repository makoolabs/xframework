/*** 
 *     ____        ____   ____          ____    __________     ____      _____
 *    |....|      |....|  \...\        /.../  /..._____...\   |....|    /..../
 *    |....|      |....|   \...\      /.../  /.../     \___\  |....|   /..../
 *    |....|______|....|    \...\____/.../   |...|            |....|__/..../
 *    |....|......|....|     \....  ..../    \...\________    |....|....../
 *    |....|______|....|      \_..  .._/      \________...\   |....|__....|
 *    |....|      |....|        |....|       ___       \...\  |....|  \....\
 *    |....|      |....|        |....|      \...\      |...|  |....|   \....\
 *    |....|      |....|        |....|       \...\____/.../   |....|    \....\
 *    |____|      |____|        |____|        \ _________/    |____|     \ ___\
 *                               Never            Bug
 */
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
