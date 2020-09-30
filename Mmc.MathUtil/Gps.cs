namespace Mmc.MathUtil
{
    public class Gps
    {
        private double wgLat;

        private double wgLon;

        public Gps(double wgLat, double wgLon)
        {
            setWgLat(wgLat);

            setWgLon(wgLon);
        }

        public double getWgLat()
        {
            return wgLat;
        }

        public double getWgLon()
        {
            return wgLon;
        }

        public void setWgLat(double wgLat)
        {
            this.wgLat = wgLat;
        }

        public void setWgLon(double wgLon)

        {
            this.wgLon = wgLon;
        }

        public override string ToString()

        {
            return wgLat + "," + wgLon;
        }
    }
}