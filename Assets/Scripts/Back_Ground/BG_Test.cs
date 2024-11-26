using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Test : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _mesh_renderer;

    private Vector2 _mesh_offset;
    // Start is called before the first frame update
    void Start()
    {
        _mesh_offset = _mesh_renderer.sharedMaterial.mainTextureOffset;
    }

    private void OnDisable()
    {
        _mesh_renderer.sharedMaterial.mainTextureOffset = _mesh_offset;
    }
    // Update is called once per frame
    void Update()
    {
        var x = Mathf.Repeat(Time.time * _speed, 1);
        var offset = new Vector2(x, _mesh_offset.y);

        _mesh_renderer.sharedMaterial.mainTextureOffset = offset;
        Debug.Log("hi");
    }
}
