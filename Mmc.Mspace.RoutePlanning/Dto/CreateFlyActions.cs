using System.Collections.Generic;

namespace Mmc.Mspace.RoutePlanning.Dto
{
    public class CreateFlyActions
    {
        // 设置起飞动作
        public List<StationItems> CreateTakeOffActions(int id, double lat, double lon, double alt,double velocity)
        {
            var tempList = new List<StationItems>
            {
                CreateTakeOffItem(id,lat, lon, alt),
                CreateVelocityItem(velocity)
            };

            return tempList;
        }

        // 设置着陆动作
        public List<StationItems> CreateLandingActions(int id, double lat, double lon, double alt)
        {
            var tempList = new List<StationItems>
            {
                CreateLandingItem(lat, lon, alt,id)
            };

            return tempList;
        }

        // 设置一般变化动作
        public List<StationItems> CreateNormalActions(int id, double lat, double lon, double alt, double yaw = 0,
            double hoverTime = 0)
        {
            var tempList = new List<StationItems>
            {
                CreateNormalItem(lat, lon, alt, id,hoverTime),
                //CreateYawItem(yaw,angul) // 偏航自动制定
            };

            return tempList;
        }

        // 设置拍照动作，需设置偏航（调转机头）-设置俯仰-拍照
        public List<StationItems> CreateTakePhotoActions(int id, double lat, double lon, double alt, double yaw,double pitch, double hoverTime=3, double angul=60)
        {
            var tempList = new List<StationItems>
            {
                CreateNormalItem(lat, lon, alt, id,hoverTime),
                CreateYawItem(yaw,angul),
                CreatePitchAngleItem(pitch),
                CreateTakePhotoItem()
            };

            return tempList;
        }

        // 设置速度变化动作
        public List<StationItems> CreateVelocityChangeActions(int id, double lat, double lon, double alt, double velocity =2,double hoverTime=0)
        {
            var tempList = new List<StationItems>
            {
                CreateNormalItem(lat, lon, alt,id,hoverTime),
                CreateVelocityItem(velocity)
            };

            return tempList;
        }
        
        private StationItems CreateTakeOffItem(int id, double lat, double lon, double alt)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 22,
                coordinate = new List<double>() { lat, lon, alt },
                frame = 3,
                id = id,
                param1 = 0,
                param2 = 0,
                param3 = 0,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreateLandingItem(double lat, double lon, double alt,int id)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 21,
                coordinate = new List<double>() { lat, lon, alt },
                frame = 3,
                id = id,
                param1 = 0,
                param2 = 0,
                param3 = 0,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreateNormalItem(double lat, double lon, double alt, int id,double hoverTime)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 16,
                coordinate = new List<double>() { lat, lon, alt },
                frame = 3,
                id = id,
                param1 = hoverTime,
                param2 = 0,
                param3 = 0,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreateVelocityItem(double velocity)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 178,
                coordinate = new List<double>() {0, 0, 0},
                frame = 2,
                id = 0,
                param1 = 1,
                param2 = velocity,
                param3 = -1,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreateYawItem(double yaw, double angularVelocity = 60)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 115,
                coordinate = new List<double>() { 0, 0, 0 },
                frame = 2,
                id = 0,
                param1 = yaw >= 0 ? yaw : yaw + 360,
                param2 = angularVelocity,
                param3 = -1,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreatePitchAngleItem(double angle)
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 205,
                coordinate = new List<double>() { 0, 0, 0 },
                frame = 2,
                id = 0,
                param1 = angle > 20 ? 20 : angle,
                param2 = 0,
                param3 = -1,
                param4 = 0,
                type = "missionItem"
            };
        }

        private StationItems CreateTakePhotoItem()
        {
            return new StationItems()
            {
                autoContinue = true,
                command = 203,
                coordinate = new List<double>() { 0, 0, 0 },
                frame = 2,
                id = 0,
                param1 = 0,
                param2 = 0,
                param3 = -1,
                param4 = 0,
                type = "missionItem"
            };
        }
    }
    
}
