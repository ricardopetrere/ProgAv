using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ControllerRayCast : MonoBehaviour {

    public LayerMask collisionMask;

    public const float skinWidth = 0.015f;
    const float distanceBetweenRays = 0.2f;
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public new BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;

    public virtual void Awake() {
        collider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start() {
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing() {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float boudnsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / distanceBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boudnsWidth / distanceBetweenRays);

        horizontalRaySpacing = (horizontalRayCount <= 1 ? 0 : bounds.size.y / (horizontalRayCount - 1));
        verticalRaySpacing = (verticalRayCount <= 1 ? 0 : bounds.size.x / (verticalRayCount - 1));
    }

    public struct RaycastOrigins {
        public Vector2 topLeft,
                       topRight,
                       bottomLeft,
                       bottomRight;
    }
}
