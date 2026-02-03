using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RuntimeNavMeshPlane : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    public GameObject plane;
    public GameObject parent;
    private GameObject planeGameObject;
    void Start()
    {
        CreatePlane();
        BuildNavMesh();
    }

    void CreatePlane()
    {
        planeGameObject = Instantiate(plane);
        planeGameObject.transform.SetParent(parent.transform);
    }

    void BuildNavMesh()
    {
        navMeshSurface = planeGameObject.GetComponent<NavMeshSurface>();
        navMeshSurface.collectObjects = CollectObjects.All;
        navMeshSurface.useGeometry = NavMeshCollectGeometry.RenderMeshes;
        navMeshSurface.BuildNavMesh();
    }
}
