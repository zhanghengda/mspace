using Mmc.Wpf.Mvvm;

namespace FireControlModule
{
    public class BuildContentViewModel : BindableBase
    {
        private string buildAdrress;
        private string imageName;
        private string buildCode;
        private string buildName;

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        private string buildCodeTitle = "建筑编码";

        public string BuildCodeTitle
        {
            get { return this.buildCodeTitle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCodeTitle, value, "BuildCodeTitle"); }
        }

        private string buildAdrressTitle = "建筑地址";

        public string BuildAdrressTitle
        {
            get { return this.buildAdrressTitle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildAdrressTitle, value, "BuildAdrressTitle"); }
        }

        private string buildNameTitle = "建筑名称";

        public string BuildNameTitle
        {
            get { return this.buildNameTitle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildNameTitle, value, "BuildNameTitle"); }
        }

        public string BuildName
        {
            get { return this.buildName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildName, value, "BuildName"); }
        }

        public string BuildAdrress
        {
            get { return this.buildAdrress; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildAdrress, value, "BuildAdrress"); }
        }

        public string ImgName
        {
            get { return this.imageName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.imageName, value, "ImgName"); }
        }
    }
}