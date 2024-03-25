using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptiveMaterialManager : MonoBehaviour
{
    [UnityEngine.SerializeField]
    public DescriptiveMaterial [] MaterialList; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DescriptiveMaterial ResolveMaterial(DescriptiveMaterials material) 
    {
        return ResolveMaterial((int)material);
    }

    public DescriptiveMaterial ResolveMaterial(int material) 
    {
        if (material < MaterialList.Length) 
        {
            return MaterialList[material];
        }
        return MaterialList[0];
    }

}

public enum DescriptiveMaterials 
{ 
    none,
    wood,
    stone,
    concrete,
    metal,
    wet,
    tile,
    gravel
}
[System.Serializable]
public struct DescriptiveMaterial 
{
    public string MaterialName;
    public Material[] GunshotDecals;
    public AudioClip[] GunshotHitSounds;
    public AudioClip[] ImpactSounds;
    public AudioClip[] FootstepSounds;
}