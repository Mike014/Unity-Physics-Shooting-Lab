using UnityEngine;

public class ShootTheCube : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody targetRb;

    [Header("Force")]
    [SerializeField] private float forceAmount = 30f;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    [Header("Bullet Hole")]
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private float surfaceOffset = 0.001f;
    [SerializeField] private Vector3 bulletHoleRotationOffset = new Vector3(0f, 180f, 0f);

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.rigidbody != null && hit.rigidbody == targetRb)
            {
                // Forza applicata nel punto cliccato
                Vector3 direction = ray.direction.normalized;
                targetRb.AddForceAtPosition(direction * forceAmount, hit.point, forceMode);

                // Bullet hole (posizione + orientamento)
                Vector3 spawnPos = hit.point + hit.normal * surfaceOffset;

                Quaternion baseRot = Quaternion.LookRotation(-ray.direction, hit.normal);
                Quaternion rot = baseRot * Quaternion.Euler(bulletHoleRotationOffset);

                GameObject hole = Instantiate(bulletHolePrefab, spawnPos, rot);

                // Rimane attaccato al cubo colpito
                hole.transform.SetParent(hit.transform, true);
            }
        }
    }
}
