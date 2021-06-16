using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsSelectedUIHandler : MonoBehaviour
{
    public Animator animator;
    
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private Image _settingChipImage;

    public void OnClickOpenChipPanel(Image image)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(Open);

        _settingChipImage = image;
        ChipsHandler.Instance.OnEnableSetSettingShip(_settingChipImage);
    }

    public void OnClickCloseChipPanel()
    {
        animator.SetTrigger(Close);

        _settingChipImage.sprite =
            ChipsHandler.Instance.SelectedChip.chipImage.sprite;
    }

    public void DisableSettingPanel()
    {
        GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        gameObject.SetActive(false);
    }
}
