using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingTextManager : MonoBehaviour
{
    //public TextMeshProUGUI TextMeshProComponent;
    //public float ScrollSpeed = 10f;

    //private TextMeshProUGUI m_cloneTextObject;

    //private RectTransform m_textRectTransform;
    //private RectTransform thisRectTransform;
    //private string sourceText;
    //private string tempText;

    //private void Awake()
    //{
    //    m_textRectTransform = TextMeshProComponent.GetComponent<RectTransform>();
    //    thisRectTransform = this.GetComponent<RectTransform>();

    //    m_cloneTextObject = Instantiate(TextMeshProComponent) as TextMeshProUGUI;
    //    RectTransform cloneRectTransform = m_cloneTextObject.GetComponent<RectTransform>();
    //    cloneRectTransform.SetParent(thisRectTransform);
    //    cloneRectTransform.anchorMin = new Vector2(1, .5f);
    //    cloneRectTransform.localScale = new Vector3(1, 1, 1);
    //}

    //IEnumerator Start()
    //{
    //    float width = TextMeshProComponent.preferredWidth;
    //    Vector3 startPosition = m_textRectTransform.position;

    //    float scrollPosition = 0;

    //    while(true)
    //    {
    //        if(TextMeshProComponent.rectTransform.hasChanged)
    //        {
    //            width = TextMeshProComponent.preferredWidth;
    //            m_cloneTextObject.text = TextMeshProComponent.text;
    //        }

    //        m_textRectTransform.position = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);

    //        scrollPosition += ScrollSpeed * 20 * Time.deltaTime;

    //        yield return null;
    //    }
    //}

    public GameObject nameText;
    GameObject cloneText;

    bool scrollBool = true;

    private void Start()
    {
        StartCoroutine(ScrollText());
    }

    IEnumerator ScrollText()
    {
        cloneText = Instantiate(nameText, Vector3.zero, Quaternion.identity);
        cloneText.transform.SetParent(this.transform);

        float width = cloneText.GetComponent<TextMeshProUGUI>().preferredWidth;
        RectTransform _cloneTextTranform = cloneText.GetComponent<RectTransform>();
        RectTransform _TextTranform = nameText.GetComponent<RectTransform>();

        _cloneTextTranform.localScale = Vector3.one;
        _cloneTextTranform.anchorMin = new Vector2(1, .5f);

        _cloneTextTranform.localPosition = new Vector3(width, 0, 0);

        while(scrollBool)
        {
            _TextTranform.localPosition = new Vector3(_TextTranform.localPosition.x - (40 * Time.deltaTime), 0, 0 );
            _cloneTextTranform.localPosition = new Vector3(_cloneTextTranform.localPosition.x - (40 * Time.deltaTime), 0, 0);

            if(_TextTranform.localPosition.x < (-width - 80))
            {
                _TextTranform.localPosition = new Vector3(width, 0, 0);
            }

            if (_cloneTextTranform.localPosition.x < (-width - 80))
            {
                _cloneTextTranform.localPosition = new Vector3(width, 0, 0);
            }

            yield return null;
        }
    }
}
