/*NEVER BUG*/
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
namespace XFramework{
	public class ViewMediator : MonoBehaviour,IMediator{
		private Notifier notifier;
		private Dictionary<string,MethodInfo> methodLib = new Dictionary<string, MethodInfo>();
		private List<string> notificationList = new List<string>();
		public string MediatorName{
			get{
				return this.name;
			}
		}
		public object ViewComponent{get;set;}
		public string[] ListNotificationInterests(){
			return notificationList.ToArray();
		}
		public void HandleNotification(INotification notification){
			var notiName = notification.Name;
			if(methodLib.ContainsKey(notiName)){
				methodLib[notiName].Invoke(this,null);
			}
		}
		public void OnRegister(){
			notifier = new Notifier();
		}
		public void OnRemove(){
			
		}
		protected void Register(MonoBehaviour view){
			foreach (var mInfo in view.GetType().GetMethods()){
				foreach (var attr in System.Attribute.GetCustomAttributes(mInfo)){
					if(attr.GetType() == typeof(NotificationAttribute)){
						var notiName = ((NotificationAttribute)attr).notificationName;
						if(!methodLib.ContainsKey(notiName)){
							methodLib.Add(notiName,mInfo);
							notificationList.Add(notiName);
						}
					}
				}
			}
			Facade.RegisterMediator(this);
		}
		public void SendNotification(string notificationName, object body = null, string type = null){
			notifier.SendNotification(notificationName,body,type);
		}
		protected IFacade Facade{
            get {
                return PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
            }
        }
	}
}

