using UnityEngine;

public static class GameObjectExtention
{
    public static void DestroyVFXGameObject(this GameObject vfx)
    {
        var ps = vfx.GetComponent<ParticleSystem>();
        if (ps == null)
        {
            ps = vfx.GetComponentInChildren<ParticleSystem>();
        }
        GameObject.Destroy(vfx, ps.main.duration);
    }
}