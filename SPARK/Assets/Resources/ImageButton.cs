using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageButton : Button, ICanvasRaycastFilter
{
    private Canvas canvas;
    private int? canvasWidth;
    private int? canvasHeight;

    private Image image;
    private AlphaMap alphaMap;

    void Start()
    {
        image = GetComponent<Image>();
        CanvasScaler scaler;
        Transform p = transform;
        do
        {
            p = p.parent;
            scaler = p.GetComponent<CanvasScaler>();
        } while (scaler == null);

        canvas = scaler.GetComponent<Canvas>();
        canvasWidth = (int)scaler.referenceResolution.x;
        canvasHeight = (int)scaler.referenceResolution.y;

        alphaMap = AlphaMap.Load(image.sprite);
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera c)
    {
        var point = new Vector2(
                        sp.x / Screen.width * canvasWidth.Value,
                        sp.y / Screen.height * canvasHeight.Value
                    );

        var screenPoint = new Vector2(
                              transform.position.x / canvas.transform.localScale.x,
                              transform.position.y / canvas.transform.localScale.y
                          );

        var areaPosition =
            point - new Vector2(
                screenPoint.x - image.rectTransform.pivot.x * image.rectTransform.sizeDelta.x,
                screenPoint.y - image.rectTransform.pivot.y * image.rectTransform.sizeDelta.y
            );

        return alphaMap.IsFlag((int)areaPosition.x, (int)areaPosition.y);
    }
}