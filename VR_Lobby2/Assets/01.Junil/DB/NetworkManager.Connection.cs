using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class NetworkManager
{
    
    public enum HTTPMethod
    {
        GET = 0,
        POST = 1
    }

    public enum ConnectionResult
    {
        None,
        Timeout,
        Success,
        Error,
        Cancel
    }

    public class WWWRequestExt
    {
        
        public string url = "";
        public HTTPMethod method = HTTPMethod.GET;
        

        // header
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        public Dictionary<string, string> headers
        {
            get { return _headers; }
            set
            {
                if (value == null) _headers = new Dictionary<string, string>();
                else _headers = new Dictionary<string, string>(value);
            }
        }

        // parameters
        private Dictionary<string, object> _parameters = new Dictionary<string, object>();
        public Dictionary<string, object> parameters
        {
            get { return _parameters; }
            set
            {
                if (value == null) _parameters = new Dictionary<string, object>();
                else _parameters = new Dictionary<string, object>(value);
            }
        }

        // timeout
        private float _timeout = 10.0f;
        public float timeout
        {
            get { return _timeout; }
            set { _timeout = Mathf.Clamp(value, 5.0f, 30.0f); }
        }
        

        public WWWRequestExt()
        {
            string keys = "abcdefghijklmnopqrstuvwxyz123456";
            AES256Cipher.SetKey(keys);
        }


        public string GetHTTPHeaderString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool first = true;
            foreach (string key in _headers.Keys)
            {
                if (_headers[key] != null)
                {
                    sb.AppendFormat("{0}[\"{1}\"]=\"{2}\"", (first ? "" : ", "), key, _headers[key]);
                    first = false;
                }
            }
            return sb.ToString();
        }

        public string GetHTTPParamsString()
        {
            string json = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
            Debug.Log(json);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("value={0}", AES256Cipher.Encrypt(json));


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //bool first = true;
            //foreach (string key in _parameters.Keys)
            //{
            //    if (_parameters[key] != null)
            //    {
            //        sb.AppendFormat("{0}{1}={2}", (first ? "" : "&"), key, _parameters[key]);
            //        first = false;
            //    }
            //}

            return sb.ToString();
        }
    }
        

}