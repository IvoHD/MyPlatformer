using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
	public interface ISaveable
	{
		public Vector3 GetPositionToSave();
		public ObjectType GetObjectType();
		public void Register();
		public void Deregister();
	}
}
