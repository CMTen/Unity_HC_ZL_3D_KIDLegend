using UnityEngine;
using UnityEngine.UI;   // 引用介面 API
using System.Collections;

namespace KID
{
    public class RandomSkill : MonoBehaviour
    {
        [Header("技能圖片：模糊與正常")]
        public Sprite[] spriteBlurs;        // 模糊圖片陣列 16
        public Sprite[] spriteSkills;       // 技能圖片陣列 8
        [Header("捲動速度"), Range(0.00001f, 3f)]
        public float speed = 0.025f;
        [Header("捲動次數"), Range(1, 10)]
        public int count = 3;
        [Header("音效")]
        public AudioClip soundScroll;
        public AudioClip soundGetSkill;

        private string[] nameSkills = { "連續射擊", "正向箭", "背向箭", "側向箭", "血量提升", "攻擊提升", "攻速提升", "爆擊提升" };
        private Image img;                  // 圖片元件
        private Button btn;                 // 按鈕元件
        private AudioSource aud;            // 音效元件   
        private Text textSkill;             // 技能名稱
        private GameObject panelSkill;      // 隨機技能物件
        private int index;                  // 隨機技能編號


        private void Start()
        {
            img = GetComponent<Image>();
            btn = GetComponent<Button>();
            aud = GetComponent<AudioSource>();
            textSkill = transform.GetChild(0).GetComponent<Text>();   // 變形.取得子物件(編號)
            panelSkill = GameObject.Find("隨機技能");

            btn.onClick.AddListener(ChooseSkill);                     // 玩家點擊按鈕 執行監聽者

            StartCoroutine(ScrollEffect());                           // 啟動協程
        }

        /// <summary>
        /// 選擇技能後：隱藏隨機技能物件
        /// </summary>
        private void ChooseSkill()
        {
            panelSkill.SetActive(false);                              // 隱藏隨機技能物件
            print("玩家選取技能：" + nameSkills[index]);              // 紀錄玩家選的技能
        }

        /// <summary>
        /// 捲動效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator ScrollEffect()
        {
            btn.interactable = false;                          // 按鈕無法點選

            // 迴圈
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < spriteBlurs.Length; i++)
                {
                    img.sprite = spriteBlurs[i];                // 圖片元件.圖片 = 模糊圖片陣列[編號]
                    aud.PlayOneShot(soundScroll, 0.2f);
                    yield return new WaitForSeconds(speed);     // 等待
                }
            }

            index = Random.Range(0, spriteSkills.Length);       // 隨機挑選 技能圖片陣列
            img.sprite = spriteSkills[index];                   // 圖片元件.圖片 = 模糊圖片陣列[隨機值]
            aud.PlayOneShot(soundGetSkill, 0.5f);
            textSkill.text = nameSkills[index];                 // 技能名稱.文字 = 技能名稱[隨機值] 

            btn.interactable = true;                            // 按鈕可以點選
        }
    }
}
