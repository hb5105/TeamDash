using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitVideoSize: MonoBehaviour
{
    public RectTransform rawImageRectTransform;
    public float maxWidth = 600f;

    private void Update()
    {
        float newWidth = Mathf.Clamp(rawImageRectTransform.sizeDelta.x, 0f, maxWidth);
        float aspectRatio = rawImageRectTransform.sizeDelta.y / rawImageRectTransform.sizeDelta.x;
        float newHeight = newWidth * aspectRatio;
        rawImageRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
