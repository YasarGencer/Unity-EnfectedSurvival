using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Movement movemet;

    //FOLLOW
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;

    //COMPONENTS
    Camera Cam;

    //ZOOM
    private float targetZoom;
    public float zoomAmount;
    public float zoomLerpSpeed;

    void Start() 
    {
        Cam = Camera.main; //Camera component
    }

    void FixedUpdate()
    {
       SmoothCameraFollow();
       CameraZoom();
    }

    void SmoothCameraFollow()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (movemet.moveSpeed + smoothSpeed) * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    void CameraZoom()
    {
        float scroll;
        scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomAmount;
        targetZoom = Mathf.Clamp(targetZoom, 5, 8);
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

    }

    //cam shake
    public void CloseShootShake()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool("shootShake", false);
    }
}
