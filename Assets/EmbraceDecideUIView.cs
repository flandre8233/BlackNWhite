using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbraceDecideUIView : SingletonMonoBehavior<EmbraceDecideUIView>
{
    [SerializeField]
    RectTransform DecideView;

    [SerializeField]
    RectTransform DecideLeftView;
    [SerializeField]
    RectTransform DecideRightView;

    [SerializeField]
    RectTransform DecidePerfectLeftView;
    [SerializeField]
    RectTransform DecidePerfectRightView;

    private void Start()
    {
        GetComponentInChildren<CanvasGroup>().alpha = 0;
    }

    private void ToUpdate()
    {
        DecideHitView();
        DecideAreaView();
    }

    public void SetOpen()
    {
        GetComponentInChildren<CanvasGroup>().alpha = 1;
        globalUpdateManager.instance.registerUpdateDg(ToUpdate);
    }
    public void SetClose()
    {
        GetComponentInChildren<CanvasGroup>().alpha = 0;
        globalUpdateManager.instance.UnregisterUpdateDg(ToUpdate);
    }

    void DecideAreaView()
    {
        float NewVal = EmbraceDecide.instance.GetLeftSideDecideArea / 100;

        DecideLeftView.anchorMin = new Vector2(NewVal, DecideLeftView.anchorMin.y);
        DecideLeftView.anchorMax = new Vector2(NewVal, DecideLeftView.anchorMax.y);

        NewVal = EmbraceDecide.instance.GetRightSideDecideArea / 100;

        DecideRightView.anchorMin = new Vector2(NewVal, DecideRightView.anchorMin.y);
        DecideRightView.anchorMax = new Vector2(NewVal, DecideRightView.anchorMax.y);

        NewVal = EmbraceDecide.instance.GetPerfectLeftSideDecideArea / 100;

        DecidePerfectLeftView.anchorMin = new Vector2(NewVal, DecidePerfectLeftView.anchorMin.y);
        DecidePerfectLeftView.anchorMax = new Vector2(NewVal, DecidePerfectLeftView.anchorMax.y);

        NewVal = EmbraceDecide.instance.GetPerfectRightSideDecideArea / 100;

        DecidePerfectRightView.anchorMin = new Vector2(NewVal, DecidePerfectRightView.anchorMin.y);
        DecidePerfectRightView.anchorMax = new Vector2(NewVal, DecidePerfectRightView.anchorMax.y);

    }

    void DecideHitView()
    {
        float NewVal = EmbraceDecide.instance.GetDecideVal / 100;
        DecideView.anchorMin = new Vector2(NewVal, DecideView.anchorMin.y);
        DecideView.anchorMax = new Vector2(NewVal, DecideView.anchorMax.y);

    }
}
