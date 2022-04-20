using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private GameObject joystick;
	[SerializeField] private Sprite fish;
	[SerializeField] private Sprite skeleton;
	private GameManager gameManager;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb;
	private Joystick joystickScript;
	private float rotationSpeed = 720f;
	private float speed = 3f;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		joystickScript = joystick.GetComponent<Joystick>();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	private void ChangeSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	public void MoveObject(GameObject gameobject, float speed)
	{
		Vector2 movementDirection = joystickScript.input;
		float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
		movementDirection.Normalize();

		void MoveLogic(float speed)
		{
			gameobject.transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
		}
		MoveLogic(speed);

		if (inputMagnitude != 0)
		{
			Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, movementDirection);
			gameobject.transform.rotation = Quaternion.RotateTowards(gameobject.transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
		}
	}

	private void Start()
	{
		ChangeSprite(fish);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Magma" || collision.gameObject.tag == "Enemy")
		{
			ChangeSprite(skeleton);
			rb.velocity = Vector3.zero;
			rb.angularVelocity = 0f;
			joystickScript.input = Vector2.zero;
			FindObjectOfType<GameManager>().EndGame();
		}
		else if(collision.gameObject.tag == "Food")
		{
			gameManager.AddScore();
			Destroy(collision.gameObject);
		}
	}

	private void Update()
	{
		MoveObject(gameObject, speed);
	}
}
