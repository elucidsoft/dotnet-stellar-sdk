using System;
using System.IO;
using System.Net;

namespace stellar_dotnet_sdk
{
    public static class AccountUtil
    {
	public static void FundTestAccount(string Public_Key)
	{
		UriBuilder baseUri = new UriBuilder("https://horizon-testnet.stellar.org/friendbot");
		string queryToAppend = "addr=" + Public_Key;

		baseUri.Query = queryToAppend;
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri.ToString());
		request.Method = "GET";
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader sr = new StreamReader(response.GetResponseStream());
		sr.ReadToEnd();
	}
    }
}

