using UnityEngine;

public class MoveToGoalAgent : MonoBehaviour
{
	private GameObject player;
	private Spawner spawner;
	private AudioManager audioManager;
	private Rigidbody2D rb;
	private float speed = 12f;
	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		spawner = FindObjectOfType<Spawner>();
		audioManager = FindObjectOfType<AudioManager>();
		rb = GetComponent<Rigidbody2D>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == player)
		{
			spawner.isGameActive = false;
		}
		else if (collision.gameObject.tag == "Magma")
		{
			Destroy(gameObject);
			audioManager.Play("obstacle touch");
		}
		else if (collision.gameObject.tag == "Rock")
		{
			audioManager.Play("obstacle touch");
		}
		else if (collision.gameObject.tag == "Border")
		{
			Destroy(gameObject);
			audioManager.Play("enemy outside");
		}
	}
	private void FixedUpdate()
	{
		if (spawner.isGameActive)
		{
			transform.right = player.transform.position - transform.position;
			rb.AddForce(transform.right * speed, ForceMode2D.Force);
		}
		else
		{
			rb.velocity = new Vector2(0, 0);
		}
	}
}