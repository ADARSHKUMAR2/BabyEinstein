using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    [SerializeField]
    private Text second_Text;

    private Vector3 start_pos;
    private Vector3 end_pos;
    private float dis = 400f;
    private float lerp_time = 5;
    private float currentLerpTime;

    void Start()
    {
        start_pos = second_Text.transform.position;
        end_pos = second_Text.transform.position + Vector3.down * dis;
        GameManager.current.onPanel1_Active += OnPanel1_Active;
    }

    private void OnPanel1_Active()
    {
        second_Text.gameObject.SetActive(true);
        second_Text.text = "Tap on the flowers and leaves to watch them rustle";
        currentLerpTime = 0;
        StartCoroutine(AnimationText());

    }

    public IEnumerator AnimationText()
    {
        while(currentLerpTime<lerp_time)
        {
            float percentage = currentLerpTime / lerp_time;
            second_Text.transform.position = Vector3.Lerp(start_pos, end_pos, percentage);
            currentLerpTime += Time.deltaTime;
            yield return null;
        }
        second_Text.transform.position = end_pos;
        yield return null;
    }

    private void OnDestroy()
    {
        GameManager.current.onPanel1_Active -= OnPanel1_Active;
    }
}
