using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskDrawer.Assets
{
    public class TouchDraw : MonoBehaviour
    {
        Coroutine _drawing;
        public GameObject linePrefab;
        public Camera mainCam;
        public static List<LineRenderer> drawnLineRenderers = new List<LineRenderer>();
        public Scratch scratchScript;
        public float minDistance = 0.1f;
        private Vector3 previousPosition;

        void Update(){
            if(Input.GetMouseButtonDown(0)){
                StartLine();
            }
            if(Input.GetMouseButtonUp(0)){
                FinishLine();
            }
        }

        private void StartLine(){
            if(_drawing!=null){
                previousPosition = transform.position;
                StopCoroutine(_drawing);
            }
            _drawing = StartCoroutine(DrawLine());
        }

        private void FinishLine(){
            if(_drawing!=null)
                StopCoroutine(_drawing);
        }
        IEnumerator DrawLine(){
            GameObject newGameObject = Instantiate(linePrefab, new Vector3(0,0,0), Quaternion.identity);
            LineRenderer line =  newGameObject.GetComponent<LineRenderer>();
            drawnLineRenderers.Add(line);
            line.positionCount = 0;
            while(true){
                Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                if (Vector3.Distance(position, previousPosition) > minDistance)
                {
                    if (previousPosition == transform.position)
                    {
                        //line.SetPosition(0, position);
                    }
                    else
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, position);
                    }
                    
                    previousPosition = position;
                    
                    scratchScript.AssignScreenAsMask();
                }
               
                yield return null;
            }
        }
    }
}

