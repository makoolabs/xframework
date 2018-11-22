/*NEVER BUG*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
namespace XFramework{
	public class ViewMediator : MonoBehaviour,IMediator{
		private Notifier notifier;
		public string MediatorName{
			get;
		}
		public object ViewComponent{get;set;}
		public string[] ListNotificationInterests(){
			return new string[10];
		}
		public void HandleNotification(INotification notification){
			//switch()
		}
		public void OnRegister(){
			notifier = new Notifier();
		}
		public void OnRemove(){
			
		}
		protected void Register(MonoBehaviour view){
			Facade.RegisterMediator(this);
			var attributes = view.GetType().GetCustomAttributes(true);

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

