using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Slot
{
    public Seed seed;
    public int count;

	public Slot(Seed s, int c)
	{
		seed = s;
		count = c;
	}
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

	public Slot curSel;
	public Slot prevSel 
	{
		get 
		{
			return inven[(curIdx + inven.Count - 1) % inven.Count];
		} 
				
	}

	public Slot postSel 
	{
		get
		{
			return inven[(curIdx + 1) % inven.Count];
		}
	}

	System.Action onUpdateCur;

	int curIdx;
    List<Slot> inven = new List<Slot>();

	private void Awake()
	{
		instance = this;
	}
	private void Update()
	{
		if(Input.mouseScrollDelta.y > 0.5f && inven.Count > 0)
		{
			curSel = postSel;
			curIdx += 1;
			curIdx %= inven.Count;
			onUpdateCur?.Invoke();
		}
		if (Input.mouseScrollDelta.y < -0.5f && inven.Count > 0)
		{
			curSel = prevSel;
			curIdx += inven.Count - 1;
			curIdx %= inven.Count;
			onUpdateCur?.Invoke();
		}
		//foreach (var item in inven)
		//{
		//	Debug.Log(item.seed.seedName + " : " + item.count);
		//}
	}

	public void AddUpdateBhv(System.Action act)
	{
		onUpdateCur += act;
	}

	public void AddItem(Seed seed, int cnt)
	{
		Slot slot = inven.Find(item => item.seed.seedName == seed.seedName);
		if (slot != null)
		{
			slot.count += cnt;
		}
		else
		{
			inven.Add(new Slot(seed, cnt));
			if(inven.Count == 1)
				curSel = inven[0];
		}
		onUpdateCur?.Invoke();
	}
	public bool UseItem(Seed seed, int cnt, System.Action callback)
	{
		if(inven.Count == 0 || seed == null)
			return false;
		Slot slot = inven.Find(item => item.seed.seedName == seed.seedName);
		int idx = inven.FindIndex(item => item.seed.seedName == seed.seedName);
		if (slot != null && slot.count >= cnt) 
		{ 
			callback();
			slot.count -= cnt;
			if(slot.count == 0)
			{
				inven.Remove(slot);
				if(inven.Count > 0)
				{
					curSel = inven[idx];
				}
				else
				{
					curSel = null;
				}
				
			}
			onUpdateCur?.Invoke();
			return true;
		}
			return false;
		
	}
}
