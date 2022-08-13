using KModkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class BottomGear2Script : MonoBehaviour
{

    static int _moduleIdCounter = 1;
    int _moduleID = 0;

    public KMBombModule a;
    public KMAudio b;
    public KMSelectable c;
    public MeshRenderer d;
    public Material e;

    private int f;
    private string[] g = { "-.", "--", "-..", "-.-", "--.", "---", "-...", "-..-", "-.-.", "-.--", "--..", "--.-", "---.", "----", "-....", "-...-", "-..-.", "-..--", "-.-..", "-.-.-", "-.--.", "-.---", "--...", "--..-", "--.-.", "--.--", "---..", "---.-", "----.", "-----", "-.....", "-....-", "-...-.", "-...--", "-..-..", "-..-.-" };
    private string h;
    private bool j;
    private bool k;
    private bool l;
    private bool m;
    private bool n;
    private Coroutine o;
    private Coroutine p;

    void Awake()
    {
        _moduleID = _moduleIdCounter++;
        f = Rnd.Range(0, 36);
        Debug.LogFormat("[Bottom Gear 2 #{0}] tweett is numbre {1}.", _moduleID, (f + 1).ToString());
        Debug.LogFormat("[Bottom Gear 2 #{0}] do {1} to klli jamms!", _moduleID, g[f].ToString());
        c.OnInteract += delegate { if (!m) StartCoroutine(s()); else n = true; return false; };
        c.OnInteractEnded += delegate { if (!m) StartCoroutine(t()); };
        a.OnActivate += delegate { b.PlaySoundAtTransform("welcome to bottom gear", transform); };
    }

    void r()
    {
        l = false;
        if (h == g[f])
        {
            Debug.LogFormat("[Bottom Gear 2 #{0}] yo0u did {1} and jams uis dewd. y7ay7!!!!!!!!!!!!!!!!!!!!!!!!!!", _moduleID, h);
            a.HandlePass();
            b.PlaySoundAtTransform("solve", transform);
            j = true;
            d.material = e;
        }
        else
        {
            a.HandleStrike();
            b.PlaySoundAtTransform("strike", transform);
            Debug.LogFormat("[Bottom Gear 2 #{0}] YOU DDI {1}, HOW DEWRE YUOI NOT OKI,L JAMS!!!!!!!!!!!! >:((((((((((", _moduleID, h);
            f = Rnd.Range(0, 36);
            Debug.LogFormat("[Bottom Gear 2 #{0}] tweett is numbre {1}.", _moduleID, (f + 1).ToString());
            Debug.LogFormat("[Bottom Gear 2 #{0}] do {1} to klli jamms!", _moduleID, g[f].ToString());
            h = "";
        }
    }

    private IEnumerator s()
    {
        n = false;
        c.AddInteractionPunch(0.5f);
        if (l)
        {
            StopCoroutine(o);
            StopCoroutine(p);
        }
        if (!j)
            o = StartCoroutine(u());
        b.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, c.transform);
        for (int i = 0; i < 3; i++)
        {
            c.transform.localPosition -= new Vector3(0, 0.005f, 0);
            yield return null;
        }
    }

    private IEnumerator t()
    {
        if (!n)
        {
            b.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonRelease, c.transform);
            m = true;
            if (k)
            {
                h += "-";
                p = StartCoroutine(v());
                l = true;
            }
            else if (l)
            {
                h += ".";
                if (l)
                    p = StartCoroutine(v());
            }
            else if (!j)
                b.PlaySoundAtTransform("audio " + (f + 1).ToString(), c.transform);
            k = false;
            if (!j)
                StopCoroutine(o);
            for (int i = 0; i < 3; i++)
            {
                c.transform.localPosition += new Vector3(0, 0.005f, 0);
                yield return null;
            }
            m = false;
        }
    }

    private IEnumerator u()
    {
        float w = 0;
        while (w < 0.5f)
        {
            yield return null;
            w += Time.deltaTime;
        }
        k = true;
        w = 0;
        b.PlaySoundAtTransform("hold", c.transform);
    }

    private IEnumerator v()
    {
        float x = 0;
        while (x < 2f)
        {
            yield return null;
            x += Time.deltaTime;
        }
        r();
        x = 0;
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "Use '!{0} -.-' to hold the button, then tap it, then hold it again.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string y)
    {
        for (int i = 0; i < y.Length; i++)
            if (y[i] != '.' && y[i] != '-')
            {
                yield return "sendtochaterror Invalid command.";
                yield break;
            }
        yield return null;
        for (int i = 0; i < y.Length; i++)
        {
            c.OnInteract();
            if (y[i] == '.')
                yield return null;
            else
                yield return new WaitUntil(() => k);
            c.OnInteractEnded();
            float z = 0;
            while (z < 0.1f)
            {
                yield return null;
                z += Time.deltaTime;
            }
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return true;
        for (int i = 0; i < g[f].Length; i++)
        {
            c.OnInteract();
            if (g[f][i] == '.')
                yield return null;
            else
                yield return new WaitUntil(() => k);
            c.OnInteractEnded();
            float z = 0;
            while (z < 0.1f)
            {
                yield return null;
                z += Time.deltaTime;
            }
        }
    }
}