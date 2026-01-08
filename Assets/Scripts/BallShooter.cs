using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint; // opzionale, se null uso la camera

    [Header("SphereCast (linea di tiro)")]
    [Min(0.01f)][SerializeField] private float castRadius = 0.25f;
    [Min(0.1f)][SerializeField] private float maxDistance = 50f;
    [SerializeField] private LayerMask grayWallMask;

    [Header("Ball settings")]
    [Min(0.1f)][SerializeField] private float speed = 15f; // oppure forza, ma qui è velocità
    [Min(0.1f)][SerializeField] private float destroyAfterSeconds = 5f;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (cam == null || ballPrefab == null)
        {
            Debug.LogError("BallShooter: manca Camera o Ball Prefab.");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // SphereCast: controlla se la linea di tiro "spessa" colpisce un muro grigio
        bool hitGrayWall = Physics.SphereCast(
            ray,
            castRadius,
            out RaycastHit hit,
            maxDistance,
            grayWallMask,
            QueryTriggerInteraction.Ignore
        );

        if (hitGrayWall)
        {
            Debug.Log("Tiro bloccato: muro grigio colpito -> " + hit.collider.name);
            return;
        }

        // Se libero: istanzia la palla
        Vector3 spawnPos = (spawnPoint != null) ? spawnPoint.position : ray.origin;
        Quaternion spawnRot = Quaternion.LookRotation(ray.direction, Vector3.up);

        GameObject ball = Instantiate(ballPrefab, spawnPos, spawnRot);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Direzione assegnata al momento dello spawn (come chiede la slide)
            rb.velocity = ray.direction.normalized * speed;
        }

        Destroy(ball, destroyAfterSeconds);
    }
}
