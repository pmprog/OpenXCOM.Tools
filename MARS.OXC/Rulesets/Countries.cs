using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet;
using YamlDotNet.RepresentationModel;


namespace MARS.OXC.Rulesets
{

  class Countries
  {
    public List<Country> CountryList = new List<Country>();

    public void Load(YamlSequenceNode CountriesNode)
    {
      foreach( YamlNode childSeq in CountriesNode.Children )
      {
        if( childSeq is YamlMappingNode )
        {
          CountryList.Add( new Country( (YamlMappingNode)childSeq ) );
        }
      }
    }

    public void Save(YamlMappingNode RulesRootNode)
    {
      // TODO: Save out Yaml
    }

  }

}
