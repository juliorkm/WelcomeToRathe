using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualTransform : MonoBehaviour
{
    public Vector3? position = null;
    public Vector3? localPosition = null;
    public Quaternion? rotation = null;
    public Quaternion? localRotation = null;
    private float step = .1f;
    private float timeBtwnUpdates = .01f;

    public Vector3 getPosition() {
        if (position is null)
            return transform.position;
        else
            return (Vector3)position;
    }

    public Vector3 getLocalPosition() {
        if (localPosition is null)
            return transform.localPosition;
        else
            return (Vector3)localPosition;
    }

    public Quaternion getRotation() {
        if (rotation is null)
            return transform.rotation;
        else
            return (Quaternion)rotation;
    }

    public Quaternion getLocalRotation() {
        if (localRotation is null)
            return transform.localRotation;
        else
            return (Quaternion)localRotation;
    }

    private void Start() {
        StartCoroutine(updateTransform());
    }

    IEnumerator updateTransform() {
        while(true) {
            // Position
            if (!(position is null)) {
                localPosition = null;
                transform.position = Vector3.Lerp(transform.position, (Vector3)position, step);
                if (Vector3.Distance(transform.position, (Vector3)position) < .001f) {
                    transform.position = (Vector3)position;
                    position = null;
                }
            } else if (!(localPosition is null)) {
                transform.localPosition = Vector3.Lerp(transform.localPosition, (Vector3)localPosition, step);
                if (Vector3.Distance(transform.localPosition, (Vector3)localPosition) < .001f) {
                    transform.localPosition = (Vector3)localPosition;
                    localPosition = null;
                }
            }
            // Rotation
            if (!(rotation is null)) {
                localRotation = null;
                transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion)rotation, step);
                if (Quaternion.Angle(transform.rotation, (Quaternion)rotation) < .1f) {
                    transform.rotation = (Quaternion)rotation;
                    rotation = null;
                }
            } else if (!(localRotation is null)) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, (Quaternion)localRotation, step);
                if (Quaternion.Angle(transform.localRotation, (Quaternion)localRotation) < .1f) {
                    transform.localRotation = (Quaternion)localRotation;
                    localRotation = null;
                }
            }
            yield return new WaitForSeconds(timeBtwnUpdates);
        }

    }
}
