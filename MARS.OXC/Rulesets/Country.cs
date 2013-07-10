using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YamlDotNet;
using YamlDotNet.RepresentationModel;

namespace MARS.OXC.Rulesets
{
  public partial class Country : UserControl
  {
    public string CountryString;
    public int FundingBaseAmount;
    public int FundingCapAmount;
    public GlobePoint LabelPosition = new GlobePoint();
    public List<GlobeRegion> Areas = new List<GlobeRegion>();


    public Country()
    {
      InitializeComponent();
    }

    public Country(YamlMappingNode YamlObject)
    {
      InitializeComponent();

      foreach( System.Collections.Generic.KeyValuePair<YamlNode, YamlNode> childProperty in YamlObject.Children )
      {
        switch( childProperty.Key.ToString() )
        {
          case "type":
            CountryString = childProperty.Value.ToString();
            break;
          case "fundingBase":
            FundingBaseAmount = Int32.Parse( childProperty.Value.ToString() );
            break;
          case "fundingCap":
            FundingCapAmount = Int32.Parse( childProperty.Value.ToString() );
            break;
          case "labelLon":
            LabelPosition.Longitude = float.Parse( childProperty.Value.ToString() );
            break;
          case "labelLat":
            LabelPosition.Latitude = float.Parse( childProperty.Value.ToString() );
            break;
          case "areas":
            YamlSequenceNode areaList = (YamlSequenceNode)childProperty.Value;

            foreach( YamlNode area in areaList.Children )
            {
              YamlSequenceNode areaSeq = (YamlSequenceNode)area;
              GlobeRegion newRegion = new GlobeRegion();
              newRegion.Minimum.Longitude = float.Parse( areaSeq.Children[0].ToString() );
              newRegion.Maximum.Longitude = float.Parse( areaSeq.Children[1].ToString() );
              newRegion.Minimum.Latitude = float.Parse( areaSeq.Children[2].ToString() );
              newRegion.Maximum.Latitude = float.Parse( areaSeq.Children[3].ToString() );
              Areas.Add( newRegion );
            }

            break;
        }
      }
    }

  }
}
