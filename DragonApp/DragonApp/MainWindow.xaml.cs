using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Windows.Threading;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Timers;

namespace DragonApp
{
    public partial class MainWindow : Window
    {
        string current_user = "Guest";

        public System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

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
            string[] movie_code = new string[5];
            var movie_def_uri = "https://movie.naver.com/movie/bi/mi/basic.nhn?code=";

            TextBlock[] textBlocks_movie = { tb_movie_1, tb_movie_2, tb_movie_3, tb_movie_4, tb_movie_5 };
            Hyperlink[] hypers_movie = { hyper_movie_1, hyper_movie_2, hyper_movie_3, hyper_movie_4, hyper_movie_5 };
            string Movie_load = HttpRequester.ExecuteIE("https://movie.naver.com/movie/sdb/rank/rmovie.nhn", "GET");
            for (int index = 0; index < movie.Length; index++)
            {
                movie[index] = Strings.Split(Strings.Split(Strings.Split(Movie_load, string.Format("alt=\"0{0}\"", index + 1))[1], "title=\"")[1], "\">")[0];
                textBlocks_movie[index].Text = string.Format("{0}위 : ", index + 1) + movie[index];
                movie_code[index] = Strings.Split(Strings.Split(Movie_load, string.Format("<li class=\"ranking0{0}\"><a href=\"/movie/bi/mi/basic.nhn?code=", index + 1))[1], "\"")[0];
                hypers_movie[index].NavigateUri = new Uri(movie_def_uri + movie_code[index]);
            }

            string search_load = HttpRequester.ExecuteIE("https://datalab.naver.com/keyword/realtimeList.naver", "GET");
            var search_date = Strings.Split(Strings.Split(search_load, "<div class=\"select_inbo _picker_component\" data-datetime=\"")[1], "\"")[0];
            var search_time = " ";//Strings.Split(Strings.Split(Strings.Split(search_load, "<strong class=\"rank_title v2\">")[1], "<em>")[1], "</em></strong>")[0];
            tb_search_date_time.Text = search_date + " " + search_time + "\n   실시간 검색어 순위";
            tb_movie_date.Text = search_date + "기준 \n영화 예매율 순위";

            string[] search = new string[10];
            string[] search_code = new string[10];
            var search_def_uri = "https://search.naver.com/search.naver?sm=top_hty&fbm=1&ie=utf8&query=";

            TextBlock[] textBlocks = { tb_search_1, tb_search_2, tb_search_3, tb_search_4, tb_search_5, tb_search_6, tb_search_7, tb_search_8, tb_search_9, tb_search_10 };
            Hyperlink[] hypers_search = { hyper_search_1, hyper_search_2, hyper_search_3, hyper_search_4, hyper_search_5, hyper_search_6, hyper_search_7, hyper_search_8, hyper_search_9, hyper_search_10 }; ;
            for (int index = 0; index < search.Length; index++)
            {
                search[index] = Strings.Split(Strings.Split(Strings.Split(search_load, string.Format("<em class=\"num\">{0}</em>", index + 1))[1], "<span class=\"title\">")[1], "</span>")[0];
                textBlocks[index].Text = string.Format("{0}위 : ", index + 1) + search[index];
                search_code[index] = "";
                hypers_search[index].NavigateUri = new Uri(search_def_uri + search[index]);
            }

            string dust_load = HttpRequester.ExecuteIE("https://aqicn.org/city/busan/kr/", "GET");
            string dust = Strings.Split(Strings.Split(dust_load, "message:\'부산광역시 ")[1], " 2")[0];
            tb_dust.Text = dust;
            #region forDB
            string to_db_weather = Strings.Split(Weather, ",")[0];
            tb_dummy_weather.Text = to_db_weather + " : " + temperature;
            tb_dummy_yymmddtt.Text = search_date;
            #endregion

            //스케줄러
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1800000);
            //timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += new EventHandler(timer_Tick);//이벤트
            timer.Start();

            //백그라운드 워크  앙댐

            //window_Loaded
            ni.Icon = new System.Drawing.Icon("C:\\icon.ico");
            ni.Text = "DRAGON_WPF";
            ni.Visible = true;
            ni.DoubleClick +=
            delegate (object sender, EventArgs args)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ni.Visible = true;
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
            return;
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

        private void bt_login_Click(object sender, RoutedEventArgs e)
        {

            List<SqlParameter> sql_params = new List<SqlParameter>();
            sql_params.Add(new SqlParameter("Username", tb_id.Text));
            sql_params.Add(new SqlParameter("Password", tb_pswd.Password));

            DataTable dt_login_results = DAL.Exec_sp("ValidateLogin", sql_params);

            if (dt_login_results.Rows.Count == 1)
            {
                string user = dt_login_results.Rows[0][1].ToString();
                MessageBox.Show($"'{user}' 님 반갑습니다.");
                current_user = user;
                grid_login.Visibility = Visibility.Collapsed;
                grid_logged.Visibility = Visibility.Visible;
                tb_hello.Text = $"'{user}' 님 반갑습니다.";

            }
            else
            {
                MessageBox.Show("로그인실패 : " + dt_login_results.Rows.Count);
            }
        }

        private void bt_register_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
        }

        private void bt_testest_Click(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            window3.Show();
        }

        private void bt_myinfo_Click(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            window4.Show();
        }

        private void bt_logout_Click(object sender, RoutedEventArgs e)
        {
            current_user = "Guest";
            grid_logged.Visibility = Visibility.Collapsed;
            grid_login.Visibility = Visibility.Visible;
            tb_id.Text = "";
            tb_pswd.Password = "";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void bt_weather_update_Click(object sender, RoutedEventArgs e)
        {
            func_refresh();
            func_update_db_weather();
        }

        private void bt_weather_grid_Click(object sender, RoutedEventArgs e)
        {
            Window5 window5 = new Window5();
            window5.Show();
        }

        private void bt_refresh_Click(object sender, RoutedEventArgs e)
        {
            func_refresh();
        }

        private void func_refresh()
        {
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
            string[] movie_code = new string[5];
            var movie_def_uri = "https://movie.naver.com/movie/bi/mi/basic.nhn?code=";

            TextBlock[] textBlocks_movie = { tb_movie_1, tb_movie_2, tb_movie_3, tb_movie_4, tb_movie_5 };
            Hyperlink[] hypers_movie = { hyper_movie_1, hyper_movie_2, hyper_movie_3, hyper_movie_4, hyper_movie_5 };
            string Movie_load = HttpRequester.ExecuteIE("https://movie.naver.com/movie/sdb/rank/rmovie.nhn", "GET");
            for (int index = 0; index < movie.Length; index++)
            {
                movie[index] = Strings.Split(Strings.Split(Strings.Split(Movie_load, string.Format("alt=\"0{0}\"", index + 1))[1], "title=\"")[1], "\">")[0];
                textBlocks_movie[index].Text = string.Format("{0}위 : ", index + 1) + movie[index];
                movie_code[index] = Strings.Split(Strings.Split(Movie_load, string.Format("<li class=\"ranking0{0}\"><a href=\"/movie/bi/mi/basic.nhn?code=", index + 1))[1], "\"")[0];
                hypers_movie[index].NavigateUri = new Uri(movie_def_uri + movie_code[index]);
            }

            string search_load = HttpRequester.ExecuteIE("https://datalab.naver.com/keyword/realtimeList.naver", "GET");
            var search_date = Strings.Split(Strings.Split(search_load, "<div class=\"select_inbo _picker_component\" data-datetime=\"")[1], "\"")[0];
            var search_time = " ";//Strings.Split(Strings.Split(Strings.Split(search_load, "<strong class=\"rank_title v2\">")[1], "<em>")[1], "</em></strong>")[0];
            tb_search_date_time.Text = search_date + " " + search_time + "\n   실시간 검색어 순위";
            tb_movie_date.Text = search_date + "기준 \n영화 예매율 순위";

            string[] search = new string[10];
            string[] search_code = new string[10];
            var search_def_uri = "https://search.naver.com/search.naver?sm=top_hty&fbm=1&ie=utf8&query=";

            TextBlock[] textBlocks = { tb_search_1, tb_search_2, tb_search_3, tb_search_4, tb_search_5, tb_search_6, tb_search_7, tb_search_8, tb_search_9, tb_search_10 };
            Hyperlink[] hypers_search = { hyper_search_1, hyper_search_2, hyper_search_3, hyper_search_4, hyper_search_5, hyper_search_6, hyper_search_7, hyper_search_8, hyper_search_9, hyper_search_10 }; ;
            for (int index = 0; index < search.Length; index++)
            {
                search[index] = Strings.Split(Strings.Split(Strings.Split(search_load, string.Format("<em class=\"num\">{0}</em>", index + 1))[1], "<span class=\"title\">")[1], "</span>")[0];
                textBlocks[index].Text = string.Format("{0}위 : ", index + 1) + search[index];
                search_code[index] = "";
                hypers_search[index].NavigateUri = new Uri(search_def_uri + search[index]);
            }

            #region forDB
            string to_db_weather = Strings.Split(Weather, ",")[0];
            tb_dummy_weather.Text = to_db_weather + " : " + temperature;
            tb_dummy_yymmddtt.Text = search_date;
            #endregion
            string dust_load = HttpRequester.ExecuteIE("https://aqicn.org/city/busan/kr/", "GET");
            string dust = Strings.Split(Strings.Split(dust_load, "message:\'부산광역시 ")[1], " 2")[0];
            tb_dust.Text = dust;

        }

        private void func_update_db_weather()
        {
            try
            {
                List<SqlParameter> sql_params = new List<SqlParameter>();
                sql_params.Add(new SqlParameter("yymmddtt", tb_dummy_yymmddtt.Text));
                sql_params.Add(new SqlParameter("weather", tb_dummy_weather.Text));
                sql_params.Add(new SqlParameter("dust", tb_dust.Text));
                DAL.Exec_sp("AddWeather", sql_params);

               // MessageBox.Show("업데이트완료");
            }
            catch (Exception)
            {
                MessageBox.Show("업데이트실패");
                this.Close();
                throw;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            func_refresh();
            func_update_db_weather();

        }

        private void bt_force_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(99);
        }

    }
}
