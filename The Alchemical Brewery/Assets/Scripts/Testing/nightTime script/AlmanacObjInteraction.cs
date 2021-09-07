using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlmanacObjInteraction : MonoBehaviour
{
	public GameObject textBox_obj;
	public GameObject panel_obj;
    public bool textBoxAppear = false;

	void Update()
	{
		textBoxAppear = false;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if(Input.GetButtonDown("Fire1"))
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					AlmanacObjInteraction almanacObjInteraction = hit.transform.GetComponent<AlmanacObjInteraction>();
					if (almanacObjInteraction != null)
					{
						almanacObjInteraction.OpenPanel();
					}
				}
			}
			else
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					AlmanacObjInteraction almanacObjInteraction = hit.transform.GetComponent<AlmanacObjInteraction>();
					if (almanacObjInteraction != null)
					{
						almanacObjInteraction.textBoxAppear = true;
					}
				}
			}
		}

		if (textBoxAppear)
		{
			textBox_obj.SetActive(true);
		}
		else
		{
			textBox_obj.SetActive(false);
		}
	}

	public void OpenPanel()
	{
		if(this.gameObject.tag == "almanac") //for almanac
		{
			//only open almanac if created at least one potion
			if(PlayerProfile.acquiredPotion.Count != 0)
			{
				panel_obj.SetActive(true);
			}
			else
			{
				//warn player to create first potion
				string _warnText = "Please create your first potion first!";
				NotificationSystem.Instance.SendPopOutNotification(_warnText);
			}
		}
		else if(this.gameObject.tag == "map") //for map
		{
			//only open map if player able to travel
			if(PlayerProfile.dayResetTravel == 0)
			{
				panel_obj.SetActive(true);
			}
			else
			{
				//warn player to wait till reset travel day
				string _warnText = PlayerProfile.dayResetTravel + " more days to travel again!";
				NotificationSystem.Instance.SendPopOutNotification(_warnText);
			}
		}
	}
}
