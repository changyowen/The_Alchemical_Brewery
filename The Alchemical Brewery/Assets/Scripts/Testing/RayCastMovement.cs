using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RayCastMovementMode
{
	Normal,
	Teleport
}

public class RayCastMovement : MonoBehaviour
{
	public static RayCastMovement Instance { get; private set; }

	[Header("Raycast Mode")]
	public RayCastMovementMode rayCastMode = RayCastMovementMode.Normal;

	[Header("Reference")]
	public GameObject teleportEffect_obj;

	[Header("Internal Data")]
	private RaycastHit vision;
	public float rayLength;
	public float smooth = 1f;
	private Rigidbody grabbedObject;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;
	private Quaternion targetRotation;
	[System.NonSerialized] public bool isTeleporting = false;

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
		int layer_mask = LayerMask.GetMask("TeleportUse");
		int layer_mask01 = LayerMask.GetMask("TeleportUse", "Ignore Raycast");

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown("Fire1"))
		{
			////debugUse
			//if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			//{
			//	if (!EventSystem.current.IsPointerOverGameObject())
			//		Debug.Log(hit.transform.gameObject.name);
			//}
			if (!EventSystem.current.IsPointerOverGameObject())
            {
				if(rayCastMode == RayCastMovementMode.Normal)
				{
					if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layer_mask01))
					{
						if (hit.collider.CompareTag("Ground"))
						{
							navMeshAgent.SetDestination(hit.point);
						}
					}
				}
				else if(rayCastMode == RayCastMovementMode.Teleport)
				{
					if(!isTeleporting)
					{
						if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
						{
							if (hit.collider.CompareTag("TeleportUse"))
							{
								Vector3 teleportLocation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
								StartCoroutine(TeleportFunc(teleportLocation));
							}
						}
					}
				}
			}
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
	}

	public void NewDestination(Vector3 newDest)
    {
		navMeshAgent.SetDestination(newDest);
	}

	private IEnumerator TeleportFunc(Vector3 teleportDest)
	{
		isTeleporting = true;
		ElementMeterPanel.Instance.elementSkillRemaining[4] -= 1; //subtract skill remaining
		navMeshAgent.SetDestination(transform.position); //reset position
		teleportEffect_obj.SetActive(true); //activate teleport effect

		//delay a bit for teleport effect
		yield return new WaitForSeconds(.7f);

		//teleport toward destination
		transform.position = teleportDest;
		navMeshAgent.Warp(teleportDest);
		navMeshAgent.SetDestination(teleportDest);

		//delay a bit for ending teleport effect
		yield return new WaitForSeconds(.3f);

		teleportEffect_obj.SetActive(false); //disable teleport effect
		isTeleporting = false;
		rayCastMode = RayCastMovementMode.Normal;

		///CHECK IF ORDO SKILL REMAINING FINISHED
		if(ElementMeterPanel.Instance.elementSkillRemaining[4] == 0)
		{
			ElementSkillManager.Instance.skillActivated[4] = false; //deactivate skill
			ElementMeterPanel.Instance.elementMana[4] = 0; //reset element mana
		}
	}
}
