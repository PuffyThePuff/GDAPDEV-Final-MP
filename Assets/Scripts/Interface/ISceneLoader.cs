using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneLoader
{
    void LoadScene(int index);
    void LoadScene(string name);
}
