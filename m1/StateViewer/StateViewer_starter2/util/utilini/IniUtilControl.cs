using System;
using System.Collections;
using System.Collections.Generic;

namespace StateViewer_starter2 {

public partial class IniUtilControl  {

    #region make
    string make_savedcat;
    void make_init()
    {
        make_savedcat = null;
    }
    string make_out(string m_cat, string m_key, string m_val)
    {
        //if (string.IsNullOrEmpty(m_val)) return string.Empty;
        if (string.IsNullOrEmpty(m_key)) throw new SystemException( "Unexpected {5B9E168F-6D21-4EC1-A648-A2D58F87FAF5}");
        var val = m_val?.TrimEnd('\x0a','\x0d');
        //if (string.IsNullOrEmpty(val)) return string.Empty;

        var result = string.Empty;
        if (!string.IsNullOrEmpty(m_cat))
        {
            if (make_savedcat != m_cat)
            {
                result = string.Format("[{0}]",m_cat) + NL + NL;
                make_savedcat = m_cat;
            }
        }
        if (val!=null && (val.Contains("\x0a") || val.Length > 40))
        {
            result += m_key + "=@@@" + NL;
            result += m_val + NL;
            result += "@@@" + NL + NL;
        }
        else
        {
            result += m_key + "=" + val + NL;
        }           
        return result;
    }
    string make_rest(ref Hashtable rest, string m_savecat)
    {
        var result = string.Empty;
        if (rest == null) throw new SystemException("Unexpected! {F7E50F56-A5B1-4CB6-9FD6-E6E49BDF696F}");
        if (string.IsNullOrEmpty(m_savecat))
        {
            var keys = get_keys_except_cat(rest);
            foreach(var k in keys) {
                result += make_out(string.Empty, k.ToString(), (string)rest[k]);
                rest.Remove(k);
            }
            return result;
        }
        var cathashx = rest[m_savecat];        
        if (cathashx!=null)
        {
            if (cathashx is Hashtable)
            {
                var cathash = (Hashtable)cathashx;
                foreach(var k in cathash.Keys)
                {
                    result += make_out(string.Empty, k.ToString(), (string)cathash[k]);
                }
                rest.Remove(m_savecat);

                return result;
            }
            else
            {
                throw new SystemException("Unexpected! {689A9F29-EB3F-4DB2-8F86-E3D726F8960C}");
            }
        }
        return result;
    }
    string make_restall(ref Hashtable rest)
    {
        var s = string.Empty;
        var catlist = new Dictionary<string,Hashtable>();
        
        //Output Globals and collect category hash
        while (rest.Keys.Count>0)
        {
            foreach (var k in rest.Keys)
            {
                if (rest[k] is Hashtable)
                {
                    catlist.Add(k.ToString(),(Hashtable)rest[k]);
                }
                else
                {
                    var val = rest[k].ToString();
                    s += make_out(null, k.ToString(), val);
                }
                rest.Remove(k);
                break;
            }
        }

        //Outpug Categories
        foreach (var p in catlist)
        {
            foreach (var k in p.Value.Keys)
            {
                var val = p.Value[k];
                s += make_out(p.Key, k.ToString(), val?.ToString());
            }
        }
        return s;
    }

    List<string> get_keys_except_cat(Hashtable hash)
    {
        var list = new List<string>();
        foreach(var k in hash.Keys)
        {
            var v = hash[k];
            if (v is Hashtable) continue;
            list.Add(k.ToString());
        }
        return list;
    }
    #endregion
    #region others
    bool GetValIfKeyExistAndDel(ref Hashtable rest, string cat, string key, out string val)
    {
        if (rest == null) throw new SystemException("Unexpected! {73E821E0-EA1D-4AC4-A32D-8082AE4559DA}");
        val = string.Empty;
        if (string.IsNullOrEmpty(cat))
        {
            if (rest.ContainsKey(key))
            {
                var valx = rest[key];
                if (valx is string)
                {
                    rest.Remove(key);
                    val = valx.ToString();
                    return true;
                }
            }
            return false;
        }
        var hashx = rest[cat];
        if (hashx != null)
        {
            if (hashx is Hashtable)
            {
                var hash = (Hashtable)hashx;
                if (hash.ContainsKey(key))
                {
                    val = hash[key]?.ToString();
                    hash.Remove(key);
                    if (hash.Keys.Count==0)
                    {
                        rest.Remove(cat);
                    }
                    else
                    {
                        rest[cat] = hash;
                    }
                    return true;
                }
            }
            else
            {
                throw new SystemException("Unexpected! {93C375D5-03D9-4228-99F9-1AC5CB1456C4}");
            }
        }
        return false;
    }
    Hashtable CloneHash(Hashtable src)
    {
        if (src == null) throw new SystemException("Unexpected! {FC722942-EE60-4964-B5B8-19FA45F172F2}");
        var dst = new Hashtable();
        foreach(var k in src.Keys)
        {
            var vx = src[k];
            if (vx is Hashtable)
            {
                var sp = (Hashtable)vx;
                var dp = new Hashtable();
                foreach(var kp in sp.Keys)
                {
                    var s = sp[kp]?.ToString();
                    dp[kp] = s;
                }
                dst[k] = dp;
            }
            else
            {
                var s = vx?.ToString();
                dst[k] = s;
            }
        }
        return dst;
    }
    bool Split(string odr, out string cat, out string key)
    {
        cat = string.Empty;
        key = string.Empty;
        if (odr.StartsWith("=")) {
            key = odr.Substring(1);
            return true;
        }
        if (!odr.Contains("=")) return false;
        var tokens = odr.Split('=');
        if (tokens.Length!=2) return false;
        cat = tokens[0];
        key = tokens[1];
        return true;
    }
    #endregion

    #region manger
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;
    Action<bool> m_tempfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    // for tempfunc
    void SetNextState(Action<bool> func)
    {
        m_tempfunc = func;
    }
    void GoNextState()
    {
        m_nextfunc = m_tempfunc;
        m_tempfunc = null;
    }
    bool HasNextState()
    {
        return m_tempfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			if (IsEnd()) break;
			
			Update();
		}
	}

	#region    // [SYN-G-GEN OUTPUT START] indent(8) $/./$
        //             psggConverterLib.dll converted from psgg-file:doc\IniUtilControl.psgg

        /*
            E_CONST
        */
        string NL = Environment.NewLine;
        /*
            E_IMPORT
        */
        public List<string> m_order_list;
        public Hashtable   m_hash;
        public string         m_newlinechars;
        /*
            E_RESULT
        */
        public string m_s = null;
        public bool m_bOK = false;
        public string m_error = null;
        /*
            E_VERS
        */
        Hashtable m_rest;
        int m_oidx;
        string m_curodr;
        string m_savecat;
        string m_cat;
        string m_key;
        string m_val;
        /*
            S_ADD_CMT
        */
        void S_ADD_CMT(bool bFirst)
        {
            if (bFirst)
            {
                m_s += m_curodr + NL;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_NEXT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_ADD_LINESPACE
        */
        void S_ADD_LINESPACE(bool bFirst)
        {
            if (bFirst)
            {
                m_s += NL;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_NEXT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CATEGORY_AND_KEY
        */
        void S_CATEGORY_AND_KEY(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_SPLIT1);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CHECK_FIRST
        */
        void S_CHECK_FIRST(bool bFirst)
        {
            var c = m_curodr[0];
            // branch
            if (c==';') { SetNextState( S_ADD_CMT ); }
            else if (c=='=') { SetNextState( S_NO_CATEGORY_KEY ); }
            else { SetNextState( S_CATEGORY_AND_KEY ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CHECK_OIDX
        */
        void S_CHECK_OIDX(bool bFirst)
        {
            // branch
            if (m_oidx < m_order_list.Count) { SetNextState( S_GET_ORDER ); }
            else { SetNextState( S_OUTPUT_REST_KEYS ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_COPY
            残り確認用のハッシュにコピー
        */
        void S_COPY(bool bFirst)
        {
            if (bFirst)
            {
                m_rest = CloneHash(m_hash);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_MAKE_INIT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_ERROR
        */
        void S_ERROR(bool bFirst)
        {
            if (bFirst)
            {
                m_error = "Unexpected! 2212";
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_END);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_ERROR1
        */
        void S_ERROR1(bool bFirst)
        {
            if (bFirst)
            {
                m_error = "Unexpected! 2217";
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_END);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_ERROR2
        */
        void S_ERROR2(bool bFirst)
        {
            if (bFirst)
            {
                m_error = "Unexpected! 2331";
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_END);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_EXIST_KEY
            RESTに該当キーありか？
            値を得て、キー削除
        */
        void S_EXIST_KEY(bool bFirst)
        {
            var b = GetValIfKeyExistAndDel(ref m_rest, m_cat, m_key, out m_val);
            // branch
            if (b) { SetNextState( S_MAKEOUT ); }
            else { SetNextState( S_UPDATE_SAVECAT ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_GET_ORDER
        */
        void S_GET_ORDER(bool bFirst)
        {
            if (bFirst)
            {
                m_curodr = m_order_list[m_oidx];
            }
            // branch
            if (string.IsNullOrEmpty(m_curodr)) { SetNextState( S_ADD_LINESPACE ); }
            else { SetNextState( S_CHECK_FIRST ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_IS_SAME_CAT
        */
        void S_IS_SAME_CAT(bool bFirst)
        {
            // branch
            if (string.IsNullOrEmpty(m_cat)) { SetNextState( S_EXIST_KEY ); }
            else { SetNextState( S_ERROR ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_IS_SAME_CAT1
        */
        void S_IS_SAME_CAT1(bool bFirst)
        {
            // branch
            if (m_cat == m_savecat) { SetNextState( S_EXIST_KEY ); }
            else { SetNextState( S_OUTPUT_REST_KEYS ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_MAKE_INIT
            作成部分の初期化
        */
        void S_MAKE_INIT(bool bFirst)
        {
            if (bFirst)
            {
                make_init();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_OTHERS_INIT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_MAKEOUT
        */
        void S_MAKEOUT(bool bFirst)
        {
            if (bFirst)
            {
                m_s += make_out(m_cat, m_key, m_val);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_UPDATE_SAVECAT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_NEXT
        */
        void S_NEXT(bool bFirst)
        {
            if (bFirst)
            {
                m_oidx++;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CHECK_OIDX);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_NO_CATEGORY_KEY
        */
        void S_NO_CATEGORY_KEY(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_SPLIT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_OTHERS_INIT
        */
        void S_OTHERS_INIT(bool bFirst)
        {
            if (bFirst)
            {
                m_oidx=0;
                m_cat = null;
                m_savecat = null;
                m_key = null;
                m_val = null;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CHECK_OIDX);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_OUTPUT_REST_KEYS
        */
        void S_OUTPUT_REST_KEYS(bool bFirst)
        {
            if (bFirst)
            {
                m_s += make_rest(ref m_rest, m_savecat);
            }
            // branch
            if (m_oidx < m_order_list.Count) { SetNextState( S_EXIST_KEY ); }
            else { SetNextState( S_OUTPUT_RESTALL ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_OUTPUT_RESTALL
        */
        void S_OUTPUT_RESTALL(bool bFirst)
        {
            if (bFirst)
            {
                m_s += make_restall(ref m_rest);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_END);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SPLIT
        */
        void S_SPLIT(bool bFirst)
        {
            var b = Split(m_curodr, out m_cat, out m_key);
            // branch
            if (b) { SetNextState( S_IS_SAME_CAT ); }
            else { SetNextState( S_ERROR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SPLIT1
        */
        void S_SPLIT1(bool bFirst)
        {
            var b = Split(m_curodr, out m_cat, out m_key);
            // branch
            if (b) { SetNextState( S_IS_SAME_CAT1 ); }
            else { SetNextState( S_ERROR2 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_COPY);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_UPDATE_SAVECAT
        */
        void S_UPDATE_SAVECAT(bool bFirst)
        {
            if (bFirst)
            {
                m_savecat = m_cat;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_NEXT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }


	#endregion // [SYN-G-GEN OUTPUT END]

	// write your code below

	bool m_bYesNo;
	
	void br_YES(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (m_bYesNo)
			{
				SetNextState(st);
			}
		}
	}

	void br_NO(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (!m_bYesNo)
			{
				SetNextState(st);
			}
		}
	}

}
}