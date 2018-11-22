/*NEVER BUG*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AttributeUsage(AttributeTargets.Method)]
public class NotificationAttribute:Attribute{
	private string notificationName;
	public NotificationAttribute(string notificationName){
		this.notificationName = notificationName;
	}
}
