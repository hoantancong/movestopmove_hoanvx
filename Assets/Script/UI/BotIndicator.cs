using UnityEngine;
using UnityEngine.UI;

public class BotIndicator : MonoBehaviour
{
    public Transform enemyTarget;
    public Canvas canvas;
    public GameObject indicatorPrefab;
    public RectTransform panel;

    private GameObject indicator;
    private RectTransform canvasRect;
    private RectTransform panelRect;
    private RectTransform indicatorRect;

    void Start()
    {
        indicator = Instantiate(indicatorPrefab, panel.transform);
        canvasRect = canvas.GetComponent<RectTransform>();
        panelRect = panel.GetComponent<RectTransform>();
        indicatorRect = indicator.GetComponent<RectTransform>();

        indicator.transform.Find("Arrow").gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(enemyTarget.position);
        bool isOnScreen = viewportPosition.z > 0 && viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, Camera.main.WorldToScreenPoint(enemyTarget.position), canvas.worldCamera, out localPoint);

        // Giới hạn indicator trong Panel
        localPoint.x = Mathf.Clamp(localPoint.x, -panelRect.sizeDelta.x / 2, panelRect.sizeDelta.x / 2);
        localPoint.y = Mathf.Clamp(localPoint.y, -panelRect.sizeDelta.y / 2, panelRect.sizeDelta.y / 2);

        if (!isOnScreen)
        {
            indicator.transform.Find("Arrow").gameObject.SetActive(true);
            indicator.transform.Find("Point").gameObject.SetActive(true);
            indicatorRect.anchoredPosition = localPoint;

            float angle = Mathf.Atan2(localPoint.y - indicatorRect.anchoredPosition.y, localPoint.x - indicatorRect.anchoredPosition.x) * Mathf.Rad2Deg;
            indicatorRect.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            indicator.transform.Find("Arrow").gameObject.SetActive(false);
            indicator.transform.Find("Point").gameObject.SetActive(true);
            indicatorRect.anchoredPosition = localPoint;
        }
    }
}
