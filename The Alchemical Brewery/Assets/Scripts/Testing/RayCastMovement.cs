using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastMovement : MonoBehaviour
{
	public static RayCastMovement Instance { get; private set; }

	private RaycastHit vision;
	public float rayLength;
	public float smooth = 1f;
	private Rigidbody grabbedObject;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;
	private Quaternion targetRotation;

	void Awake()
    {
		Instance = this;
    }

	void Start()
	{
		rayLength = 4.0f;
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		navMeshAgent.angularSpeed = 0;
		targetRotation = transform.rotation;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown("Fire1"))
		{
			if(!EventSystem.current.IsPointerOverGameObject())
            {
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					if (hit.collider.CompareTag("Ground"))
					{
						//navMeshAgent.destination = hit.point;
						//navMeshAgent.Resume();
						navMeshAgent.SetDestination(hit.point);
					}
				}
			}
			
		}

		//if (Input.GetKeyDown(KeyCode.Q))
		//{
		//	targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
		//}
		//else if (Input.GetKeyDown(KeyCode.E))
		//{
		//	targetRotation *= Quaternion.AngleAxis(-90, Vector3.up);
		//}

		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
	}

	public void NewDestination(Vector3 newDest)
    {
		navMeshAgent.SetDestination(newDest);
	}
}
