using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawHalfCircle : MonoBehaviour
{
    public float Radius = 6;          //外半径  
    public float innerRadius = 3;     //内半径
    public float angleDegree = 360;   //扇形或扇面的角度
    public int Segments = 60;         //分割数  

    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateMesh(Radius, innerRadius, angleDegree, Segments);
    }

    public void FixedUpdate()
    {
        Vector3 mouseposition = Input.mousePosition;
        Vector3 position = Camera.main.ScreenToWorldPoint(mouseposition);
        position.z = 0;
        Vector2 direction = (position - transform.position).normalized;

        Vector2 dir = new Vector2(0, 1);
        float num1 = direction.x * dir.x + direction.y * dir.y;
        float cos = num1 / 1.0f;
        float angle = Mathf.Acos(cos) * (180 / Mathf.PI);

        //叉乘判断左右
        float left_right = direction.x * dir.y - direction.y * dir.x;
        
        if (left_right < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (angle + (angleDegree / 2))));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -(angle - (angleDegree / 2))));
        }

        meshFilter.mesh = CreateMesh(Radius, innerRadius, angleDegree, Segments);

    }
    Mesh CreateMesh(float radius, float innerradius, float angledegree, int segments)
    {
        //vertices(顶点):
        int vertices_count = segments * 2 + 2;              //因为vertices(顶点)的个数与triangles（索引三角形顶点数）必须匹配
        Vector3[] vertices = new Vector3[vertices_count];
        float angleRad = Mathf.Deg2Rad * angledegree;
        float angleCur = angleRad;
        float angledelta = angleRad / segments;
        for (int i = 0; i < vertices_count; i += 2)
        {
            float cosA = Mathf.Cos(angleCur);
            float sinA = Mathf.Sin(angleCur);

            vertices[i] = new Vector3(radius * cosA, radius * sinA,0 );
            vertices[i + 1] = new Vector3(innerradius * cosA, innerradius * sinA, 0);
            angleCur -= angledelta;
        }

        //triangles:
        int triangle_count = segments * 6;
        int[] triangles = new int[triangle_count];
        for (int i = 0, vi = 0; i < triangle_count; i += 6, vi += 2)
        {
            triangles[i] = vi;
            triangles[i + 1] = vi + 3;
            triangles[i + 2] = vi + 1;
            triangles[i + 3] = vi + 2;
            triangles[i + 4] = vi + 3;
            triangles[i + 5] = vi;
        }

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
        }

        //负载属性与mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        return mesh;
    }
}
