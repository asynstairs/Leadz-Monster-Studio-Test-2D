using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileGenerator
{
    public void Instantiate(uint count);
    public void Generate(Vector3Int position);
}
