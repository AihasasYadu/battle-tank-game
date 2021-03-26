using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private TankController tank;
    [SerializeField] private Transform target;
    private LineRenderer lineRenderer;
    private bool isRendering;

    public TankController setTank { set { tank = value; } }
    private void Start()
    {
        isRendering = false;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.startColor = Color.white;
    }

    private void Update()
    {
        if(isRendering)
        {
            RenderLine();
        }
    }

    private void RenderLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, tank.transform.position);
        lineRenderer.SetPosition(1, target.position);
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!tank.isReloading)
        {
            isRendering = true;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isRendering = false;
        lineRenderer.enabled = false;
        tank.ShootShell();
    }
}
