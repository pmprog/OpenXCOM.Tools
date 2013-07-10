using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS.OXC
{

  public class GlobePoint
  {
    public float Longitude = 0;
    public float Latitude = 0;

    public GlobePoint()
    {
    }

    public GlobePoint(float Lng, float Lat)
    {
      Longitude = Lng;
      Latitude = Lat;
    }

  }

  public class GlobeRegion
  {
    public GlobePoint Minimum = new GlobePoint();
    public GlobePoint Maximum = new GlobePoint();

    public GlobeRegion()
    {
    }

    public GlobeRegion(GlobePoint Min, GlobePoint Max)
    {
      Minimum = Min;
      Maximum = Max;
    }

    public GlobeRegion(float MinLng, float MinLat, float MaxLng, float MaxLat)
    {
      Minimum.Longitude = MinLng;
      Minimum.Latitude = MinLat;
      Maximum.Longitude = MaxLng;
      Maximum.Latitude = MaxLat;

    }

  }

}
