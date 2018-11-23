/*NEVER BUG*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;
public class View:ViewMediator{
	private void Awake() {
		Register(this);
		SendNotification("hello",12);
	}
	[Notification("hello")]
	public void Hello(){
		Debug.LogWarning("ooooo::ooooo");
	}
}
