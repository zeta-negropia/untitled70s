using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;



public class TargetingSystem : MonoBehaviour
{
    public float Angle = 35.0f;

    public int NumberOfTargetsInRange = 0;
    public List<GameObject> CandidateTargets
     = new List<GameObject>();

     public GameObject ObjectClosestToCamera;
     private bool TargetButton = false;
     public bool isTargeting = false;
     private bool AxisRight = false;
     private bool AxisLeft = false;
    
    

    // Update is called once per frame
    void Update()
    {
        bool isDown = Input.GetKeyDown(KeyCode.S);

        if (ObjectClosestToCamera == null)
        {
            TargetButton = false;
        }

        for (int i = CandidateTargets.Count - 1; i >= 0; --i)
            {
                if (CandidateTargets[i] == null || !CandidateTargets[i].activeInHierarchy)
                    {
                        CandidateTargets.RemoveAt(i);
                        NumberOfTargetsInRange--;
                    
                    }
            }
        if (isDown && NumberOfTargetsInRange > 0)
            {
               List<GameObject> SortedTargetList = CandidateTargets.OrderBy(go =>
                {
                    Vector3 target_direction = go.transform.position - Camera.main.transform.position;
                    var camera_forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                    var target_dir = new Vector2(target_direction.x, target_direction.z);
                    float angle = Vector2.Angle(camera_forward, target_dir);
                    
                    return angle;

                }) .ToList();

                for (var i = 0; i < CandidateTargets.Count(); ++i)
                {
                    CandidateTargets[i] = SortedTargetList[i];

                    if (!CandidateTargets[i].activeInHierarchy)
                    {
                        CandidateTargets.RemoveAt(i);
                        NumberOfTargetsInRange--;
                    }
                }

                GameObject recent_target = ObjectClosestToCamera;

                UnTarget(recent_target);

                ObjectClosestToCamera = CandidateTargets.First();


                if (recent_target!= ObjectClosestToCamera)
                {
                    Target(ObjectClosestToCamera);
                }
                TargetButton = !TargetButton;
                
            }

            if (TargetButton && NumberOfTargetsInRange > 0 && ObjectClosestToCamera != null && ObjectClosestToCamera.activeInHierarchy)
            {
                Vector3 target_direction = ObjectClosestToCamera.transform.position - Camera.main.transform.position;
                var camera_forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                var target_dir = new Vector2(target_direction.x, target_direction.z);
                float angle = Vector2.Angle(camera_forward, target_dir);

                if (angle < Mathf.Abs(Angle))
                {
                    isTargeting = true;

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (AxisRight == false)
                        {
                            List<GameObject> SortedTargetList = CandidateTargets.OrderBy(target =>
                            {
                                Vector3 targetDir = target.transform.position - Camera.main.transform.position;

                                var cameraFor = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                                var newTarget_dir = new Vector2(targetDir.x, targetDir.z);
                                float new_angle = Vector2.SignedAngle(cameraFor, newTarget_dir);
                                return new_angle;
                            }).ToList();

                            for (var i = 0; i < CandidateTargets.Count(); ++i)
                            {
                                CandidateTargets[i] = SortedTargetList[i];
                            }
                            if (CandidateTargets.IndexOf(ObjectClosestToCamera) - 1 >=0)
                            {
                                UnTarget(ObjectClosestToCamera);

                                GameObject next_targeted_object = CandidateTargets[CandidateTargets.IndexOf(ObjectClosestToCamera) - 1];
                                Vector3 target_direction_vec3 = next_targeted_object.transform.position - Camera.main.transform.position;
                                var camera_forwar_vec2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                                var new_target_dir = new Vector2(target_direction_vec3.x, target_direction_vec3.z);
                                float angle_between_vectors = Vector2.Angle(camera_forwar_vec2, new_target_dir);

                                if (angle_between_vectors < Mathf.Abs(Angle))
                                {
                                    ObjectClosestToCamera = CandidateTargets[CandidateTargets.IndexOf(ObjectClosestToCamera) - 1];

                                    Target(ObjectClosestToCamera);
                                    AxisRight = !AxisRight;
                                }
                            }

                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
                        {
                            if (AxisLeft == false)
                            {
                                List<GameObject> SortedTargetList = CandidateTargets.OrderBy(go =>
                                {
                                    Vector3 target_Dir = go.transform.position - Camera.main.transform.position;

                                    var camForward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                                    var new_target_dir = new Vector2(target_Dir.x, target_Dir.z);

                                    float angle_between_vectors = Vector2.SignedAngle(camForward, new_target_dir);
                                    return angle_between_vectors;

                                }).ToList();
                                for (var i = 0; i < CandidateTargets.Count(); ++i)
                                {
                                    CandidateTargets[i] = SortedTargetList[i];
                                }
                                if (CandidateTargets.IndexOf(ObjectClosestToCamera) + 1 < CandidateTargets.Count)
                                {
                                    UnTarget(ObjectClosestToCamera);

                                    GameObject nextTargetedObject = CandidateTargets[CandidateTargets.IndexOf(ObjectClosestToCamera) + 1];

                                    Vector3 targetDir = nextTargetedObject.transform.position - Camera.main.transform.position;

                                    var cameraFor = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);

                                    var newtarget_dir = new Vector2(targetDir.x, targetDir.z);
                                    float anglex = Vector2.Angle(cameraFor, newtarget_dir);

                                    if (anglex < Mathf.Abs(Angle))
                                    {
                                        ObjectClosestToCamera = CandidateTargets[CandidateTargets.IndexOf(ObjectClosestToCamera) + 1];
                                    }
                                    Target(ObjectClosestToCamera);
                                    AxisLeft = !AxisLeft;
                                }
                            }
                        }
                        else
                        {
                            AxisRight = false;
                            AxisLeft = false;
                        }
                    }
                    else
                    {
                        TargetButton = false;
                    }
                }
                else
                {
                    isTargeting = false;

                    if (ObjectClosestToCamera != null)
                    {
                        UnTarget(ObjectClosestToCamera);

                        ObjectClosestToCamera = null;
                    }
                }
                //set and tell animator we are targeting
            }
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
        {
            CandidateTargets.Add(other.gameObject);
            ++NumberOfTargetsInRange;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy")
        {
            CandidateTargets.Remove(other.gameObject);
            --NumberOfTargetsInRange;
        }
    }
    private void Target(GameObject enemy)
    {
        if (enemy != null)
        {
            var agent = enemy.GetComponentInParent<EnemyMono>();

            //if (agent != null)
            //{
            //    agent.OnTargeted(GetComponent<EnemyMono>());
            //    agent.OnDie.AddListener(AgentDies);
            //}
        }
    }
    private void UnTarget(GameObject enemy)
    {
        if (enemy != null)
        {
            var agent = enemy.GetComponentInParent<EnemyMono>();

           // if (agent != null)
            //{
            //    agent.OnUnTargeted(GetComponent<EnemyMono>());
            //    agent.OnDie.RemoveListener(AgentDies);
           // }
        }
    }
    private void AgentDies (EnemyMono agent)
    {
        CandidateTargets.Remove(agent.gameObject.gameObject);
        UnTarget(agent.gameObject);
        ObjectClosestToCamera = null;
    }
}

