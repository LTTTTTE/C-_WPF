using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
namespace DragonApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string IP_load = HttpRequester.ExecuteIE("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%EB%82%B4+%EC%95%84%EC%9D%B4%ED%94%BC+%ED%99%95%EC%9D%B8", "GET");
            tb_IP.Text = "IP : " + Strings.Split(Strings.Split(IP_load, "이 컴퓨터의 IP주소는 <em>")[1], "</em>")[0];

            string Weather_Load = HttpRequester.ExecuteIE("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=" + /*weather_selected +*/ "날씨", "GET");
            var City = Strings.Split(Strings.Split(Weather_Load, "<span class=\"btn_select\" role=\"button\"> <em>")[1], "</em>")[0];
            tb_city.Text = "오늘 " + City + "의 날씨에요";
            var Weather = Strings.Split(Strings.Split(Weather_Load, "<p class=\"cast_txt\">")[1], "</p>")[0];
            tb_weather.Text = "날씨 : " + Weather;
            var temperature = Strings.Split(Strings.Split(Weather_Load, "<span class=\"todaytemp\">")[1], "</span>")[0];
            tb_temperature.Text = "기온 : " + temperature + "℃    " + "체감온도 : " + Strings.Split(Strings.Split(Weather_Load, "체감온도 <em><span class=\"num\">")[1], "</span>")[0] + "℃";

            string[] movie = new string[5];
            TextBlock[] textBlocks_movie = { tb_movie_1, tb_movie_2, tb_movie_3, tb_movie_4, tb_movie_5 };
            string Movie_load = HttpRequester.ExecuteIE("https://movie.naver.com/movie/sdb/rank/rreserve.nhn", "GET");
            for (int index = 0; index < movie.Length; index++)
            {
                textBlocks_movie[index].Text = movie[index] =string.Format("{0}위 : ",index+1)+ Strings.Split(Strings.Split(Strings.Split(Movie_load, string.Format("alt=\"0{0}\"", index+1))[1], "title=\"")[1], "\">")[0];
            }


            string search_load = HttpRequester.ExecuteIE("https://datalab.naver.com/keyword/realtimeList.naver", "GET");
            var search_date = Strings.Split(Strings.Split(search_load, "<strong class=\"rank_title v2\">")[1], "<em>")[0];
            var search_time = Strings.Split(Strings.Split(Strings.Split(search_load, "<strong class=\"rank_title v2\">")[1], "<em>")[1], "</em></strong>")[0];
            tb_search_date_time.Text = search_date + " " + search_time +"\n   실시간 검색어 순위";
            tb_movie_date.Text = search_date + "기준 \n영화 예매율 순위";

            string[] search = new string[10];
            TextBlock[] textBlocks = { tb_search_1, tb_search_2, tb_search_3, tb_search_4, tb_search_5, tb_search_6, tb_search_7, tb_search_8, tb_search_9,tb_search_10 };
            for (int index = 0; index < search.Length; index++)
            {
                textBlocks[index].Text = search[index] =string.Format("{0}위 : ",index+1)+ Strings.Split(Strings.Split(Strings.Split(search_load, string.Format("<em class=\"num\">{0}</em>",index+1))[1], "<span class=\"title\">")[1], "</span>")[0];
            }
        }

        private void bt_crawling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window1 window1 = new Window1();
                window1.Show();
                string URL = tb_url.Text;
                string Source = HttpRequester.ExecuteIE(URL, "GET", "", URL);
                // tb_Test.Text = Source;
                window1.tb_source.Text = Source;
            }
            catch
            {
                MessageBox.Show("Exception");
            }

        }

    }
}
