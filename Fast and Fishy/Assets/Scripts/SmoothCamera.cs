using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
	[SerializeField] private GameObject player;
	private Vector3 offset = new Vector3(0, 0, -6f);
	private float smoothTime = 0.2f;
	private Vector3 velocity = Vector3.zero;

	private void LateUpdate()
	{
		Vector3 targetPosition = player.transform.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	}
}
