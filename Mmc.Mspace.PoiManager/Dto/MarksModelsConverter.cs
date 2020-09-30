using Mmc.Mspace.PoiManagerModule.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Mmc.Mspace.Services.HttpService;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class MarksModelsConverter
    {
        public MarkerNew MarkerConverting(PostMarkerNew inModel)
        {
            var outModel = new MarkerNew()
            {
                MarkerId = inModel.marker_id,
                Geom = inModel.geom,
                Code = inModel.code,
                Address = inModel.address,
                Type = inModel.type,
                Title = inModel.title,
                Style = inModel.style,

                Size = double.TryParse( inModel.lp_size,out double size)?size:0 ,
                Phone = inModel.phone,
                Operator = inModel.@operator,
                Level = inModel.level,
                Status = inModel.status,
                UserId = int.TryParse( inModel.user_id ,out int idnum)?idnum:0,
                //AddTime = inModel.addtime,
                Letter = inModel.letter,
                Detail = inModel.detail,

            };

            var tempTags = new List<TagItem>();
            if (inModel.tags.Count > 0)
            {
                foreach (var tag in inModel.tags) {
                    string id = Convert.ToString(tag.id);
                    if (MarkerHelper.Instance.TagsDic.ContainsKey(id))
                        tempTags.Add( tag);

            } }
            outModel.Tags = new ObservableCollection<TagItem>(tempTags);


            if ((bool)inModel.img?.ToLower().Contains(WebConfig.MspaceHostUrl))
                outModel.ImgPath = inModel.img;
            else
                inModel.img = string.Format("{0}/resource{1}", WebConfig.MspaceHostUrl, inModel.img);

            if (outModel.Type == 1)
            {
                if (inModel.cat_id == 0) inModel.cat_id = 1;
                outModel.CatId = inModel.cat_id;
                //outModel.cat_Name = MarkerHelper.Instance.PoiTypeDic[inModel.cat_id].cat_name;
                //marker.cat_url = _poiTypeDic[markerInfo.Marker.cat_id].cat_url;
            }

            //if(inModel)
            return outModel;
        }

        public PostMarkerNew MarkerConverting(MarkerNew inModel)
        {
            var outModel = new PostMarkerNew()
            {
                marker_id = inModel.MarkerId,
                geom = inModel.Geom,
                code = inModel.Code,
                img =inModel.ImgPath,
                address = inModel.Address,
                type = inModel.Type,
                //cat_id =inModel.CatId,
                title = inModel.Title,
                style = inModel.Style,
                lp_size = inModel.Size.ToString(),
                phone = inModel.Phone,
                @operator = inModel.Operator,
                detail = inModel.Detail,
                level = inModel.Level,
                status = inModel.Status,
                user_id = inModel.UserId.ToString(),
                //addtime = inModel.AddTime,
                letter = inModel.Letter
            };

            //if (inModel.ImgPath.ToLower().Contains("http"))
                outModel.img = inModel.ImgPath;
            //else
            //    outModel.img = string.Format("{0}/resource{1}", _poiHost, inModel.img);

            if (outModel.type == 1)
            {
                if (inModel.CatId == 0) inModel.CatId = 1;
                outModel.cat_id = inModel.CatId;
                //marker.cat_Name = MarkerHelper.Instance.PoiTypeDic[inModel.cat_id].cat_name;
                //marker.cat_url = _poiTypeDic[markerInfo.Marker.cat_id].cat_url;
            }

            return outModel;
        }

        public AccountNew AccountConverting(PostAccountNew inModel)
        {
            var time = DateTime.Now.ToString("d");
            var outModel  =new AccountNew();
            outModel.MarkerId = Convert.ToInt32(inModel.marker_id);
            outModel.Id = Convert.ToInt32(inModel.id);
            outModel.Title = inModel.title;
            outModel.ImgNum = Convert.ToInt32(inModel.img_num);
            outModel.ProblemTime =string.IsNullOrEmpty(inModel.problem_time)? time: DateTime.Parse(inModel.problem_time).ToString("d");
            outModel.IsShowInReport = inModel.is_show.Equals("1") ? "显示" : "不显示";
            outModel.Video = inModel.video;

            var imgPaths = inModel.img.Split(',');
            List<string> temp = new List<string>();
            if (imgPaths.Length > 0)
            {
                foreach (var path in imgPaths)
                {
                    if ((bool)path.ToLower().Contains(WebConfig.MspaceHostUrl))
                        temp.Add(path);
                    else
                        temp.Add(string.Format("{0}/resource{1}", WebConfig.MspaceHostUrl, path));
                }
            }

            outModel.ImgPathList = new ObservableCollection<string>(temp);

            return outModel;
        }

        public PostAccountNew AccountConverting(AccountNew inModel)
        {
            var outModel = new PostAccountNew();
            outModel.marker_id = inModel.MarkerId.ToString();
            outModel.id = inModel.Id.ToString();
            outModel.title = inModel.Title;
            outModel.img_num = inModel.ImgNum.ToString();
            if(!string.IsNullOrEmpty(inModel.ProblemTime))
            {
                outModel.problem_time = DateTime.TryParse(inModel.ProblemTime.ToString(), out DateTime result) ? result.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            }
            outModel.is_show = inModel.IsShowInReport.Equals("显示") ? "1" : "2";
            outModel.video = inModel.Video;

            outModel.img = string.Join(",",inModel.ImgPathList);

            return outModel;
        }
    }
}