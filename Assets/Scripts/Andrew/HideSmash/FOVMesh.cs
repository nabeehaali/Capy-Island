using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FOVMesh : MonoBehaviour
{

    private Mesh mesh;
    private float startAngle;
    private float fov;
    public Transform idol;
    public Transform idolTarget;
    Vector3 origin;
    Vector3 idolVector;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //this.transform.position = idol.position ;
        Vector3 origin = this.transform.position;

    }

    void Update()
    {
        
        //Debug.Log(idolVector);
        //Debug.Log(idolTarget.position);

        float fov = 90f;
        
        int rayCount = 50;

        float angle = startAngle;
        //aimingDirection(idol.forward.normalized);
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        idolVector = (idolTarget.position - origin).normalized; //Oddly specific
        aimingDirection(idolVector);



        //this.GetComponentInParent<Transform>().position = new Vector3(0, 0, 0);




        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;
        //vertices[1] = new Vector3(5, 0, 0);
        //vertices[2] = new Vector3(-5, 0, 0);


        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) 
        {
            Vector3 vertex;
            
            Physics.Raycast(origin, VectorFromAngle(angle), out RaycastHit hit, viewDistance);

            if (hit.collider == null)
            {
                vertex = origin + VectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Debug.Log(hit.collider.tag);
                Debug.Log(hit.collider.transform.position);
                
                vertex = hit.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;

            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    public void originReset(Vector3 origin)
    {
        
    }

    float aimingDirection(Vector3 aimDir) 
    {
        startAngle = GetAngleFromVec(aimDir) - fov / 2f;
        return GetAngleFromVec(aimDir) - fov / 2f;
    }


    static Vector3 VectorFromAngle(float angle) 
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
    }

    static float GetAngleFromVec(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) 
        {
            n += 360;
        }

        return n;
    
    }
    
}
