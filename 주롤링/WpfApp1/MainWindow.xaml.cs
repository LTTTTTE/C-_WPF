namespace WpfApp1
{
    using Microsoft.VisualBasic;
    using System.Windows;

    public partial class MainWindow : Window
    {
        string weather_selected;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( tb_url.Text.Length == 0 )
            {
                MessageBox.Show("입력해라");
                return;
            }
            

           /* string MemberNumber = "";
            string CafeName = "";
            string Manager = "";
            string ManagerID = "";*/
            string URL = tb_url.Text;
            if ( URL.Contains("http://") == false && URL.Contains("https://") == false )
                URL = URL.Insert(0, "https://");
            try
            {
                string source = HttpRequester.ExecuteIE(URL, "GET", "", URL);
                //string source = HttpRequester.ExecuteIE("www.naver.com", "GET");
                #region 크롤링
                tb_fullsource.Text = source;
              /*  var temperature = Strings.Split(Strings.Split(source, "<span class=\"todaytemp\">")[1], "</span>")[0];
                tb_temperature.Text = "기온 : " + temperature+"℃";
                var Weather = Strings.Split(Strings.Split(source, "<p class=\"cast_txt\">")[1], "</p>")[0];
                tb_weather.Text ="날씨 : "+ Weather;*/
                /*  CafeName = Strings.Split(Strings.Split(source, "var cafenameTitle = \"")[1], "\"")[0];
                  try
                  {
                      MemberNumber = Strings.Split(Strings.Split(source, "<a href=\"#\" onclick=\"$('hiddenOpenMemberInfoLayer').style.display = '';\" class=\"gm-tcol-c\"><span class=\"ico\"></span><br><em>")[1], "<")[0];
                  }
                  catch
                  {
                      try
                      {
                          MemberNumber = Strings.Split(Strings.Split(source, "<em>")[2], "<span class=")[0];
                      }
                      catch { MemberNumber = "0"; }
                  }

                  Manager = Strings.Split(Strings.Split(source, "<div class=\"ellipsis gm-tcol-c\"><div class=\"ellipsis\">")[1], "<")[0];
                  ManagerID = Strings.Split(Strings.Split(source, "/member/")[1], "/")[0];*/
                #endregion

                /*tb_fullsource.Text = source;
                tb_cafename.Text = "카페명 : " + CafeName;
                tb_count.Text = "회원수 : " + MemberNumber;
                tb_manager.Text = "매니저 : " + Manager + "/" + ManagerID;*/
            }
            catch
            {
                MessageBox.Show("URL 똑띠 입력해라");
                return;
            }


        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string IPload = HttpRequester.ExecuteIE("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%EB%82%B4+%EC%95%84%EC%9D%B4%ED%94%BC+%ED%99%95%EC%9D%B8", "GET");
            tb_ip.Text =  Strings.Split(Strings.Split(IPload, "이 컴퓨터의 IP주소는 <em>")[1], "</em>")[0];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // %EB%82%A0%EC%94%A8
                //                                             https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%EB%B6%80%EC%82%B0%EB%82%A0%EC%94%A8;;
                //string Weather_Load = HttpRequester.ExecuteIE("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=%EB%B6%80%EC%82%B0%EB%82%A0%EC%94%A8", "GET");
                string Weather_Load = HttpRequester.ExecuteIE("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=1&ie=utf8&query=" + weather_selected + "날씨", "GET");
                var City = Strings.Split(Strings.Split(Weather_Load, "<span class=\"btn_select\" role=\"button\"> <em>")[1], "</em>")[0];
                tb_city.Text = "오늘 " + City + "의 날씨에요";
                var Weather = Strings.Split(Strings.Split(Weather_Load, "<p class=\"cast_txt\">")[1], "</p>")[0];
                tb_weather.Text = "날씨 : " + Weather;
                var temperature = Strings.Split(Strings.Split(Weather_Load, "<span class=\"todaytemp\">")[1], "</span>")[0];
                tb_temperature.Text = "기온 : " + temperature + "℃\t" + "체감온도 : " + Strings.Split(Strings.Split(Weather_Load, "체감온도 <em><span class=\"num\">")[1], "</span>")[0] + "℃";
            }
            catch
            {
                MessageBox.Show("URL 똑띠 입력해라");
                return;
            }

        }

        private void ComboBoxItem_Selected_Busan(object sender, RoutedEventArgs e)
        {
            weather_selected = "부산";
        }

        private void ComboBoxItem_Selected_Seoul(object sender, RoutedEventArgs e)
        {
            weather_selected = "서울";
        }

        private void ComboBoxItem_Selected_Jeju(object sender, RoutedEventArgs e)
        {
            weather_selected = "제주도";
        }
    }
}
