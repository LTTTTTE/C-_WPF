using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DragonApp
{
    public class HttpRequester
    {
        private static CookieContainer Cook = new CookieContainer();
       
        public static string ExecuteIE(string URL, string METHOD = "POST", string Postdata = "", string Referer = "", string CharacterSet = "", string Content = "application/x-www-form-urlencoded", string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8")
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Accept = Accept;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36";
                request.Method = METHOD;
                request.ContentType = Content;
                request.Referer = Referer;
                request.ContentLength = Postdata.Length;
                request.KeepAlive = true;
                request.CookieContainer = Cook;
                request.Timeout = 0x7530;
                if (Postdata.Length > 0)
                {
                    TextWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(Postdata);
                    writer.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.CookieContainer.Add(response.Cookies);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TextReader reader;
                    if (CharacterSet.Length > 0)
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(CharacterSet));
                    }
                    else if ((response.CharacterSet == "ms949") | (response.CharacterSet == "MS949"))
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ks_c_5601-1987"));
                    }
                    else if (response.CharacterSet == "")
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    }
                    else
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                    }
                    return reader.ReadToEnd();
                }
                request.Abort();
            }
            catch (Exception exception1)
            {
                return "error\n" + exception1.Message;
            }
            return "error";
        }

        public static string HttpUploadFile(string url, string file, string paramName, string contentType, byte[] filebytes, string referer, NameValueCollection nvc)
        {
            string boundary = "----------" + "ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7";
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 정도사파리/537.36";
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Referer = referer;
            wr.AllowWriteStreamBuffering = true;
            wr.CookieContainer = Cook;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            rs.Write(filebytes, 0, filebytes.Length);

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            HttpWebResponse wresp = (HttpWebResponse)wr.GetResponse();
            wr.CookieContainer.Add(wresp.Cookies);
            if (wresp.StatusCode == HttpStatusCode.OK)
            {
                TextReader reader = new StreamReader(wresp.GetResponseStream(), Encoding.GetEncoding(wresp.CharacterSet));
                reader = new StreamReader(wresp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                return reader.ReadToEnd();
            }
            wr.Abort();
            return "error";
        }


        public static string ImageUpload(string url, byte[] filebytes, string referer, NameValueCollection ㅈㄷ)
        {
            string image01 = "------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"Filename\"\r\n\r\n파일이름\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"imgIndex\"\r\n\r\n0\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"fileName\"\r\n\r\n파일이름\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"userId\"\r\n\r\n아이디\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"image\"; filename=\"파일이름\"\r\nContent-Type: application/octet-stream\r\n\r\n데이터";
            string image02 = "데이터\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7\r\nContent-Disposition: form-data; name=\"Upload\"\r\n\r\nSubmit Query\r\n------------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7--";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36";
                request.Method = "Post";
                request.ContentType = "multipart/form-data; boundary=----------ae0Ij5ei4cH2ei4Ef1Ef1gL6gL6KM7";
                request.Referer = referer;
                request.KeepAlive = true;
                request.AllowWriteStreamBuffering = false;
                request.CookieContainer = Cook;
                request.Timeout = 0xea60;
                request.GetRequestStream().Write(filebytes, 0, filebytes.Length);
                request.GetRequestStream().Write(Encoding.Default.GetBytes(image02.Replace("데이터", "")), 0, Encoding.Default.GetBytes(image02.Replace("데이터", "")).Length);
                request.GetRequestStream().Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.CookieContainer.Add(response.Cookies);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TextReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                    reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    return reader.ReadToEnd();
                }
                request.Abort();
            }
            catch (Exception exception1)
            {
                return "error" + exception1.Message;
            }
            return "error";
        }

    }
}
