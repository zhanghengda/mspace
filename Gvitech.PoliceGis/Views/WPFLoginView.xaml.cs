using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using GFramework.BlankWindow;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Languagepack.LanguageManager;
using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.Models.User;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Mvvm;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// WPFLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class WPFLoginView : BlankWindow
    {
        private readonly string dbPath = System.Windows.Forms.Application.LocalUserAppDataPath + "\\" + ConfigPath.ConfigDb;
        private readonly string dbConfigKey = "LoginUserDatasConfigKey";
        private LiteDbHelper liteDbHelper = new LiteDbHelper();

        private readonly string desKey = "desK1022";
        private readonly string desIv = "desIv022";

        private bool isChanged = false;
        private ObservableCollection<LoginModel> loginUsers = new ObservableCollection<LoginModel>();

        public WPFLoginView()
        {
            InitializeComponent();

            Loaded += (s, e) => Init();
        }

        private void Init()
        {
            string configFile = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig;

            if (!File.Exists(configFile))
            {
                MessageBox.Show(Helpers.ResourceHelper.FindKey("ConfigFileMiss"));
                SystemLog.Log(string.Format("找不到配置文件:{0}", configFile));
                return;
            }

            var config = JsonUtil.DeserializeFromFile<dynamic>(configFile);

            if (config.poiUrl == null || config.mspaceVersion == null)
            {
                MessageBox.Show(Helpers.ResourceHelper.FindKey("ConfigItemError"));
                SystemLog.Log(string.Format("配置项不能为空:poiUrl or mspaceVersion"));
                return;
            }

            HttpServiceUtil.MspaceHostUrl = config.poiUrl;
            HttpServiceUtil.MspaceVersion = config.mspaceVersion;

            WebConfig.MspaceHostUrl = config.poiUrl;
            WebConfig.MspaceVersion = config.mspaceVersion;
            WebConfig.UavSocketUri = config.uavSocketUri;
            double TracePoiDistance = 5000;
            double LabelMaxDistance = 1000;
            if (config.tracePoiMaxDistance != null && double.TryParse(config.tracePoiMaxDistance.ToString(), out TracePoiDistance))
                WebConfig.TracePoiMaxDistance = TracePoiDistance;
            if (config.labelMaxDistance != null && double.TryParse(config.labelMaxDistance.ToString(), out LabelMaxDistance))
                WebConfig.LabelMaxDistance = LabelMaxDistance;


            #region 语言选项
            var dicLanguages = new Dictionary<string, string>();
            int j = 0, index = 0;
            foreach (var languagese in LanguageManager.Languages)
            {
                dicLanguages.Add(languagese.Code, languagese.Name);

                if (languagese.Name == CacheData.CurrentLanguage)
                {
                    index = j;
                }
                j++;
            }

            cmbLanguages.ItemsSource = dicLanguages;
            cmbLanguages.SelectedIndex = index;
            #endregion

            #region 加载Logo配置
            string logoConfigFile = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.LogoConfig;

            if (File.Exists(logoConfigFile))
            {
                try
                {
                    var logoConfig = JsonUtil.DeserializeFromFile<dynamic>(logoConfigFile);

                    if (logoConfig.TopTitleIcon != null)
                    {
                        if (!string.IsNullOrWhiteSpace(logoConfig.TopTitleIcon.ToString()))
                        {
                            topIcon.Source = logoConfig.TopTitleIcon;
                        }
                        else
                        {
                            topIcon.Source = null;
                        }

                    }

                    if (logoConfig.CenterLogoIcon != null)
                    {
                        if (!string.IsNullOrWhiteSpace(logoConfig.CenterLogoIcon.ToString()))
                        {
                            centerLogo.Source = logoConfig.CenterLogoIcon;
                        }
                        else
                        {
                            centerLogo.Source = null;
                        }
                    }

                    if (logoConfig.BottomTitleIcon != null)
                    {
                        if (!string.IsNullOrWhiteSpace(logoConfig.BottomTitleIcon.ToString()))
                        {
                            bottomIcon.Source = logoConfig.BottomTitleIcon;
                        }
                        else
                        {
                            bottomIcon.Source = null;
                        }
                    }

                    if (logoConfig.BottomTitle != null)
                    {
                        if (!string.IsNullOrWhiteSpace(logoConfig.BottomTitle.ToString()))
                        {
                            bottomTitle.Text = logoConfig.BottomTitle;
                        }
                        else
                        {
                            bottomTitle.Text = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载Logo配置异常:{ex.Message}");
                    Application.Current.Shutdown(0);
                }
            }
            #endregion

            #region 加载用户名和密码

            liteDbHelper.OpenDb(dbPath);
            var lst = liteDbHelper.GetCollections<LoginDataModel>(dbConfigKey);

            foreach (var loginModel in lst)
            {
                loginUsers.Add(new LoginModel
                {
                    Id = loginModel.Id,
                    username = loginModel.username,
                    password = loginModel.password,
                    isVisible = Visibility.Visible
                });
            }

            listLoginUser.ItemsSource = loginUsers;

            tbLoginUser.TextChanged += (s, e) =>
            {
                if (!isChanged)
                {
                    if (!string.IsNullOrWhiteSpace(tbLoginUser.Text))
                    {
                        listLoginUser.SelectedIndex = -1;

                        int count = 0;

                        foreach (var loginModel in loginUsers)
                        {
                            string loginUser = tbLoginUser.Text.Trim();
                            if (loginModel.username.Contains(loginUser))
                            {
                                loginModel.isVisible = Visibility.Visible;
                                count++;
                            }
                            else
                            {
                                loginModel.isVisible = Visibility.Collapsed;
                            }
                        }

                        if (count > 0)
                        {
                            popLogin.IsOpen = true;
                        }
                        else
                        {
                            popLogin.IsOpen = false;
                        }
                    }
                    else
                    {
                        popLogin.IsOpen = false;
                        tbLoginPassword.Password = string.Empty;
                    }
                }
            };

            #endregion
        }

        public void LanguageSwitching(string lang)
        {
            try
            {
                if (string.IsNullOrEmpty(lang)) return;
                var language = LanguageManager.Languages.SingleOrDefault(t => t.Name == lang);
                LanguageManager.ChangeLanguages(Application.Current.Resources, language);
                CacheData.SaveLanguage(lang);
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LoginView.LanguageSwitching", ex);
            }
        }

        private void BtnClose_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void CmbLanguages_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageSwitching(cmbLanguages.SelectedValue.ToString());
        }

        private void btnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            tbMessage.Text = string.Empty;
            tbMessage.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(tbLoginUser.Text) || string.IsNullOrWhiteSpace(tbLoginPassword.Password))
            {
                tbMessage.Text = "请输入用户名或密码";
                tbMessage.Visibility = Visibility.Visible;
                return;
            }

            LoginModel model = new LoginModel
            {
                username = tbLoginUser.Text.Trim(),
                password = tbLoginPassword.Password
            };

            //var httpRequestModel = HttpServiceHelper.Instance.PostRequestForResultModel("/api/login/gislogin",
            //    JsonUtil.SerializeToString(model));

            HttpService httpService = new HttpService();
            string result = httpService.RequestService($"{WebConfig.MspaceHostUrl}/api/login/usercenter", JsonUtil.SerializeToString(model), method: "POST");
            var httpRequestModel = JsonUtil.DeserializeFromString<HttpResultModel>(result);

            if (httpRequestModel.status == "1")
            {
                string token = JsonUtil.DeserializeFromString<dynamic>(httpRequestModel.data.ToString()).token;
                HttpServiceUtil.Token = token;


                GetUserInfo();
                this.DialogResult = true;

                liteDbHelper.OpenDb(dbPath);
                var res = liteDbHelper.FindOne<LoginDataModel>(dbConfigKey, t => t.username == model.username);
                if (res == null)
                {
                    liteDbHelper.Insert(dbConfigKey, new LoginDataModel
                    {
                        username = model.username,
                        password = DESEncrypt(model.password)
                    });
                }
                else
                {
                    liteDbHelper.Update(dbConfigKey, new LoginDataModel
                    {
                        Id = res.Id,
                        username = model.username,
                        password = DESEncrypt(model.password)
                    });
                }
            }
            else
            {
                tbMessage.Text = httpRequestModel.message;
                tbMessage.Visibility = Visibility.Visible;
            }

        }

        public void GetUserInfo()
        {
            try
            {
                string userApi = UserCfgInterface.UserCng;
                var resutl = HttpServiceHelper.Instance.GetRequestAsync(userApi);
                CacheData.UserInfo = JsonUtil.DeserializeFromString<UserInfo>(resutl);
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LoginView.GetUserInfo", ex);
            }
        }

        /// <param name="encryptedValue">要加密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>加密后的字符串</returns>  
        public string DESEncrypt(string originalValue)
        {

            using (DESCryptoServiceProvider sa
                = new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(desKey), IV = Encoding.UTF8.GetBytes(desIv) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(originalValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,
                            CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public string DESDecrypt(string encryptedValue)
        {
            using (DESCryptoServiceProvider sa =
                new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(desKey), IV = Encoding.UTF8.GetBytes(desIv) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }

        private void ListLoginUser_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popLogin.IsOpen = false;
            var model = listLoginUser.SelectedItem as LoginModel;

            if (model != null)
            {
                isChanged = true;

                tbLoginUser.Text = model.username;

                string password = DESDecrypt(model.password);
                tbLoginPassword.Password = password;

                isChanged = false;
            }
        }

        private void BtnDelete_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            MmcConfirmationBox mmcConfirmation = new MmcConfirmationBox
            {
                Title = "删除记录",
                Msg = "确定删除这条记录?",
                Owner = this,
            };
            mmcConfirmation.ShowDialog();

            if (mmcConfirmation.IsOk)
            {
                if ((sender as Image)?.DataContext is LoginModel model)
                {
                    popLogin.IsOpen = false;
                    liteDbHelper.Delete<LoginDataModel>(dbConfigKey, t => t.username == model.username);
                    loginUsers.Remove(model);
                }
            }
        }
    }

    public class LoginDataModel : BindableBase
    {
        public string username { get; set; }
        public string password { get; set; }

        public int Id { get; set; }


    }

    public class LoginModel : LoginDataModel
    {
        private Visibility _isVisible;
        public Visibility isVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged("isVisible");
            }
        }
    }
}
