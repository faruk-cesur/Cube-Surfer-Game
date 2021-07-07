using UnityEngine;
using Random = UnityEngine.Random;

public class RunnerController : MonoBehaviour
{
	[SerializeField] private float _firstLine;
	[SerializeField] private float _secondLine;
	[SerializeField] private float _thirdLine;

	[SerializeField] private float _moveThreshold;
	[SerializeField] private float _speed;
	[SerializeField] private float _moveSpeed;

	private float _lastMoveTime;
	private Rigidbody _rigidbody;
	private int _point;
	private bool _hitted;
	private Vector3 moveTo;

	enum Lane
	{
		First,
		Second,
		Third
	}

	private Lane _lane = Lane.Second;

	

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			float movePow = touch.deltaPosition.normalized.x;
			if (Mathf.Abs(movePow) > _moveThreshold && Time.time - _lastMoveTime > 0.5f)
			{
				_lastMoveTime = Time.time;
				if (movePow < 0)
				{
					switch (_lane)
					{
						case Lane.First:
							break;
						case Lane.Second:
							//transform.position += new Vector3(_firstLine, 0, 0);
							moveTo = new Vector3(_firstLine, 0, transform.position.z);
							_lane = Lane.First;
							break;
						case Lane.Third:
							moveTo = new Vector3(_secondLine, 0, transform.position.z);
							_lane = Lane.Second;
							break;
					}
				}

				if (movePow > 0)
				{
					switch (_lane)
					{
						case Lane.First:
							moveTo = new Vector3(_secondLine, 0, transform.position.z);
							_lane = Lane.Second;
							break;
						case Lane.Second:
							moveTo = new Vector3(_thirdLine, 0, transform.position.z);
							_lane = Lane.Third;
							break;
						case Lane.Third:
							break;
					}
				}
			}
		}

		Move(moveTo);
	}

	private void FixedUpdate()
	{
		if(!_hitted)
			_rigidbody.velocity = transform.forward * (Time.deltaTime * _moveSpeed);
	}


	private void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
		_point++;
		print(_point);
	}

	private void OnCollisionEnter(Collision other)
	{
		_hitted = true;
		_point--;
		print(_point);
		
		
		switch (_lane)
		{
			case Lane.First:
				moveTo = new Vector3(_secondLine, 0, 0);
				_lane = Lane.Second;
				break;
			case Lane.Second:
				//transform.position += new Vector3(_firstLine, 0, 0);
				if (Random.Range(0f, 1f) > 0.5f)
				{
					moveTo = new Vector3(_firstLine, 0, 0);
					_lane = Lane.First;
				}
				else
				{
					moveTo = new Vector3(_thirdLine, 0, 0);
					_lane = Lane.Third;
				}
				break;
			case Lane.Third:
				moveTo = new Vector3(_secondLine, 0, 0);
				_lane = Lane.Second;
				break;
		}
	}


	private void Move(Vector3 moveTo)
	{
		moveTo = new Vector3(moveTo.x,0,transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * _speed);
		_hitted = false;
	}
	
	
}