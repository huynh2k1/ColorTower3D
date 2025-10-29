using UnityEngine;
namespace BaseH
{
    public abstract class PanelBase : MonoBehaviour
    {
        //UI kế thừa phải override lại UIType VD: public override UIType Type => UIType.Home;
        public abstract PanelType Type { get; }    
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }

    public enum PanelType
    {
        Home,
        Game,
        Win,
        Lose,
        Pause,
        Setting,
        HowToPlay,
        Loading
    }

}
