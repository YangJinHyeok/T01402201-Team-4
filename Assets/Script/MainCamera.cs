using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Camera camera;

    private Transform character;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Player").transform;
    }

    // Update is called once per frame


    public Vector3 offset;
    void Update()
    {
        /*
         * 처음 시작시 카메라 사이즈 크게 해서 전체 맵 보여줌
         *  몬스터 스폰 할 때 스폰 하는 몬스터 보여줌
         * 이후 플레이어에게로 화면 고정
         */
    
        Vector3 position = character.position;
        transform.position = new Vector3(position.x + offset.x, position.y + offset.y, offset.z);
    }
}
