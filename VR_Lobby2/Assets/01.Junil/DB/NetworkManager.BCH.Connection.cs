using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public partial class NetworkManager
{
    /// <summary>
    /// BCH API 서버 도메인.
    /// </summary>
    private static string serverDomain_BCH
    {
        get { return "34.217.102.173"; }
    }

    /// <summary>
    /// BCH 서버 API 포트.
    /// </summary>
    private static int serverPort_BCH
    {
        get { return 6023; }
    }

    /// <summary>
    /// BCH 서버 프로토콜.
    /// </summary>
    private static string serverProtocol
    {
        get { return "http"; }
    }

    /// <summary>
    /// 서버 응답 데이터가 MessagePack인지 여부.
    /// true일 경우 MessagePack 데이터를 Json 텍스트로 변환하여 반환한다.
    /// </summary>
    private static bool serverMsgPack
    {
        get { return false; }
    }

    private static byte[] _signatureKey;
    /// <summary>
    /// 서버 검증용 시그니쳐 키 값.
    /// </summary>
    private static byte[] signatureKey
    {
        get
        {
            if (_signatureKey == null)
            {
                _signatureKey = Encoding.ASCII.GetBytes("locologic");
            }
            return _signatureKey;
        }
    }

    /// <summary>
    /// API 호출에 필요한 Request 클래스.
    /// </summary>
    public class APIRequest : WWWRequestExt
    {
        /// <summary>
        /// 호출할 API 이름.
        /// </summary>
        public string API { get; private set; }

        /// <summary>
        /// 로딩 인디케이터 표시 여부
        /// </summary>
        public bool showsIndicator { get; set; }

        /// <summary>
        /// 시그니쳐 포함 여부
        /// </summary>
        public bool signature { get; set; }

        /// <summary>
        /// 에러 발생 시 예외처리 여부
        /// </summary>
        public bool noErrorHandling { get; set; }

        /// <summary>
        /// 지연 호출 여부
        /// </summary>
        public bool deferred { get; set; }

        /// <summary>
        /// 재시도 횟수
        /// </summary>
        public int tryCount { get; set; }

        public APIRequest(string newAPI)
        {
            API = newAPI;
            url = string.Format("{0}://{1}:{2}/{3}", serverProtocol, serverDomain_BCH, serverPort_BCH, API);
            showsIndicator = true;
            signature = true;
            deferred = true;
            tryCount = 0;
        }
    }

    

}
