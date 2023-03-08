using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;

    [SerializeField]
    private int iteration;

    [SerializeField]
    private float size;

    List<GameObject> cubes = new List<GameObject>();

    void Start()
    {
        //  This statement create a game object given a prefab, position and rotation.
        Instantiate(
            //  This parameter sets a prefab for an instance to be created.
            cubePrefab,

            //  This parameter sets an instance position to (0, 0, 0).
            Vector3.zero,

            //  This parameter sets an instance rotation to (0, 0, 0).
            Quaternion.identity
        );
        
        GameObject box = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
        box.transform.localScale = new Vector3(size, size, size);
        cubes.Add(box);
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            List<GameObject> newCubes = Split(cubes);
            foreach (var box in cubes)
            {
                Destroy(box);
            }

            cubes = newCubes;
        }
    }
    
    List<GameObject> Split(List<GameObject> cubes)
    {
        List<GameObject> subCubes = new List<GameObject>();

        foreach (var cube in cubes)
        {
            float scale = cube.GetComponent<Fractal>().size;
        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 2;  y++)
            {
                for(int z = -1; z < 2; z++)
                {
                    float xx = x * (scale / 3f);
                    float yy = y * (scale / 3f);
                    float zz = z * (scale / 3f);

                    Vector3 cubePos = new Vector3(xx, yy, zz)
                                            + cube.transform.position;

                    int sum = Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z);
                    if(sum > 0)
                    {
                        GameObject copy = Instantiate(cube, cubePos, Quaternion.identity);
                        copy.GetComponent<Fractal>().size = scale / 3f;
                        copy.transform.localScale = new Vector3 (scale / 3f , scale / 3f, scale / 3f);

                        subCubes.Add(copy);
                    }
                }
            }
        }
        }

        return subCubes;
    }
    
}
