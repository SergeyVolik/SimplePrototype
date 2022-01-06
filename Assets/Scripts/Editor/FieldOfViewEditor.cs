using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SerV112.UtilityAI.Game.Editor
{

	[CustomEditor(typeof(FieldOfViewSystem))]
	public class FieldOfViewEditor : UnityEditor.Editor
	{
        private void OnEnable()
        {
			FieldOfViewSystem fow = (FieldOfViewSystem)target;

			fow.OnTargetDetected.AddListener(SetTarget);
		}
		private void OnDisable()
		{
			FieldOfViewSystem fow = (FieldOfViewSystem)target;

			fow.OnTargetDetected.RemoveListener(SetTarget);
		}

		void SetTarget(Transform t)
		{
			targetEnemy = t;
		}
		Transform targetEnemy;
        void OnSceneGUI()
		{
			FieldOfViewSystem fow = (FieldOfViewSystem)target;
			Handles.color = Color.white;
			Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
			Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
			Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

			Handles.color = Color.red;
			if(targetEnemy)
				Handles.DrawLine(fow.transform.position, targetEnemy.position);
			}

		}

	
}