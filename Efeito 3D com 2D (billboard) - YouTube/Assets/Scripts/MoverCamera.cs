using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    public Transform posicaoDaCamera;

    // Update is called once per frame
    void Update()
    {
        PosicionarCamera();
    }

    private void PosicionarCamera()
    {
        transform.position = posicaoDaCamera.position;
    }
}
