package crc6493855b22b6fa0721;


public class TextToSpeechInternalImplementation
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.speech.tts.TextToSpeech.OnInitListener,
		android.speech.tts.TextToSpeech.OnUtteranceCompletedListener
{

	public TextToSpeechInternalImplementation ()
	{
		super ();
		if (getClass () == TextToSpeechInternalImplementation.class) {
			mono.android.TypeManager.Activate ("Microsoft.Maui.Media.TextToSpeechInternalImplementation, Microsoft.Maui.Essentials", "", this, new java.lang.Object[] {  });
		}
	}

	public void onInit (int p0)
	{
		n_onInit (p0);
	}

	private native void n_onInit (int p0);

	public void onUtteranceCompleted (java.lang.String p0)
	{
		n_onUtteranceCompleted (p0);
	}

	private native void n_onUtteranceCompleted (java.lang.String p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
