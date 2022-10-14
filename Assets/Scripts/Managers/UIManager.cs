using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    class UIManager : Singleton<UIManager>
    {
        class UIElement
        {
            public string Resource;
            public bool Cache;
            public GameObject Instance;
        }
        private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

        //需要使用的UI必须在这里注册资源地址
        public UIManager()
        {
            this.UIResources.Add(typeof(UISystemConfig), new UIElement() { Resource = "UI/UISystemConfig", Cache = true });
        }
        ~UIManager()
        {

        }

        public T Show<T>()
        {
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Open);
            Type type = typeof(T);
            if (this.UIResources.ContainsKey(type))
            {
                
                UIElement info = this.UIResources[type];
                if (info.Instance != null)
                {
                    info.Instance.SetActive(true);
                }
                else
                {
                    UnityEngine.Object prefab = Resources.Load(info.Resource);
                    if (prefab == null)
                    {
                        return default(T);
                    }
                    info.Instance =(GameObject)GameObject.Instantiate(prefab, GameObject.Find("Canvas").transform);
                    info.Instance.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                }
                return info.Instance.GetComponent<T>();
            }
            return default(T);
        }

        public void Close(Type type)
        {
            if (this.UIResources.ContainsKey(type))
            {
                UIElement info = this.UIResources[type];
                if (info.Cache)
                {
                    info.Instance.SetActive(false);
                }
                else
                {
                    GameObject.Destroy(info.Instance);
                    info.Instance = null;
                }
            }
        }
    }
}
