using UnityEngine;

public class spriteUtillity
{
    public static void flip(GameObject gameObject)
    {
        flip(gameObject.transform);
    }
    public static void flip(Transform transform)
    {
        transform.localScale = new Vector3()
        {
            x = transform.localScale.x * -1,
            y = transform.localScale.y,
            z = transform.localScale.z
        };
    }
}