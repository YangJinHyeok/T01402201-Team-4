using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{

    private GameObject Bazzi;
    private GameObject prefab_obj;
    public float destructionDelay = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Bazzi = GameObject.FindGameObjectWithTag("Bazzi");
        prefab_obj = Resources.Load("Prefabs/Bomb") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeBomb();
        }


    }
    // 폭탄 생성
    void MakeBomb()
    {
        GameObject obj = MonoBehaviour.Instantiate(prefab_obj);
        obj.AddComponent<BoxCollider2D>();
        obj.name = "clone";
        Vector2 pos = Bazzi.transform.position;
        obj.transform.position = pos;

        StartCoroutine(DestroyBomb(obj));
    }


    // 폭탄 삭제
    IEnumerator DestroyBomb(GameObject bomb)
    {
        yield return new WaitForSeconds(destructionDelay);

        Destroy(bomb);
    }




}
