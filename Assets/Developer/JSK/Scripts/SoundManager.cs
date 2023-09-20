using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Define1
{
	public enum Sound
	{
		Bgm,
		Effect,
		UI,
		MaxCount, // 종류 개수 의미
	}
}

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);

			Init();

		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public Slider bgmVolumeSlider = null;

	public Slider effectVolumeSlider = null;

	public Slider uiVolumeSlider = null;

	[SerializeField]
	private AudioMixer mixer = null;

	private float bgmValue = 0;

	private float effectValue = 0;

	private float uiValue = 0;

	public AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
	private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	private void Update()
	{
		//      if (Input.GetKeyDown(KeyCode.K))
		//      {
		//	SoundManager.instance.Play("a", Define.Sound.Bgm);
		//      }
		//if (Input.GetKeyDown(KeyCode.L))
		//{
		//	SoundManager.instance.Play("b", Define.Sound.Bgm);
		//}
		//if (Input.GetKeyDown(KeyCode.P))
		//{
		//	SoundManager.instance.Play("c");
		//}

		//      if (Input.GetKeyDown(KeyCode.T))
		//      {
		//	Time.timeScale = 0;
		//      }
		//else if (Input.GetKeyDown(KeyCode.Y))
		//{
		//	Time.timeScale = 1;
		//}
	}

	public void Init()
	{
		GameObject root = GameObject.Find("@Sound");
		if (root == null)
		{
			root = new GameObject { name = "@Sound" };
			Object.DontDestroyOnLoad(root);

			string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
			for (int i = 0; i < soundNames.Length - 1; i++)
			{
				GameObject go = new GameObject { name = soundNames[i] };
				audioSources[i] = go.AddComponent<AudioSource>();
				go.transform.parent = root.transform;
			}

			audioSources[(int)Define.Sound.Bgm].loop = true;
		}

		audioSources[(int)Define.Sound.Bgm].outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
		audioSources[(int)Define.Sound.Effect].outputAudioMixerGroup = mixer.FindMatchingGroups("Effect")[0];
		audioSources[(int)Define.Sound.UI].outputAudioMixerGroup = mixer.FindMatchingGroups("UI")[0];

		bgmVolumeSlider.value = 0.5f;
		effectVolumeSlider.value = 0.5f;
		uiVolumeSlider.value = 0.5f;
	}

	public void Pause(Define.Sound type = Define.Sound.Bgm)
	{
		//bgm
		if (type == Define.Sound.Bgm)
		{
			audioSources[(int)Define.Sound.Bgm].Pause();
		}
		//effect
		else
		{
			audioSources[(int)Define.Sound.Effect].Pause();
		}

	}

	public void Stop(Define.Sound type = Define.Sound.Bgm)
	{
		if (type == Define.Sound.Bgm)
		{
			audioSources[(int)Define.Sound.Bgm].Stop();
		}
		else
		{
			audioSources[(int)Define.Sound.Effect].Stop();
		}

	}

	public void RePlay(Define.Sound type = Define.Sound.Bgm)
	{
		if (type == Define.Sound.Bgm)
		{
			audioSources[(int)Define.Sound.Bgm].Play();
		}
		else
		{
			audioSources[(int)Define.Sound.Effect].Play();
		}
	}

	public void Play(string path, Define.Sound type = Define.Sound.Effect, float volum = 1.0f)
	{
		AudioClip audioClip = GetOrAddAudioClip(path, type);
		Play(audioClip, type, volum);
	}

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float volume = 1.0f)
	{
		if (audioClip == null)
			return;

		if (type == Define.Sound.Bgm)
		{
			AudioSource audioSource = audioSources[(int)Define.Sound.Bgm];

			audioSource.volume = volume;
			audioSource.clip = audioClip;
			if (!audioSource.isPlaying)
				audioSource.Play();
		}
		else
		{
			AudioSource audioSource = audioSources[(int)Define.Sound.Effect];
			audioSource.volume = volume;
			audioSource.PlayOneShot(audioClip);
		}
	}

	AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
	{
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}";

		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm)
		{
			audioClip = Load<AudioClip>(path);
		}
		else
		{
			if (audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = Load<AudioClip>(path);
				audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.LogError($"AudioClip Missing ! {path}");

		return audioClip;
	}

	public void Clear()
	{
		// 재생기 전부 재생 스탑, 음반 빼기
		foreach (AudioSource audioSource in audioSources)
		{
			audioSource.clip = null;
			audioSource.Stop();
		}
		// 효과음 Dictionary 비우기
		audioClips.Clear();
	}

	public T Load<T>(string path) where T : Object
	{
		if (typeof(T) == typeof(GameObject))
		{
			string name = path;
			int index = name.LastIndexOf('/');
			if (index >= 0)
				name = name.Substring(index + 1);
		}
		return Resources.Load<T>(path);
	}

	public void BGMSoundVolume(float value)
	{
		mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
	}

	public void EffectSoundVolume(float value)
	{
		mixer.SetFloat("Effect", Mathf.Log10(value) * 20);
	}

	public void UISoundVolume(float value)
	{
		mixer.SetFloat("UI", Mathf.Log10(value) * 20);
	}

	public void BGMVolumValueSetint()
	{
		//bgmSliderValueText.text = a.ToString();
		if (bgmVolumeSlider.value != bgmValue)
		{
			bgmValue = bgmVolumeSlider.value;
		}
		else
		{
			bgmVolumeSlider.value = bgmValue;
		}

		BGMSoundVolume(bgmValue);
	}

	public void EffectVolumValueSetint()
	{
		if (effectVolumeSlider.value != effectValue)
		{
			effectValue = effectVolumeSlider.value;
		}
		else
		{
			effectVolumeSlider.value = effectValue;
		}

		EffectSoundVolume(effectValue);
	}

	public void UIVolumValueSetint()
	{
		if (uiVolumeSlider.value != uiValue)
		{
			uiValue = uiVolumeSlider.value;
		}
		else
		{
			uiVolumeSlider.value = uiValue;
		}

		UISoundVolume(uiValue);
	}
}

