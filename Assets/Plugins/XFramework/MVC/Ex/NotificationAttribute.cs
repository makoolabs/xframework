/*NEVER BUG*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XFramework{
	[AttributeUsage(AttributeTargets.Method)]
	public class NotificationAttribute:Attribute{
		public readonly string notificationName;
		public NotificationAttribute(string notificationName){
			this.notificationName = notificationName;
		}
	}
}
