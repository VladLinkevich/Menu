using UnityEngine;

public class SettingUIHandler : MonoBehaviour
{
    public Animator animator;
    
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    public void OnClickOpenSettingPanel()
    {
        gameObject.SetActive(true);
        animator.SetTrigger(Open);
    }

    public void OnClickCloseSettingPanel()
    {
        animator.SetTrigger(Close);
    }

    public void DisableSettingPanel()
    {
        GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        gameObject.SetActive(false);
    }
}
