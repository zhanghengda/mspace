using System.Collections.Generic;

namespace FireControlModule
{
    public class ThreeColorItemData
    {
        public string Title { get; set; }
        public List<ThreeColorItem> ThreeColorItems { get; set; }

        public static ThreeColorItemData GetThreeTestItems()
        {
            ThreeColorItemData data = new ThreeColorItemData();
            data.Title = "三色预警";
            data.ThreeColorItems = new List<ThreeColorItem>();
            data.ThreeColorItems.Add(new ThreeColorItem() { ColorFill = "#FFFF4500", Item = "红色预警" });
            data.ThreeColorItems.Add(new ThreeColorItem() { ColorFill = "#FF008B00", Item = "绿色预警" });
            data.ThreeColorItems.Add(new ThreeColorItem() { ColorFill = "#FFFFA500", Item = "黄色预警" });
            return data;
        }

        public static ThreeColorItemData GetSupervisoryReviewTestItems(string tille = "隐患排查")
        {
            ThreeColorItemData data = new ThreeColorItemData();
            data.Title = tille;
            data.ThreeColorItems = new List<ThreeColorItem>();
            data.ThreeColorItems.Add(new ThreeColorItem() { ColorFill = "#FFFF4500", Item = "不合格" });
            data.ThreeColorItems.Add(new ThreeColorItem() { ColorFill = "#FF008B00", Item = "合格" });
            return data;
        }
    }

    public class ThreeColorItem
    {
        public string ColorFill { get; set; }

        public string Item { get; set; }
    }
}