using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraReposition : MonoBehaviour {
    float minDist = 1f;
    float maxDist = 4f;
    float smooth = 10f;
    Vector3 dollyDir;
    float dist;
    void Awake() {
        dollyDir = transform.localPosition.normalized;
        dist = transform.localPosition.magnitude;
    }
    void Update() {
        print(transform);
        Vector3 adjustedCamPos = transform.TransformPoint(dollyDir * maxDist);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, adjustedCamPos, out hit)) dist = Mathf.Clamp((hit.distance * 0.9f), minDist, maxDist);
        else dist = maxDist;
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dist, Time.deltaTime * smooth);
    }
}
