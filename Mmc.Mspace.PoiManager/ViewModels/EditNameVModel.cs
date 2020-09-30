using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
   public class EditNameVModel : CheckedToolItemModel
    {
        public Action<int,QueryWktGroup> UpdateName;
        public EditNameView editName = new EditNameView();
        public ICommand CloseCmd { get; set; }
        public ICommand UpdateCmd { get; set; }
        private string newName;
        QueryWktGroup query = null;
        int index;
    
        public EditNameVModel(int _index , QueryWktGroup _queryWktGroup)
        {

            editName.DataContext = this;
            NewName = _queryWktGroup.Name;
            query = _queryWktGroup;
            index = _index;
          
            this.CloseCmd = new RelayCommand(() =>
            {                
                CloseEditName();
            });
            this.UpdateCmd = new RelayCommand(() =>
            {
                if (query != null)
                {
                    query.Name = NewName;
                    var statue = UpdateNameOnServer();
                    if (statue)
                    {
                        UpdateName(index, query);
                    }
                    
                }              
            });
            
          
        }
        public void CloseEditName()
        {           
           editName.Hide();
        }
        public void OpenEditName()
        {
            editName.Show();
        }
        public string NewName
        {
            get { return newName; }
            set { newName = value; NotifyPropertyChanged("NewName"); }
        }

        private bool UpdateNameOnServer()
        {
            bool UpdateStatue = false; ;
            if (query != null)
            {
                string url = MarkInterface.QueryWktGroupUpdateName;
                var jsonObj = new JObject();
                var array = new JArray();                
                for (int i = 0; i < query.WktStringList.Count; i++)
                {
                    var obj = new JObject { { "id", "content" } };
                    obj["id"] = i;
                    obj["content"] = query.WktStringList[i];
                    array.Add(obj);
                }
                jsonObj["content"] = array;
                jsonObj["name"] = NewName;
                jsonObj["id"] = query.ID;
                string json = jsonObj.ToString();
                string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
                if (resStr == null || resStr == "")
                {
                    Messages.ShowMessage("修改失败,该区域管理名称已存在");
                    UpdateStatue = false;
                }
                else
                {
                    JsonTextReader reader = new JsonTextReader(new StringReader(resStr));
                    JObject jobj = (JObject)JToken.ReadFrom(reader);
                    if (jobj["status"].ToString() == "1")
                    {
                        Messages.ShowMessage("修改成功");
                        UpdateStatue = true;
                    }
                    else
                    {
                        Messages.ShowMessage("修改失败,该区域管理名称已存在");
                        UpdateStatue = false;
                    }
                }
            }
            return UpdateStatue;
        }
    }
}
