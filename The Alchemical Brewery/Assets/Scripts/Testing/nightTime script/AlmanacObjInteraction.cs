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
		panel_obj.SetActive(true);
	}
}
