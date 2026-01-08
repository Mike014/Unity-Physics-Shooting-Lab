using UnityEngine;

public class Instantiator2D : MonoBehaviour
{
    [Header("Prefab & Parent")]
    [SerializeField] private GameObject quadPrefab;
    [SerializeField] private Transform parent; // opzionale

    [Header("Grid Size")]
    [Min(1)][SerializeField] private int righe = 10;
    [Min(1)][SerializeField] private int colonne = 10;

    [Header("Spacing / Offset")]
    [SerializeField] private float offsetX = 1.05f;
    [SerializeField] private float offsetY = 1.05f;

    [Header("Origin")]
    [SerializeField] private Vector3 startPosition = Vector3.zero;

    private void Start()
    {
        if (quadPrefab == null)
        {
            Debug.LogError("Instantiator2D: quadPrefab non assegnato nell'Inspector.");
            return;
        }

        CreaGriglia();
    }

    private void CreaGriglia()
    {
        int r = 0;

        while (r < righe)
        {
            int c = 0;

            while (c < colonne)
            {
                // Posizione: riga = Y (o Z se preferisci), colonna = X
                float x = startPosition.x + (c * offsetX);
                float y = startPosition.y + (r * offsetY);

                Vector3 pos = new Vector3(x, y, startPosition.z);

                GameObject quad = Instantiate(quadPrefab, pos, quadPrefab.transform.rotation);

                if (parent != null)
                    quad.transform.SetParent(parent, true);

                quad.name = $"Quad_{r}_{c}";

                c++;
            }

            r++;
        }
    }
}

