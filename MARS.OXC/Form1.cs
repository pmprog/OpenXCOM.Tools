using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YamlDotNet;
using YamlDotNet.RepresentationModel;

namespace MARS.OXC
{
  public partial class Form1 : Form
  {

    private string rulesetFilename = "";
    private YamlStream rulesetStream;
    private YamlMappingNode rulesetRoot;

    private Rulesets.Countries rulesetCountries;


    public Form1()
    {
      InitializeComponent();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void openRulesetToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if( ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
      {
        string rulesetContents;

        rulesetFilename = ofd.FileName;
        this.Text = "M.A.R.S. for OpenXCOM [" + rulesetFilename + "]";

        rulesetContents = System.IO.File.ReadAllText( ofd.FileName );
        rulesetStream = new YamlStream();

        using( System.IO.StringReader docReader = new System.IO.StringReader(rulesetContents) )
        {
          rulesetStream.Load( docReader );
        }

        rulesetRoot = (YamlMappingNode)rulesetStream.Documents[0].RootNode;

        foreach( System.Collections.Generic.KeyValuePair<YamlNode, YamlNode> child in rulesetRoot.Children )
        {
          // child are "<name>:" markers in the yaml
          TreeNode coreNode = null;
          TreeNode objNode = null;

          switch( child.Key.ToString() )
          {
            case "countries":
              coreNode = oxcTree.Nodes.Add( "Countries" );
              coreNode.Tag = child.Value;

              rulesetCountries = new Rulesets.Countries();
              rulesetCountries.Load( (YamlSequenceNode)child.Value );

              foreach( Rulesets.Country c in rulesetCountries.CountryList )
              {
                objNode = coreNode.Nodes.Add( c.CountryString );
                objNode.Tag = c;
              }

              break;

            case "regions":
              coreNode = oxcTree.Nodes.Add( "Regions" );
              coreNode.Tag = child.Value;
              break;
          }

          /*
          if( coreNode == null )
          {
            coreNode = oxcTree.Nodes.Add( child.Key.ToString() );
            coreNode.Tag = child.Value;
          }
          */

        }
        
      }
    }

    private void oxcTree_AfterSelect(object sender, TreeViewEventArgs e)
    {
      while( splitContainer1.Panel2.Controls.Count > 0 )
      {
        splitContainer1.Panel2.Controls.RemoveAt( 0 );
      }

      if( e.Node.Tag != null && e.Node.Tag is UserControl )
      {
        splitContainer1.Panel2.Controls.Add( (UserControl)e.Node.Tag );
        splitContainer1.Panel2.Controls[0].Dock = DockStyle.Fill;
      }

    }

  }
}
