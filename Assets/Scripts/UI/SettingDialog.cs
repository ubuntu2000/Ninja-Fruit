using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameBasic
{
    public class SettingDialog : Dialog, IComponentCheck
    {
        public Slider musicSlider;
        public Slider sfxSlider;
        private AudioController m_auCtr;

        public bool IsComponentsNull()
        {
            return m_auCtr == null || musicSlider == null || sfxSlider == null;
        }

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            m_auCtr = FindObjectOfType<AudioController>();

            if (IsComponentsNull()) return;

            musicSlider.value = GameData.Ins.musicVol;
            sfxSlider.value = GameData.Ins.sfxVol;


        }

        public void OnMusicChange(float value)
        {
            if (IsComponentsNull()) return;

            m_auCtr.musicVol = value;
            m_auCtr.musicAus.volume = value;
            GameData.Ins.musicVol = value;
        }
        public void OnSfxChange(float value)
        {
            if (IsComponentsNull()) return;

            m_auCtr.SfxVol = value;
            m_auCtr.SfxAus.volume = value;
            GameData.Ins.sfxVol = value;
        }
    }
}