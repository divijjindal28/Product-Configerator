using UnityEngine;

public class GetRaticlePosition : MonoBehaviour
{
    [SerializeField]
    private GameObject reticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GetRaticlePosition : " + reticle.transform.position);
    }
}
