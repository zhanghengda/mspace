namespace Mmc.Mspace.Services.NavigationService
{
    public class CameraTourData
    {
        public int CameraTourId { get; set; }

        public int TourGroupId { get; set; }

        public string NodeName { get; set; }

        public string LocationName { get; set; }

        public string XmlRoute { get; set; }

        public string XmlRoad { get; set; }

        public int PlanId { get; set; }

        public string ImageTour { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this.NodeName) ? string.Empty : this.NodeName;
        }
    }
}