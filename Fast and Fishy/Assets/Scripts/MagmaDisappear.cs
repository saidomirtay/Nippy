using System.Collections;
using UnityEngine;

public class MagmaDisappear : MonoBehaviour
{
	private ParticleSystem bonkEffect;
	public float timeToDisappear;

	private void Awake()
	{
		bonkEffect = GameObject.FindGameObjectWithTag("Bonk").GetComponent<ParticleSystem>();
	}

	private IEnumerator DestroyCoroutine()
	{
		yield return new WaitForSeconds(timeToDisappear);
		Destroy(gameObject);
	}

	void Start()
	{
		StartCoroutine("DestroyCoroutine");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bonkEffect.transform.position = transform.position;
		bonkEffect.Play();
	}
}
