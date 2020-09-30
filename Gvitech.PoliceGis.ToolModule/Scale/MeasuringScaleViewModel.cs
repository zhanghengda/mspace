using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ResourceServices;
using Mmc.Windows.Design;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule.Scale
{
    public class MeasuringScaleViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ObservableCollection<ScaleItem> ScaleItems
        {
            get { return this.scaleItems; }
            set { base.SetAndNotifyPropertyChanged<ObservableCollection<ScaleItem>>(ref this.scaleItems, value, "ScaleItems"); }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)5;
            this.ScaleItems = new ObservableCollection<ScaleItem>();
            this.ScaleItems.Add(new ScaleItem
            {
                ScaleType = ScaleType.City,
                Content=Helpers.ResourceHelper.FindKey("City"),
                Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺左.png",
                MouseOverIcon = (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺左H.png"),
                PressedIcon= (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺左S.png"),
            });
            this.ScaleItems.Add(new ScaleItem
            {
                ScaleType = ScaleType.Street,
                Content = Helpers.ResourceHelper.FindKey("Street"),
                Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺中.png",
                MouseOverIcon = (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺中H.png"),
                PressedIcon = (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺中S.png"),
            });
            this.ScaleItems.Add(new ScaleItem
            {
                ScaleType = ScaleType.Hamlet,
                Content = Helpers.ResourceHelper.FindKey("Block"),
                Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺右.png",
                MouseOverIcon = (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺右H.png"),
                PressedIcon = (base.Icon = Singleton<ResourceService>.Instance.GetImagePath() + "比例尺右S.png"),
            });
        }

        public override FrameworkElement CreatedView() { return new ScaleView(); }

        private ObservableCollection<ScaleItem> scaleItems;
    }
}