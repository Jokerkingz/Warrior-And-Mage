using UnityEngine;
using System.Collections;

public class SpinZ : MonoBehaviour
{
	


	void Update ()
	{
		transform.Rotate (new Vector3 (Time.deltaTime * 0,0, 2));
	}
}