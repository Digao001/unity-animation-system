using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        this.transform.position = new Vector3(
            target.position.x,
            target.position.y,
            this.transform.position.z);
    }
}
