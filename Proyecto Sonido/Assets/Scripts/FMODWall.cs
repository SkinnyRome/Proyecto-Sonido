using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODWall : MonoBehaviour
{

    private FMOD.System _system;
    private FMOD.Geometry _geometry;
    private int _polygonIndex;

    public float directOcclusion;
    public float reverbOcclusion;

    private const int PLANE_FACTOR = 10;

    // Start is called before the first frame update
    void Start()
    {
        _system = SoundManager.instance.GetSystem();
        SoundManager.instance.checkError(_system.createGeometry(1, 4, out _geometry));

        CreateAndSetGeometry();

        
    }

    // Update is called once per frame
    void Update()
    {
        Update3DAttributes();

    }

    

    private void Update3DAttributes()
    {
        //Cogemos la rotación y la posición
        FMOD.VECTOR position = FMODUnity.RuntimeUtils.ToFMODVector(gameObject.transform.position);
        FMOD.VECTOR forward = FMODUnity.RuntimeUtils.ToFMODVector(gameObject.transform.forward);
        FMOD.VECTOR up = FMODUnity.RuntimeUtils.ToFMODVector(gameObject.transform.up);

        //Ponemos la pared en la posición y mirando hacia donde mira el gameobject
        _geometry.setPosition(ref position);
        _geometry.setRotation(ref forward, ref up);
    }

    private void CreateAndSetGeometry()
    {
        Update3DAttributes();

        Vector3 size = gameObject.transform.localScale;

        FMOD.VECTOR[] vertices = new FMOD.VECTOR[4];
        vertices[0].x = ((-size.x / 2) * PLANE_FACTOR);
        vertices[0].z = (size.y * PLANE_FACTOR);
        vertices[0].y = 0;

        vertices[1].x = ((size.x / 2) * PLANE_FACTOR);
        vertices[1].z = (size.y * PLANE_FACTOR);
        vertices[1].y = 0;

        vertices[2].x = ((size.x / 2) * PLANE_FACTOR);
        vertices[2].z = (-size.y * PLANE_FACTOR);
        vertices[2].y = 0;

        vertices[3].x = ((-size.x / 2) * PLANE_FACTOR);
        vertices[3].z = (-size.y * PLANE_FACTOR);
        vertices[3].y = 0;

        SoundManager.instance.checkError(_geometry.addPolygon(directOcclusion, reverbOcclusion, true, 4, vertices, out _polygonIndex));
        
    }
}
