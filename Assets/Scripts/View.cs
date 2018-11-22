/*NEVER BUG*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;
public class View:ViewMediator{
	private void Awake() {
		Register(this);
	}
	[Notification("hello")]
	public void Hello(){

	}
}
