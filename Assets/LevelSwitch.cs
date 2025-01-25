using UnityEngine;
using UnityEngine.Serialization;

public class LevelSwitch : MonoBehaviour
{
    [FormerlySerializedAs("Transform")] public Transform SwitchTransform;
    public float rotationAngle;
    public float targetAngle;
    void Start()
    {
        rotationAngle = 4.0f;
        targetAngle = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = SwitchTransform.localRotation;
    

        if (!Mathf.Approximately(rotationAngle, targetAngle))
        {
            rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 10.0f);
        }
        SwitchTransform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotationAngle);
    }
}
