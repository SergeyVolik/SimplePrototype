using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SerV112.UtilityAIEditor
{

    static class VisualElementMeshUtils
    {
        public static void CreateCurveMesh(Rect contentRect, float thickness, ICurve curve, MeshGenerationContext mgc, int points = 200, float startX = 0f, float endX = 1f)
        {
            var r = contentRect;
            float x = startX;
            float y;
            float offset = 1f / points;

            float rationX = r.width / 1;
            float rationY = r.height / 1;

            List<List<Vertex>> vertexesList = new List<List<Vertex>>();
            var vertexes = new List<Vertex>();

            var newPoint1 = Vector3.zero;
            var newPoint2 = Vector3.zero;

            while (x <= endX)
            {

                y = curve.Evaluate(x);

                newPoint1 = new Vector3(x * rationX, r.height - y * rationY, Vertex.nearZ);
                newPoint2 = new Vector3(x * rationX, r.height - y * rationY, Vertex.nearZ);


                var res = VisualElementMeshUtils.AddVertexes(newPoint1, newPoint2, vertexes, r);

                if (!res)
                {
                    if (vertexes.Count > 2)
                    {
                        vertexesList.Add(vertexes);
                    }

                    vertexes = new List<Vertex>();
                }

                x += offset;


            }

            x = 1;
            y = curve.Evaluate(x);
            newPoint1 = new Vector3(x * rationX, r.height - y * rationY, Vertex.nearZ);
            newPoint2 = new Vector3(x * rationX, r.height - y * rationY, Vertex.nearZ);

            VisualElementMeshUtils.AddVertexes(newPoint1, newPoint2, vertexes, r);

            if (vertexes.Count > 2)
            {
                vertexesList.Add(vertexes);
            }


            var indicesList = new List<List<ushort>>();

            for (var j = 0; j < vertexesList.Count; j++)
            {
                var indices = new List<ushort>();

                for (int i = 0; i < vertexesList[j].Count - 2; i += 2)
                {


                    var v1 = vertexesList[j][i];
                    var v2 = vertexesList[j][i + 1];
                    var v3 = vertexesList[j][i + 2];

                    var vec = v3.position - v1.position;
                    var prep = MathUtils.GetPerpendicular(vec).normalized* thickness;

                    v1.position = v1.position + new Vector3(prep.x, prep.y);
                    v2.position = v2.position - new Vector3(prep.x, prep.y);

                    vertexesList[j][i] = v1;
                    vertexesList[j][i + 1] = v2;

                    indices.Add((ushort)i);
                    indices.Add((ushort)(i + 2));
                    indices.Add((ushort)(i + 1));

                    indices.Add((ushort)(i + 2));
                    indices.Add((ushort)(i + 3));
                    indices.Add((ushort)(i + 1));
                }

                indicesList.Add(indices);
            }


            for (var j = 0; j < vertexesList.Count; j++)
            {


                MeshWriteData mwd = mgc.Allocate(vertexesList[j].Count, indicesList[j].Count, null);


                mwd.SetAllVertices(vertexesList[j].ToArray());
                mwd.SetAllIndices(indicesList[j].ToArray());
            }
        }
        private static bool AddVertexes(Vector3 newPoint1, Vector3 newPoint2, List<Vertex> vertexes, Rect contentRect)
        {

            if (MathUtils.PointInBounds(newPoint1, contentRect) && MathUtils.PointInBounds(newPoint2, contentRect))
            {
                var vertex1 = new Vertex();
                vertex1.position = newPoint1;
                vertex1.tint = Color.green;
                vertexes.Add(vertex1);

                var vertex2 = new Vertex();
                vertex2.position = newPoint2;
                vertex2.tint = Color.green;
                vertexes.Add(vertex2);

                return true;
            }

            return false;
        }
    }
}
