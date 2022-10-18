using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera virtualCamera;
    private float shakerTime;
    private float comboBuffer;
    private int combo;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineBasicMultiChannelPerlin =
    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        EventManager.Instance.Subscribe("EnemyAttacked", OnEnemyAttacked);
        EventManager.Instance.Subscribe("ShakeCamera", OnShakeCamera);
    }

    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("EnemyAttacked", OnEnemyAttacked);
        EventManager.Instance.Unsubscribe("ShakeCamera", OnShakeCamera);
    }
    void OnShakeCamera(object[] param)
    {
        ShakeCamera(1f, 0.2f);
    }
    
    void OnEnemyAttacked(object[] param)
    {
        if (comboBuffer <= 0)
        {
            comboBuffer = 0.2f;
            combo = 1;
        }
        else
        {
            comboBuffer = 0.2f;
            combo++;
        }
        if (combo >= 3)
        {
            ShakeCamera(1,0.2f);
            combo = 0;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (comboBuffer > -1)
        {
            comboBuffer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShakeCamera(1, 0.1f);
        }
        if(shakerTime >= 0)
        {
            shakerTime -= Time.deltaTime;
            if (shakerTime <= 0)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }

    }

    private void ShakeCamera(float intensity,float time)
    {


        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakerTime = time;

    }
}
